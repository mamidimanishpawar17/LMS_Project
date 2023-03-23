using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_API_BusinessLayer.Messaging
{
    public interface IReminderService
    {
        Task SendReminder();
        Task SendReminderConfirm(IMessageSender messageSender, string recipient, string subject, string body);
    }
}
