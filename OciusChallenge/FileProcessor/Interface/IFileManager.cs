using Microsoft.AspNetCore.Mvc;

namespace FileProcessor.Interface
{
    public interface IFileManager
    {
        public string ProcessFile(string pickupPath, string dropPath, char seperator, int parallelCount, int batchSize, int batchCount);

    }
}
