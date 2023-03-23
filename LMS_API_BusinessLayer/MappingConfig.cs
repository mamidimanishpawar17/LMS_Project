using AutoMapper;

using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.DTO.AuthDto;
using LMS_API_DataLayer.Models.DTO.Author;
using LMS_API_DataLayer.Models.DTO.Book;
using LMS_API_DataLayer.Models.DTO.Issue;
using LMS_API_DataLayer.Models.DTO.Member;
using LMS_API_DataLayer.Models.DTO.Subjects;
using LMS_API_DataLayer.Models.Issues;
using LMS_API_DataLayer.Models.Members;
using LMS_API_DataLayer.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_API_BusinessLayer
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Author, AuthorCreateDTO>().ReverseMap();
            CreateMap<Author, AuthorUpdateDTO>().ReverseMap();


            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Book, BookCreateDTO>().ReverseMap();
            CreateMap<Book, BookUpdateDTO>().ReverseMap();

            CreateMap<IssueOverDue, IssueOverDueDTO>().ReverseMap();
            CreateMap<IssueOverDue, IssueOverDueCreateDTO>().ReverseMap();
            CreateMap<IssueOverDue, IssueOverDueUpdateDTO>().ReverseMap();

            CreateMap<Issue, IssueDTO>().ReverseMap();
            CreateMap<Issue, IssueCreateDTO>().ReverseMap();
            CreateMap<Issue, IssueUpdateDTO>().ReverseMap();

            CreateMap<Member, MemberDTO>().ReverseMap();
            CreateMap<Member, MemberCreateDTO>().ReverseMap();
            CreateMap<Member, MemberUpdateDTO>().ReverseMap();

            CreateMap<Subject, SubjectDTO>().ReverseMap();
            CreateMap<Subject, SubjectCreateDTO>().ReverseMap();
            CreateMap<Subject, SubjectUpdateDTO>().ReverseMap();

            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        }
    }
}
