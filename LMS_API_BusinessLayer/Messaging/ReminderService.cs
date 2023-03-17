
using LMS_API_BusinessLayer.Contracts;
using Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
namespace Messaging
{
    public class ReminderService : BackgroundService, IReminderService
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IConfiguration _configuration;

        public ReminderService(IIssueRepository issueRepository, IConfiguration configuration)
        {
            _issueRepository = issueRepository;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var overdueIssues = await _issueRepository.GetOverdueIssues();
                foreach (var issue in overdueIssues)
                {
                    if (issue.returnDate.HasValue && issue.FinePaid)
                    {
                        // Book has been returned and fine has been paid, no need to send reminders
                        continue;
                    }
                    var member = issue.Member;
                    var fineAmount = CalculateFineAmount(issue.DueDate);
                    var subject = "Reminder: Book Due Date Passed";
                    var body = $"Dear {member.FirstName} {member.LastName}," +
                        $"\n\nThis is a reminder that the due date for your book \"{issue.Book.Title}\" has passed. " +
                        $"Please return the book as soon as possible to avoid any additional fines. Your current fine amount is {fineAmount:C}." +
                        $"\n\nThank you,\nThe Library";
                    var emailSender = new EmailMessageSender("smtp.gmail.com", 587, "manishtestmail17@gmail.com", "jyrnfpfzpnughszi", "manishtestmail17@gmail.com");
                    var smsSender = new SmsMessageSender("AC0a58ba23242c7f7c781af59b11633a7b", "0bee9c44a96adf0f521a00eba42cb878", "+15073796869");
                    // Send reminder message via email or SMS
                    if (!string.IsNullOrWhiteSpace(member.Email))
                    {
                        await SendReminder(emailSender, member.Email, subject, body);
                    }
                    if (!string.IsNullOrWhiteSpace(member.PhoneNumber))
                    {
                        await SendReminder(smsSender, member.PhoneNumber, subject, body);
                    }

                    await _issueRepository.UpdateAsync(issue);
                }
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken); // Delay for 1 day
            }
        }
        public async Task SendReminder(IMessageSender messageSender, string recipient, string subject, string body)
        {
            try
            {
                await messageSender.SendAsync(recipient, subject, body);
            }
            catch (Exception ex)
            {
                // log the error
                Console.WriteLine($"Error sending reminder message to {recipient}: {ex.Message}");
            }
        }
        public decimal CalculateFineAmount(DateTime dueDate)
        {
            const decimal finePerDay = 10.0m;
            var daysOverdue = Math.Max((DateTime.Now - dueDate).Days, 0);
            return daysOverdue * finePerDay;
        }
    }
}

