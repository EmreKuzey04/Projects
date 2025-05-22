using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shippers.Exceptions
{
    public class ShipperNameCannotBeEmptyException:BadRequestException
    {
        public ShipperNameCannotBeEmptyException() : base(Messages.ShipperNameCannotBeEmpty)
        {

        }


    }
}
