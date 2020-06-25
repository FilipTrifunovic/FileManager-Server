using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Dtos;

namespace FileManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IWordsRepository _repository;

        public WordsController(IWordsRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet("{filter}")]
        async public Task<ActionResult<IEnumerable<WordsDto>>> GetAllWords(string filter)
        {
            var wordsItems = await _repository.GetAllWords(filter);
            if(wordsItems!=null){
            return Ok(_mapper.Map<IEnumerable<WordsDto>>(wordsItems));
            }
            return NotFound();
        }

         [HttpPost]
         public async Task<ActionResult<WordsDto>> AddWord(WordsDto dto)
         {
                 var word= await _repository.CreateWord(dto);
                 if(word!=null) return Ok(_mapper.Map<WordsDto>(word));
                 
                  return StatusCode(501);
            
         }

         [HttpDelete("{id}")]
         public async Task<ActionResult<bool>> DeleteWord(Guid id){
             var deleted = await _repository.DeleteWord(id);
             if(deleted) return Ok(deleted);
             return StatusCode(501);
         }

         [HttpPut]
         public async Task<ActionResult<bool>> UpdateWord(WordsDto dto){
             var updated = await _repository.UpdateWord(dto);
             if(updated) return Ok(updated);
             return StatusCode(404);
         }
    }
}