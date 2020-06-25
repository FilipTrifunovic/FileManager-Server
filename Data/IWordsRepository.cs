using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using server.Dtos;
using server.Models;

namespace server.Data
{
    public interface IWordsRepository
    {
          bool saveChanges();
           Task<Words> CreateWord(WordsDto word);
           Task<IEnumerable<Words>> GetAllWords(string filter);
           Task<bool> DeleteWord(Guid id);

           Task<bool> UpdateWord(WordsDto dto);
    }
}