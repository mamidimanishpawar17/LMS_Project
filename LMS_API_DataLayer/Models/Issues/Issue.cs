using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.Members;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_API_DataLayer.Models.Issues
{
    public class Issue
    {
        [Key]
        public int IssueId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? returnDate { get; set; }
        public DateTime DueDate { get; set; }
        public int OverdueFine { get; set; }
        public bool FinePaid { get; set; }
        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public Member Member { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
