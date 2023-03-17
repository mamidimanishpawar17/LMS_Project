using LMS_API_DataLayer.Models.Issues;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_API_DataLayer.Models.DTO
{
    [Index(nameof(ISBN), IsUnique = true)]
    public class BookUpdateDTO
    {
  
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Publisher { get; set; }
        public string Language { get; set; }
        [Required]
        public string ISBN { get; set; }
        public string CallNumber { get; set; }

        [MaxLength(10, ErrorMessage = "Enter the value below 10")]
        public int MaxIssueDays { get; set; }
        
    }
}
