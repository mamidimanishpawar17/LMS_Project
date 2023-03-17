using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LMS_API_DataLayer.Models.Books
{
    [Index(nameof(Name), IsUnique = true)]

    public class Author
    {
   
        public int AuthorId { get; set; }

        public string Name { get; set; }

        public List<BookAuthor> BookAuthors { get; set; }
    }
}
