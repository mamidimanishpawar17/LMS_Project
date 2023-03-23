using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_API_BusinessLayer.Messaging
{
    public interface IMessageSender
    {
        Task SendAsync(string to, string subject, string body);
    }
}
