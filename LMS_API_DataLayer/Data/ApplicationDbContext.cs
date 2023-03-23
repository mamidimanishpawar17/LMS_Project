
using LMS_API_DataLayer.Configuration;
using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.Issues;
using LMS_API_DataLayer.Models.Members;
using LMS_API_DataLayer.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace LMS_API_DataLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueOverDue> OverDues { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BookAuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new BookSubjectConfiguration());
            modelBuilder.ApplyConfiguration(new IssueConfiguration());

            modelBuilder.Entity<Book>().HasData(
                new Book
                {

                    BookId = 1,
                    Title = "To Kill a Mockingbird",
                    Description = "A classic novel about racism and justice in a small Southern town.",
                    Publisher = "HarperCollins",
                    Language = "English",
                    ISBN = "9780061120084",
                    CallNumber = "F LEE",
                    MaxIssueDays = 14
                },
                new Book
                {

                    BookId = 2,
                    Title = "The Catcher in the Rye",
                    Description = "A coming-of-age novel about teenage angst and alienation in post-World War II America.",
                    Publisher = "Little, Brown and Company",
                    Language = "English",
                    ISBN = "9780316769488",
                    CallNumber = "F SAL",
                    MaxIssueDays = 10
                },
                new Book
                {

                    BookId = 3,
                    Title = "The Lord of the Rings",
                    Description = "A high fantasy novel set in the fictional world of Middle-earth," +
                    " following hobbit Frodo Baggins on his quest to destroy the One Ring.",
                    Publisher = "George Allen & Unwin",
                    Language = "English",
                    ISBN = "9780395489314",
                    CallNumber = "F TOL",
                    MaxIssueDays = 14
                },
                new Book
                {

                    BookId = 4,
                    Title = "Pride and Prejudice",
                    Description = "A romantic novel set in 19th-century England, " +
                    "following the Bennet family and their five daughters, particularly the headstrong Elizabeth Bennet.",
                    Publisher = "T. Egerton, Whitehall",
                    Language = "English",
                    ISBN = "9780141439518",
                    CallNumber = "F AUS",
                    MaxIssueDays = 7
                },
                new Book
                {

                    BookId = 5,
                    Title = "1984",
                    Description = "A dystopian novel set in a totalitarian society, following protagonist Winston Smith as he rebels against the oppressive government.",
                    Publisher = "Secker & Warburg",
                    Language = "English",
                    ISBN = "9780451524935",
                    CallNumber = "F ORW",
                    MaxIssueDays = 14
                }
                );
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    AuthorId = 1,
                    Name = "Harper Lee"
                },
                 new Author
                 {
                     AuthorId = 2,
                     Name = "J.D. Salinger"
                 },
                  new Author
                  {
                      AuthorId = 3,
                      Name = "J.R.R. Tolkien"

                  },
                   new Author
                   {
                       AuthorId = 4,
                       Name = "Jane Austen"
                   },
                    new Author
                    {
                        AuthorId = 5,
                        Name = "George Orwell"
                    }
                );
            modelBuilder.Entity<Subject>().HasData(
                new Subject
                {
                    SubjectId = 1,
                    Name = "Southern literature"
                },
                new Subject
                {
                    SubjectId = 2,
                    Name = "Coming-of-age fiction"
                },
                new Subject
                {
                    SubjectId = 3,
                    Name = "High fantasy"
                },
                new Subject
                {
                    SubjectId = 4,
                    Name = "Romance"

                },
                new Subject
                {
                    SubjectId = 5,
                    Name = "Dystopian fiction"
                }

            );
            modelBuilder.Entity<BookAuthor>().HasData(
                new BookAuthor
                {
                    BookId = 1,
                    AuthorId = 1
                },
                new BookAuthor
                {
                    BookId = 2,
                    AuthorId = 2
                },
                new BookAuthor
                {
                    BookId = 3,
                    AuthorId = 3
                },
                new BookAuthor
                {
                    BookId = 4,
                    AuthorId = 4
                },
                new BookAuthor
                {
                    BookId = 5,
                    AuthorId = 5
                }
                );
            modelBuilder.Entity<BookSubject>().HasData(
                new BookSubject
                {
                    BookId = 1,
                    SubjectId = 1
                },
                new BookSubject
                {
                    BookId = 2,
                    SubjectId = 2
                },
                new BookSubject
                {
                    BookId = 3,
                    SubjectId = 3
                },
                new BookSubject
                {
                    BookId = 4,
                    SubjectId = 4
                },
                new BookSubject
                {
                    BookId = 5,
                    SubjectId = 5
                }
                );
            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    MemberId = 1,
                    FirstName = "Manish",
                    LastName = "Pawar",
                    UserName = "manishpawar",
                    PhoneNumber = "+91 9491222042",
                    Password = "Password",
                    Email = "manishtestmail17@gmail.com",
                    Fine = 0,
                    MemberType = MemberType.Staff,
                },
                new Member
                {
                    MemberId = 2,
                    FirstName = "Ashwak",
                    LastName = "Rayan",
                    UserName = "ashwakrayan",
                    PhoneNumber = "+91 7095345417",
                    Password = "Password",
                    Email = "Ashwak8143@gmail.com",
                    Fine = 0,
                    MemberType = MemberType.Student,
                },
                new Member
                {
                    MemberId = 3,
                    FirstName = "Manohar",
                    LastName = "Pawar",
                    UserName = "manoharpawar",
                    PhoneNumber = "+91 9866128276",
                    Password = "Password",
                    Email = "mamidiman@gmail.com",
                    Fine = 0,
                    MemberType = MemberType.Teacher,
                }
                );
            modelBuilder.Entity<Issue>().HasData(
                new Issue
                {
                    IssueId = 1,
                    IssueDate = new DateTime(2022, 12, 01),
                    returnDate = null,
                    DueDate = new DateTime(2023, 03, 15),
                    OverdueFine = 0,
                    FinePaid = false,
                    MemberId = 1,
                    BookId = 1
                },
                new Issue
                {
                    IssueId = 2,
                    IssueDate = new DateTime(2022, 12, 01),
                    returnDate = null,
                    DueDate = new DateTime(2023, 03, 16),
                    OverdueFine = 0,
                    FinePaid = false,
                    MemberId = 2,
                    BookId = 2
                },
                new Issue
                {
                    IssueId = 3,
                    IssueDate = new DateTime(2022, 12, 01),
                    returnDate = new DateTime(2023, 03, 15),
                    DueDate = new DateTime(2023, 03, 16),
                    OverdueFine = 0,
                    FinePaid = false,
                    MemberId = 3,
                    BookId = 3
                },
                 new Issue
                 {
                     IssueId = 4,
                     IssueDate = new DateTime(2022, 12, 01),
                     returnDate = null,
                     DueDate = new DateTime(2023, 03, 14),
                     OverdueFine = 10,
                     FinePaid = false,
                     MemberId = 1,
                     BookId = 2
                 }

                );
        }
    }
}

