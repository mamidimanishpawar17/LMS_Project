using System;

namespace LMS_WEB.Models.DTO
{
    public class IssueCreateDTO
    {
     
        public int IssueId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? returnDate { get; set; }
        public DateTime DueDate { get; set; }
        public int OverdueFine { get; set; }
        public bool FinePaid { get; set; }

        public int MemberId { get; set; }
 

        public int BookId { get; set; }
     
    }
}
