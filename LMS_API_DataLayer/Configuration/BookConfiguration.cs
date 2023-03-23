using LMS_API_DataLayer.Models.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LMS_API_DataLayer.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(b => b.BookId);
            builder.HasOne(i => i.Issue).WithOne(b => b.Book).HasForeignKey<Book>(b => b.BookId);

        }
    }
}
