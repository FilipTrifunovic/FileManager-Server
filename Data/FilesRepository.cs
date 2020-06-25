using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class FilesRepository : IFilesRepository
    {
        private readonly DataContext _context;

        public FilesRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<decimal> AddFileForCheck(IFormFile file,string text)
        {
            var listOfWords = await _context.Words.Where(x=>x.IsDeleted==false).ToListAsync();
            var score = CountArrayOccuranceCharByChar(file,listOfWords,text);
            return score;
        }


            public decimal CountArrayOccuranceCharByChar(IFormFile file,List<Words> words,string text=null){
            int[] occurances = new int[words.Count];
            byte[] textByteArray=text!=null ? Encoding.UTF8.GetBytes(text) : null;

            decimal grade = 0;
            StringBuilder buffer = new StringBuilder();
            if (file?.Length > 0 || text!=null)
            {
                using (var stream = file!=null ? file.OpenReadStream() : new MemoryStream(textByteArray))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        int nextChar;
                        while ((nextChar = reader.Read()) != -1)
                        {
                            char ch = (char)nextChar;
                            if(char.IsLetter(ch)){
                                ch=char.ToLower(ch);
                                buffer.Append(ch);
                                for (int i = 0; i < words.Count; i++)
                                {
                                    string word = words[i].Text;
                                    if(EndsWith(buffer,word)){
                                        occurances[i]++;
                                    }
                                }
                            }
                            else{
                                buffer.Clear();
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < words.Count; i++)
            {
               grade += words[i].Value*occurances[i];
            }
            return grade;
        }
         bool EndsWith(StringBuilder buffer,string str){
            if(buffer.Length<str.Length){
                return false;
            }
            for (int bufIndex = buffer.Length - str.Length, strIndex =0; strIndex< str.Length; bufIndex++,strIndex++){
                if(buffer[bufIndex] != str[strIndex]){
                    return false;
                }
            }
            return true;
        }
    }
}