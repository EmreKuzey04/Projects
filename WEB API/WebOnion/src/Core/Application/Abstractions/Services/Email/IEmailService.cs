using Application.Models.Parameters.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services.Email
{
    public interface IEmailService
    {
       Task SendAsync(EmailRequest email);
    }
}
