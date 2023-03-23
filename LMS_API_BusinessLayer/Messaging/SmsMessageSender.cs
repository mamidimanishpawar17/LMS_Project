using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;

namespace LMS_API_BusinessLayer.Messaging
{
    public class SmsMessageSender : IMessageSender
    {
        private readonly IConfiguration configuration;
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly TwilioRestClient _client;
        private readonly string _fromPhoneNumber;

        public SmsMessageSender(string accountSid, string authToken, string fromPhoneNumber)
        {
            //_accountSid = configuration["Twilio:AccountSid"];
            //_authToken = configuration["Twilio:AuthToken"];
            //_fromPhoneNumber = configuration["Twilio:FromPhoneNumber"];
            _accountSid = accountSid;
            _authToken = authToken;
            _fromPhoneNumber = fromPhoneNumber;
            _client = new TwilioRestClient(_accountSid, _authToken);
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            var message = await MessageResource.CreateAsync(
                body: body,
                from: new Twilio.Types.PhoneNumber(_fromPhoneNumber),
                to: new Twilio.Types.PhoneNumber(to)
            );
        }
    }
}
