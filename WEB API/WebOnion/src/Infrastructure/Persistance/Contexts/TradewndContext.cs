using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;

namespace Persistance.Contexts
{
    public class TradewndContext:IdentityDbContext<AppUser,AppRole,int>
    {

        public TradewndContext(DbContextOptions<TradewndContext> options):base(options) 
        {
           
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<IdentityUserClaim<int>>();
            modelBuilder.Ignore<IdentityUserToken<int>>();
            modelBuilder.Ignore<IdentityUserLogin<int>>();
            modelBuilder.Ignore<IdentityRoleClaim<int>>();

            //modelBuilder.ApplyConfiguration(new SupplierConfiguration());

            //entity konfigürasyonu için yazdım
          

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); /*(bütün confları tek seferde eklemek için)*/
        }
    

        public async Task SeedAsync()
        {
            //await SeedSuppliersDataAsync();
            //await SeedShippersDataAsync();
        }

        private async Task SeedSuppliersDataAsync()
        {
            #region Elle Veri Ekleme
            //elle veri ekleme
            //List<Supplier> suppliers = new List<Supplier>();

            //suppliers.Add(new Supplier()
            //{
            //    SupplierID = 1,
            //    SupplierName = null,
            //    ContactName = "Şirket",
            //    Address = "sdadasda",
            //    City = "Ankara",
            //    Country = "Türkiye",
            //    Phone = "6465656",
            //    PostalCode = "06546",



            //});

            //await Suppliers.AddRangeAsync(suppliers);
            //await SaveChangesAsync();
            #endregion

            // otomatik json veri seedleme
            var directory = Path.GetDirectoryName(Environment.CurrentDirectory) + "\\Persistance";
            directory = directory.Replace("Presentation", "Infrastructure");
            var path = Path.Combine(directory, "Seeds", "suppliers.json");
            var seedDataJson= File.ReadAllText(path);
            var seedData = JsonSerializer.Deserialize<List<Supplier>>(seedDataJson,new JsonSerializerOptions() { PropertyNameCaseInsensitive= true });

            using (var transaction = await Database.BeginTransactionAsync()) //Identity kolonunu otomatik setlemeyi kaldırma
            {
                //await Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT SUPPLIERS ON");
                await Suppliers.AddRangeAsync(seedData);
                var result = SaveChangesAsync().Result;
                //await Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT SUPPLIERS OFF");
                await transaction.CommitAsync();


            }

  
        }

        private async Task SeedShippersDataAsync()
        {
            var directory = Path.GetDirectoryName(Environment.CurrentDirectory) + "\\Persistance";
            directory = directory.Replace("Presentation", "Infrastructure");
            var path = Path.Combine(directory, "Seeds", "shippers.json");
            var seedDataJson = File.ReadAllText(path);
            var seedData = JsonSerializer.Deserialize<List<Shipper>>(seedDataJson, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            using (var transaction = await Database.BeginTransactionAsync()) //Identity kolonunu otomatik setlemeyi kaldırma
            {
                await Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT SHIPPERS ON");
                await Shippers.AddRangeAsync(seedData);
                var result = SaveChangesAsync().Result;
                await Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT SHIPPERS OFF");
                await transaction.CommitAsync();


            }
        }


    }


}
