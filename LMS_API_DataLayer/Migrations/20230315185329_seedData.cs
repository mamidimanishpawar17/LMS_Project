using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_API_DataLayer.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Name" },
                values: new object[,]
                {
                    { 1, "Harper Lee" },
                    { 2, "J.D. Salinger" },
                    { 3, "J.R.R. Tolkien" },
                    { 4, "Jane Austen" },
                    { 5, "George Orwell" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "CallNumber", "Description", "ISBN", "Language", "MaxIssueDays", "Publisher", "Title" },
                values: new object[,]
                {
                    { 1, "F LEE", "A classic novel about racism and justice in a small Southern town.", "9780061120084", "English", 14, "HarperCollins", "To Kill a Mockingbird" },
                    { 2, "F SAL", "A coming-of-age novel about teenage angst and alienation in post-World War II America.", "9780316769488", "English", 10, "Little, Brown and Company", "The Catcher in the Rye" },
                    { 3, "F TOL", "A high fantasy novel set in the fictional world of Middle-earth, following hobbit Frodo Baggins on his quest to destroy the One Ring.", "9780395489314", "English", 14, "George Allen & Unwin", "The Lord of the Rings" },
                    { 4, "F AUS", "A romantic novel set in 19th-century England, following the Bennet family and their five daughters, particularly the headstrong Elizabeth Bennet.", "9780141439518", "English", 7, "T. Egerton, Whitehall", "Pride and Prejudice" },
                    { 5, "F ORW", "A dystopian novel set in a totalitarian society, following protagonist Winston Smith as he rebels against the oppressive government.", "9780451524935", "English", 14, "Secker & Warburg", "1984" }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberId", "Email", "Fine", "FirstName", "LastName", "MemberType", "Password", "PhoneNumber", "UserName" },
                values: new object[,]
                {
                    { 1, "manishtestmail17@gmail.com", 0, "Manish", "Pawar", 3, "Password", "+91 9491222042", "manishpawar" },
                    { 2, "Ashwak8143@gmail.com", 0, "Ashwak", "Rayan", 1, "Password", "+91 7095345417", "ashwakrayan" },
                    { 3, "mamidiman@gmail.com", 0, "Manohar", "Pawar", 2, "Password", "+91 9866128276", "manoharpawar" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "Name" },
                values: new object[,]
                {
                    { 1, "Southern literature" },
                    { 2, "Coming-of-age fiction" },
                    { 3, "High fantasy" },
                    { 4, "Romance" },
                    { 5, "Dystopian fiction" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthor",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "BookSubject",
                columns: new[] { "BookId", "SubjectId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "Issues",
                columns: new[] { "IssueId", "BookId", "DueDate", "FinePaid", "IssueDate", "MemberId", "OverdueFine", "returnDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, null },
                    { 3, 3, new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 0, new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 2, new DateTime(2023, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 10, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "BookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "BookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "BookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "BookSubject",
                keyColumns: new[] { "BookId", "SubjectId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BookSubject",
                keyColumns: new[] { "BookId", "SubjectId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "BookSubject",
                keyColumns: new[] { "BookId", "SubjectId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "BookSubject",
                keyColumns: new[] { "BookId", "SubjectId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "BookSubject",
                keyColumns: new[] { "BookId", "SubjectId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "IssueId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "IssueId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "IssueId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "IssueId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 5);
        }
    }
}
