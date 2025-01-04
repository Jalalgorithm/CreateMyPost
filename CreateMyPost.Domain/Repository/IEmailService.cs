using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Domain.Repository
{
    public interface IEmailService
    {
        Task SendEmail(string recipientName, string recipientEmail, string subject, string body);
    }
}
