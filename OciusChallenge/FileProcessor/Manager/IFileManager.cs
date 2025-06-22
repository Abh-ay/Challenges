using FileProcessor.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileProcessor.Manager
{
    public interface IFileManager
    {
        public Task<string> ProcessFileAsync(InputClass inputValue);

    }
}
