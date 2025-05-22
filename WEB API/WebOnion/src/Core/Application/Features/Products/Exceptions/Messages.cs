using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Exceptions
{
    public static class Messages
    {
        public const string UnitPriceCannotBeNegative = "Ürün Fiyatı Negatif Olamaz";
        public const string ProductNameCannotBeEmpty = "Ürün Adı Boş Bırakılamaz";
    }
}
