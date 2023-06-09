﻿// <auto-generated />
using System;
using LMS_API_DataLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LMS_API_DataLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230315185329_seedData")]
    partial class seedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LMS_API_DataLayer.Models.Books.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorId"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AuthorId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            AuthorId = 1,
                            Name = "Harper Lee"
                        },
                        new
                        {
                            AuthorId = 2,
                            Name = "J.D. Salinger"
                        },
                        new
                        {
                            AuthorId = 3,
                            Name = "J.R.R. Tolkien"
                        },
                        new
                        {
                            AuthorId = 4,
                            Name = "Jane Austen"
                        },
                        new
                        {
                            AuthorId = 5,
                            Name = "George Orwell"
                        });
                });

            modelBuilder.Entity("LMS_API_DataLayer.Models.Books.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"), 1L, 1);

                    b.Property<string>("CallNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxIssueDays")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookId");

                    b.HasIndex("ISBN")
                        .IsUnique();

                    b.ToTable("Books", (string)null);

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            CallNumber = "F LEE",
                            Description = "A classic novel about racism and justice in a small Southern town.",
                            ISBN = "9780061120084",
                            Language = "English",
                            MaxIssueDays = 14,
                            Publisher = "HarperCollins",
                            Title = "To Kill a Mockingbird"
                        },
                        new
                        {
                            BookId = 2,
                            CallNumber = "F SAL",
                            Description = "A coming-of-age novel about teenage angst and alienation in post-World War II America.",
                            ISBN = "9780316769488",
                            Language = "English",
                            MaxIssueDays = 10,
                            Publisher = "Little, Brown and Company",
                            Title = "The Catcher in the Rye"
                        },
                        new
                        {
                            BookId = 3,
                            CallNumber = "F TOL",
                            Description = "A high fantasy novel set in the fictional world of Middle-earth, following hobbit Frodo Baggins on his quest to destroy the One Ring.",
                            ISBN = "9780395489314",
                            Language = "English",
                            MaxIssueDays = 14,
                            Publisher = "George Allen & Unwin",
                            Title = "The Lord of the Rings"
                        },
                        new
                        {
                            BookId = 4,
                            CallNumber = "F AUS",
                            Description = "A romantic novel set in 19th-century England, following the Bennet family and their five daughters, particularly the headstrong Elizabeth Bennet.",
                            ISBN = "9780141439518",
                            Language = "English",
                            MaxIssueDays = 7,
                            Publisher = "T. Egerton, Whitehall",
                            Title = "Pride and Prejudice"
                        },
                        new
                        {
                            BookId = 5,
                            CallNumber = "F ORW",
                            Description = "A dystopian novel set in a totalitarian society, following protagonist Winston Smith as he rebels against the oppressive government.",
                            ISBN = "9780451524935",
                            Language = "English",
                            MaxIssueDays = 14,
                            Publisher = "Secker & Warburg",
                            Title = "1984"
                        });
                });

            modelBuilder.Entity("LMS_API_DataLayer.Models.Books.BookAuthor", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.HasKey("BookId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("BookAuthor", (string)null);

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            AuthorId = 1
                        },
                        new
                        {
                            BookId = 2,
                            AuthorId = 2
                        },
                        new
                        {
                            BookId = 3,
                            AuthorId = 3
                        },
                        new
                        {
                            BookId = 4,
                            AuthorId = 4
                        },
                        new
                        {
                            BookId = 5,
                            AuthorId = 5
                        });
                });

            modelBuilder.Entity("LMS_API_DataLayer.Models.Books.BookSubject", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("BookId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("BookSubject", (string)null);

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            SubjectId = 1
                        },
                        new
                        {
                            BookId = 2,
                            SubjectId = 2
                        },
                        new
                        {
                            BookId = 3,
                            SubjectId = 3
                        },
                        new
                        {
                            BookId = 4,
                            SubjectId = 4
                        },
                        new
                        {
                            BookId = 5,
                            SubjectId = 5
                        });
                });

            modelBuilder.Entity("LMS_API_DataLayer.Models.Books.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectId"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SubjectId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Subjects");

                    b.HasData(
                        new
                        {
                            SubjectId = 1,
                            Name = "Southern literature"
                        },
                        new
                        {
                            SubjectId = 2,
                            Name = "Coming-of-age fiction"
                        },
                        new
                        {
                            SubjectId = 3,
                            Name = "High fantasy"
                        },
                        new
                        {
                            SubjectId = 4,
                            Name = "Romance"
                        },
                        new
                        {
                            SubjectId = 5,
                            Name = "Dystopian fiction"
                        });
                });

            modelBuilder.Entity("LMS_API_DataLayer.Models.Issues.Issue", b =>
                {
                    b.Property<int>("IssueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IssueId"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("FinePaid")
                        .HasColumnType("bit");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("OverdueFine")
                        .HasColumnType("int");

                    b.Property<DateTime?>("returnDate")
                        .HasColumnType("datetime2");

                    b.HasKey("IssueId");

                    b.HasIndex("BookId")
                        .IsUnique();

                    b.HasIndex("MemberId");

                    b.ToTable("Issues", (string)null);

                    b.HasData(
                        new
                        {
                            IssueId = 1,
                            BookId = 1,
                            DueDate = new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FinePaid = false,
                            IssueDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MemberId = 1,
                            OverdueFine = 0
                        },
                        new
                        {
                            IssueId = 2,
                            BookId = 2,
                            DueDate = new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FinePaid = false,
                            IssueDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MemberId = 2,
                            OverdueFine = 0
                        },
                        new
                        {
                            IssueId = 3,
                            BookId = 3,
                            DueDate = new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FinePaid = false,
                            IssueDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MemberId = 3,
                            OverdueFine = 0,
                            returnDate = new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            IssueId = 4,
                            BookId = 2,
                            DueDate = new DateTime(2023, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FinePaid = false,
                            IssueDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MemberId = 1,
                            OverdueFine = 10
                        });
                });

            modelBuilder.Entity("LMS_API_DataLayer.Models.Issues.IssueOverDue", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("FinePaid")
                        .HasColumnType("bit");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IssueId")
                        .HasColumnType("int");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("OverDueIssueNo")
                        .HasColumnType("int");

                    b.Property<int>("OverdueFine")
                        .HasColumnType("int");

                    b.Property<DateTime?>("returnDate")
                        .HasColumnType("datetime2");

                    b.HasIndex("IssueId");

                    b.ToTable("OverDues");
                });

            modelBuilder.Entity("LMS_API_DataLayer.Models.Members.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Fine")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MemberType")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("MemberId");

                    b.HasIndex("FirstName")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Members");

                    b.HasData(
                        new
                        {
                            MemberId = 1,
                            Email = "manishtestmail17@gmail.com",
                            Fine = 0,
                            FirstName = "Manish",
                            LastName = "Pawar",
                            MemberType = 3,
                            Password = "Password",
                            PhoneNumber = "+91 9491222042",
                            UserName = "manishpawar"
                        },
                        new
                        {
                            MemberId = 2,
                            Email = "Ashwak8143@gmail.com",
                            Fine = 0,
                            FirstName = "Ashwak",
                            LastName = "Rayan",
                            MemberType = 1,
                            Password = "Password",
                            PhoneNumber = "+91 7095345417",
                            UserName = "ashwakrayan"
                        },
                        new
                        {
                            MemberId = 3,
                            Email = "mamidiman@gmail.com",
                            Fine = 0,
                            FirstName = "Manohar",
                            LastName = "Pawar",
                            MemberType = 2,
                            Password = "Password",
                            PhoneNumber = "+91 9866128276",
                            UserName = "manoharpawar"
                        });
                });

            modelBuilder.Entity("LMS_API_DataLayer.Models.User.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("LMS_API_DataLayer.Models.User.LocalUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LocalUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("LMS_API_DataLayer.Models.Books.BookAuthor", b =>
                {
                    b.HasOne("LMS_API_DataLayer.Models.Books.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS_API_DataLayer.Models.Books.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LMS_API_DataLayer.Models.Books.BookSubject", b =>
                {
                    b.HasOne("LMS_API_DataLayer.Models.Books.Book", "Book")
                        .WithMany("BookSubjects")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS_API_DataLayer.Models.Books.Subject", "Subject")
                        .WithMany("BookSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("LMS_API_DataLayer.Models.Issues.Issue", b =>
                {
                    b.HasOne("LMS_API_DataLayer.Models.Books.Book", "Book")
                        .WithOne("Issue")
                        .HasForeignKey("LMS_API_DataLayer.Models.Issues.Issue", "BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS_API_DataLayer.Models.Members.Member", "Member")
                        .WithMany("Issues")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("LMS_API_DataLayer.Models.Issues.IssueOverDue", b =>
                {
                    b.HasOne("LMS_API_DataLayer.Models.Issues.Issue", "Issue")
                        .WithMany()
                        .HasForeignKey("IssueId");

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LMS_API_DataLayer.Models.User.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LMS_API_DataLayer.Models.User.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS_API_DataLayer.Models.User.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LMS_API_DataLayer.Models.User.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LMS_API_DataLayer.Models.Books.Author", b =>
                {
                    b.Navigation("BookAuthors");
                });

            modelBuilder.Entity("LMS_API_DataLayer.Models.Books.Book", b =>
                {
                    b.Navigation("BookAuthors");

                    b.Navigation("BookSubjects");

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("LMS_API_DataLayer.Models.Books.Subject", b =>
                {
                    b.Navigation("BookSubjects");
                });

            modelBuilder.Entity("LMS_API_DataLayer.Models.Members.Member", b =>
                {
                    b.Navigation("Issues");
                });
#pragma warning restore 612, 618
        }
    }
}
