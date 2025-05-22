using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Exceptions
{
    public class ProductNameCannotBeEmptyException:BadHttpRequestException
    {
        public ProductNameCannotBeEmptyException():base(Messages.ProductNameCannotBeEmpty)
        {
            
        }
    }
}
