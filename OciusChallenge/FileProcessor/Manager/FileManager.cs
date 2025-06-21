using FileProcessor.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FileProcessor.Folder
{
    public class FileManager : IFileManager
    {
        private readonly IFileRepository _fileRepository;

        public FileManager(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }


        public string ProcessFile(string pickupPath, string dropPath, char seperator, int parallelCount, int batchSize, int batchCount)
        {
            foreach (var filePath in Directory.GetFiles(pickupPath))
            {
                var fileName = Path.GetFileName(filePath);
                var dropFilePath = Path.Combine(dropPath, fileName);
                bool isSuccess = false;

                try
                {
                    var fileText = System.IO.File.ReadAllText(filePath);

                    var fileLog = new FileProcessLog
                    {
                        FileName = fileName,
                        FileText = fileText,
                        IsSuccess = false,
                        Separator = new string(new[] { seperator }),
                    };

                    _fileRepository.SaveFileLogs(fileLog);

                    var dict = fileText
                        .Split(seperator, StringSplitOptions.RemoveEmptyEntries)
                        .GroupBy(i => i.Trim())
                        .ToDictionary(j => j.Key, j => j.Count());

                    var wordDetails = dict.Select(kvp => new LinkFileWordDetails
                    {
                        Word = kvp.Key,
                        Count = kvp.Value,
                        FileProcessLogId = fileLog.FileProcessLogId
                    }).ToList();

                    _fileRepository.SaveWordFileDetails(wordDetails,fileLog);

                }
                catch (Exception ex)
                {
                    return $"Error processing file {filePath}: {ex.Message}";
                }
                finally
                {
                    try
                    {
                        if (isSuccess)
                        {

                            if (!Directory.Exists(dropPath))
                                Directory.CreateDirectory(dropPath);

                            if (System.IO.File.Exists(dropFilePath))
                                System.IO.File.Delete(dropFilePath);

                            System.IO.File.Move(filePath, dropFilePath);
                        }
                    }
                    catch (Exception moveEx)
                    {
                        Console.WriteLine($"Error moving file to drop folder: {moveEx.Message}");
                    }
                }
            }
            return $"File Processed successfully";

        }
    }
}
