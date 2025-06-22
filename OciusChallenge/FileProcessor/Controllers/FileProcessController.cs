using FileProcessor.Manager;
using FileProcessor.Models;
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
        public async Task<IActionResult> Post(InputClass inputValue)
        {
            return Ok(await _fileManager.ProcessFileAsync(inputValue));
        }

    }
}
