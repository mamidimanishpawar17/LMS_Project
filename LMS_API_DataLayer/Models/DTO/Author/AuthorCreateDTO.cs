using Microsoft.EntityFrameworkCore;

namespace LMS_API_DataLayer.Models.DTO
{
    [Index(nameof(Name), IsUnique = true)]

    public class AuthorCreateDTO
    {

        public int AuthorId { get; set; }

        public string Name { get; set; }

    
    }
}
