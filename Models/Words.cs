using System;
using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Words
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Text { get; set; }
        [Range(-1, 1)]
        public decimal Value {get;set;}

        public bool IsDeleted { get; set; }
    }
}