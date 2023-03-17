using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LMS_WEB.Models.DTO
{
    [Index(nameof(Name), IsUnique = true)]
    public class SubjectCreateDTO
    {
       
        public int SubjectId { get; set; }
        public string Name { get; set; }

    }
}
