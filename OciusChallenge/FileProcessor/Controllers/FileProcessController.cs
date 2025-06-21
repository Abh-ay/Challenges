using FileProcessor.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TempFileProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileProcessController : ControllerBase
    {
        private readonly IFileManager _fileManager;

        public FileProcessController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpPost]
        public IActionResult Post(string pickupPath, string dropPath, char seperator, int parallelCount, int batchSize, int batchCount)
        {
            return Ok(_fileManager.ProcessFile(pickupPath, dropPath, seperator, parallelCount, batchSize, batchCount));
        }

    }
}
