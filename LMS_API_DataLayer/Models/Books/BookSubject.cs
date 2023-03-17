using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_API_DataLayer.Models.Books
{
    public class BookSubject
    {
        [ForeignKey("Book")]
        public int BookId { get; set; }
        
        public Book Book { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        
        public Subject Subject { get; set; }
    }
}
