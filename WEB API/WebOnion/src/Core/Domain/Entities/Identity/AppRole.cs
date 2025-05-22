using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Identity
{
    public class AppRole:IdentityRole<int>
    {
        public AppRole() 
        {
            
        }   
        
        public AppRole(string roleName) : base(roleName)
        {

        }

        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
