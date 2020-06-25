using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FileManager.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Dtos;
using server.Models;

namespace server.Data
{
    public class WordsRepository : IWordsRepository
    {
        private readonly DataContext _context;


        public WordsRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Words> CreateWord(WordsDto word)
        {
            try
            {
                var wordModel = new Words
                {
                    Id = new Guid(),
                    Text = word.Text,
                    IsDeleted = false,
                    Value = word.Value
                };
                await _context.Words.AddAsync(wordModel);
                saveChanges();
                return wordModel;
            }
            catch (System.Exception)
            {
                return null;
            }


        }

        public async Task<bool> DeleteWord(Guid id)
        {
            try
            {
                var word = await _context.Words.FirstOrDefaultAsync(x => x.Id == id);
                if (word == null) return false;

                word.IsDeleted = true;
                return saveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        public async Task<IEnumerable<Words>> GetAllWords(string filter)
        {
            var words = await _context.Words.Where(x => !x.IsDeleted&&(filter!="all" ? (filter=="positive" ? x.Value>=0 : x.Value<0):true)).ToListAsync();
            return words;
        }

        public async Task<bool> UpdateWord(WordsDto dto)
        {
             try
            {
                var word = await _context.Words.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (word == null) return false;

                word.Text = dto.Text;
                word.Value = dto.Value;
                return saveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}