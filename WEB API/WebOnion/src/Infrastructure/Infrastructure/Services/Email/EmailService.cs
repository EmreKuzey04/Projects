using Application.Abstractions.Services.Email;
using Application.Models.Parameters.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        public Task SendAsync(EmailRequest email)
        {
            throw new NotImplementedException();
        }
    }
}
