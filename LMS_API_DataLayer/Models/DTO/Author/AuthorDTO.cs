using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LMS_API_DataLayer.Models.DTO
{
    [Index(nameof(Name), IsUnique = true)]

    public class AuthorDTO
    {
        [Key]
        public int AuthorId { get; set; }

        public string Name { get; set; }

       
    }
}
