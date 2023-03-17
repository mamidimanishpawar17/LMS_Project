using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LMS_API_DataLayer.Models.Books
{
    [Index(nameof(Name), IsUnique = true)]
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public List<BookSubject> BookSubjects { get; set; }
    }
}
