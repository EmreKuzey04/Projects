using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Exceptions
{
    public class WrongPasswordException:BadRequestException
    {
        public WrongPasswordException( ) : base(Messages.WrongPassword) 
        { 
        } 
    }
}
