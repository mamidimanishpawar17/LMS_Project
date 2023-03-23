using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_WEB.Models.DTO.Issue
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
