using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LMS_WEB.Models.DTO.Author
{


    public class AuthorDTO
    {
        [Key]
        public int AuthorId { get; set; }

        public string Name { get; set; }


    }
}
