using System;

namespace server.Dtos
{
    public class WordsDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public decimal Value {get;set;}
    }
}