using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LMS_WEB.Models.DTO
{
   

    public class AuthorDTO
    {
        [Key]
        public int AuthorId { get; set; }

        public string Name { get; set; }

       
    }
}
