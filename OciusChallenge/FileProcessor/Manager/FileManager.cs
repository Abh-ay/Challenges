using FileProcessor.Manager;
using FileProcessor.Misc;
using FileProcessor.Models;
using FileProcessor.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
namespace FileProcessor.Folder
{
    public class FileManager : IFileManager
    {
        private readonly IFileRepository _fileRepository;
        private readonly ILogger<FileManager> _logger;

        // To handle concurrent request 
        private static readonly ConcurrentDictionary<string, byte> _processingFiles = new();

        public FileManager(IFileRepository fileRepository, ILogger<FileManager> logger)
        {
            _fileRepository = fileRepository;
            _logger = logger;
        }


        public async Task<string> ProcessFileAsync([FromBody] InputClass inputValue)
        {
            if (inputValue.DropPath.Equals(inputValue.PickupPath))
                throw new FileException($"Both pickup and drop path cant be same Pickup Path : {inputValue.PickupPath} ,Drop Path : {inputValue.DropPath}",HttpStatusCode.MisdirectedRequest);

            var requiredFiles = Directory.GetFiles(inputValue.PickupPath).Take(inputValue.BatchCount * inputValue.BatchSize).ToArray();

            requiredFiles = requiredFiles.Except(await _fileRepository.GetAllFiles()).ToArray(); 

            _logger.LogInformation($"Total files processed : {requiredFiles.Count()}");

            //used to manage both the tables data and save the details in one shot 
            //to avoid db call in parallel.for loop
            var fileLogsDic = new ConcurrentDictionary<string, FileProcessLog>();
            var wordDetailsBag = new ConcurrentBag<List<LinkFileWordDetails>>();

            try
            {
                await Parallel.ForEachAsync(requiredFiles, new ParallelOptions { MaxDegreeOfParallelism = inputValue.ParallelCount }, async (file, CancellationToken) =>
                {

                    if (!_processingFiles.TryAdd(file, 0))
                        return;

                    try
                    {
                        var fileName = Path.GetFileName(file);
                        var dropFilePath = Path.Combine(inputValue.DropPath, fileName);

                        var fileText = await File.ReadAllTextAsync(file);
                        var fileLog = new FileProcessLog
                        {
                            FileName = fileName,
                            FileText = fileText,
                            IsSuccess = true,
                            Separator = inputValue.Separator,
                        };

                        fileLogsDic[fileName] = fileLog;

                        var wordDetails = fileText
                            .Split(inputValue.Separator, StringSplitOptions.RemoveEmptyEntries)
                            .GroupBy(i => i.Trim())
                            .ToDictionary(j => j.Key, j => j.Count()).Select(kvp => new LinkFileWordDetails
                            {
                                Word = kvp.Key,
                                Count = kvp.Value,
                                FileProcessLog = fileLog
                            }).ToList();

                        fileLog.LinkFileWordDetails = wordDetails;

                        wordDetailsBag.Add(wordDetails);

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error Occured in ProcessFileAsync() under Remarks catch block {ex}");
                        fileLogsDic[file].Remarks = ex.Message; 
                    }
                    finally
                    {
                        _processingFiles.TryRemove(file, out _);
                    }
                });
            }
            finally
            {
                //Moving files from pick up path to drop path as we no need to process the same file again
                try
                {

                    bool isSuccess = await _fileRepository.SaveFileLogsAsync(fileLogsDic.Values.ToList());

                    if (isSuccess)
                    {
                        if (!Directory.Exists(inputValue.DropPath))
                            Directory.CreateDirectory(inputValue.DropPath);

                        foreach (var item in fileLogsDic.Values)
                        {
                            string dropPath = Path.Combine(inputValue.DropPath, item.FileName);
                            string pickupPath = Path.Combine(inputValue.PickupPath, item.FileName);

                            if (File.Exists(dropPath))
                                File.Delete(inputValue.DropPath);

                            File.Move(pickupPath, dropPath);
                        }
                    }
                }
                catch (DbUpdateException ex)
                {
                    // Might be getting exception string or binry data as if user sends larger word length
                    _logger.LogError($"Something went wrong with database update operation : {ex}");
                    throw new FileException(ex.Message,HttpStatusCode.InternalServerError);
                }
            }
            return $"Files processed successfullt with {inputValue.PickupPath}";

        }
    }
}
