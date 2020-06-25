using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FileManager.Dtos;
using FileManager.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using server.Data;

namespace FileManager.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFilesRepository _repository;

        public FilesController(IFilesRepository repository)
        {
            _repository = repository;

        }

        [HttpPost]
        public async Task<IActionResult> AddFileForCheck(IFormFile file)
        {
            var result =await _repository.AddFileForCheck(file);
            return Ok(result);

        }

        [HttpPost("text")]
        public async Task<IActionResult> CheckString([FromBody]string text)
        {
            var result =await _repository.AddFileForCheck(null,text);
            return Ok(result);

        }


        public async Task<string> ReadFile(Stream file)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                string line = await sr.ReadToEndAsync();
                return line;
            }
        }










    }
}