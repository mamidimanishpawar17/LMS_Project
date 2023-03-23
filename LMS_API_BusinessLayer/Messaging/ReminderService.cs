
using LMS_API_BusinessLayer.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
namespace LMS_API_BusinessLayer.Messaging
{

    public class ReminderService : IReminderService
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IConfiguration _configuration;

        public ReminderService(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;

        }

        public async Task SendReminder()
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
                var body = $"Dear customer ," +
                    $"\n\nThis is a reminder that the due date for your book  has passed. " +
                    $"Please return the book as soon as possible to avoid any additional fines." +
                    $"\n\nThank you,\nThe Library";
                //var emailSender = new EmailMessageSender(_configuration.GetValue("SmtpSettings", "smtp.gmail.com"),
                //    _configuration.GetValue("SmtpSettings", 587),
                //    _configuration.GetValue("SmtpSettings", "SmtpUsername"),
                //    _configuration.GetValue("SmtpSettings", "SmtpPassword"),
                //    _configuration.GetValue("SmtpSettings", "FromEmail"));
                //var smsSender = new SmsMessageSender(_configuration.GetValue("Twilio", "AccountSid"),
                //    _configuration.GetValue("Twilio", "AuthToken"),
                //    _configuration.GetValue("Twilio", "FromPhoneNumber"));
                var emailSender = new EmailMessageSender("smtp.gmail.com", 587, "manishtestmail17@gmail.com", "jyrnfpfzpnughszi", "manishtestmail17@gmail.com");
                var smsSender = new SmsMessageSender("AC0a58ba23242c7f7c781af59b11633a7b", "0bee9c44a96adf0f521a00eba42cb878", "+15073796869");
                // Send reminder message via email or SMS
                if (!string.IsNullOrWhiteSpace(member.Email))
                {
                    await SendReminderConfirm(emailSender, member.Email, subject, body);
                }
                if (!string.IsNullOrWhiteSpace(member.PhoneNumber))
                {
                    await SendReminderConfirm(smsSender, member.PhoneNumber, subject, body);
                }

                await _issueRepository.UpdateAsync(issue);
            }

        }

        public async Task SendReminderConfirm(IMessageSender messageSender, string recipient, string subject, string body)
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
        private decimal CalculateFineAmount(DateTime dueDate)
        {
            const decimal finePerDay = 10.0m;
            var daysOverdue = Math.Max((DateTime.Now - dueDate).Days, 0);
            return daysOverdue * finePerDay;
        }


    }
}

