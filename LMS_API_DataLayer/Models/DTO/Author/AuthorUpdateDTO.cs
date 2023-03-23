using Microsoft.EntityFrameworkCore;

namespace LMS_API_DataLayer.Models.DTO.Author
{
    [Index(nameof(Name), IsUnique = true)]

    public class AuthorUpdateDTO
    {

        public int AuthorId { get; set; }

        public string Name { get; set; }


    }
}
