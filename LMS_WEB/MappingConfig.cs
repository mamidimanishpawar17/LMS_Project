using AutoMapper;
using LMS_WEB.Models.DTO.Author;
using LMS_WEB.Models.DTO.Book;
using LMS_WEB.Models.DTO.Issue;
using LMS_WEB.Models.DTO.Member;
using LMS_WEB.Models.DTO.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WEB
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            
            CreateMap<AuthorDTO, AuthorCreateDTO>().ReverseMap();
            CreateMap<AuthorDTO, AuthorUpdateDTO>().ReverseMap();


            //CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<BookDTO, BookCreateDTO>().ReverseMap();
            CreateMap<BookDTO, BookUpdateDTO>().ReverseMap();

            //CreateMap<IssueOverDue, IssueOverDueDTO>().ReverseMap();
            CreateMap<IssueOverDueDTO, IssueOverDueCreateDTO>().ReverseMap();
            CreateMap<IssueOverDueDTO, IssueOverDueUpdateDTO>().ReverseMap();

            //CreateMap<Issue, IssueDTO>().ReverseMap();
            CreateMap<IssueDTO, IssueCreateDTO>().ReverseMap();
            CreateMap<IssueDTO, IssueUpdateDTO>().ReverseMap();

            //CreateMap<Member, MemberDTO>().ReverseMap();
            CreateMap<MemberDTO, MemberCreateDTO>().ReverseMap();
            CreateMap<MemberDTO, MemberUpdateDTO>().ReverseMap();

            //CreateMap<Subject, SubjectDTO>().ReverseMap();
            CreateMap<SubjectDTO, SubjectCreateDTO>().ReverseMap();
            CreateMap<SubjectDTO, SubjectUpdateDTO>().ReverseMap();
           
        }
    }
}
