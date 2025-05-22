using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared
{
    public abstract class BaseEntity
    {
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        //public Datetime? Created { get; set; }
        //public Datetime? Updated { get; set; }
    }
}
