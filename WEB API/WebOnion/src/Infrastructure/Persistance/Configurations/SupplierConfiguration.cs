using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            //builder.ToTable("Tedarikciler");

            //builder.Property(x => x.SupplierName)
            //        .HasColumnName("Tedarikci adı")
            //        .HasMaxLength(20)
            //        .IsRequired();

            
        }
    }
}
