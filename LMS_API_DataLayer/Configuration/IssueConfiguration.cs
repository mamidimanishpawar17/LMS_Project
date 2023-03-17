using LMS_API_DataLayer.Models.Issues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DataLayer.Configuration
{
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.ToTable("Issues");

            builder.HasKey(i => i.IssueId);

            builder.HasOne(b => b.Book)
                .WithOne(i => i.Issue)
                .HasForeignKey<Issue>(b => b.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Member)
                .WithMany(i => i.Issues)
                .HasForeignKey(m => m.MemberId);
            //.OnDelete(DeleteBehavior.Cascade);
        }
    }
}
