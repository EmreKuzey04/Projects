using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Exceptions
{
    public class UserNotFoundException:BadRequestException
    {
        public UserNotFoundException( ):base(Messages.UserNotFound)
        {

        }
    }
}
