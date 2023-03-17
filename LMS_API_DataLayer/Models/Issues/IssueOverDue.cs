using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.Members;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LMS_API_DataLayer.Models.Issues
{
    [Keyless]
    public class IssueOverDue
    {
        [ForeignKey("Member")]
        public int OverDueIssueNo { get; set; }
        public Issue Issue { get; set; }
        public DateTime IssueDate { get; set; }
        [JsonIgnore]
        public DateTime? returnDate { get; set; } = null;
        public DateTime DueDate { get; set; }
        public int OverdueFine { get; set; }
        public bool FinePaid { get; set; }

        public int MemberId { get; set; }
      
   
        public int BookId { get; set; }
     


    }
}
