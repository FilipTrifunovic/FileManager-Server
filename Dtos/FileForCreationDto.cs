using System;
using Microsoft.AspNetCore.Http;

namespace FileManager.Dtos
{
    public class FileForCreationDto
    {
        public Guid Id { get; set; }

        public IFormFile File { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }

        public string PublicId { get; set; }

        public FileForCreationDto()
        {
            DateAdded =DateTime.Now;
        }
    }
}