using Domain.Entities;
using Domain.Interfaces.Repositories.Categories;
using Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories.Categories
{
    public class CategoryQueryRepository:QueryRepository<Category>,ICategoryQueryRepository
    {
        public CategoryQueryRepository(TradewndContext context) : base(context)
        {
            
        }
      
    }
}
