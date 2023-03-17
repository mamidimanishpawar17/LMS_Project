using LMS_API_DataLayer.Models.Issues;
using LMS_API_DataLayer.Models.Members;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LMS_API_DataLayer.Models.DTO
{
    [Index(nameof(UserName), IsUnique = true)]
    [Index(nameof(FirstName), IsUnique = true)]
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class MemberUpdateDTO
    {

    


        public int MemberId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Fine { get; set; }
        public MemberType MemberType { get; set; }

    }
}
