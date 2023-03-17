using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.Members;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_API_DataLayer.Models.DTO
{
    public class IssueOverDueDTO
    {
        public int OverDueIssueNo { get; set; }
        public int IssueId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? returnDate { get; set; } = null;
        public DateTime DueDate { get; set; }
        public int OverdueFine { get; set; }
        public bool FinePaid { get; set; }

        public int MemberId { get; set; }


        public int BookId { get; set; }

    }
}
