using IS_Projekt.Extensions;
using IS_Projekt.Models;
using Microsoft.EntityFrameworkCore;

namespace IS_Projekt.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<InternetUse> InternetUseData { get; set; }
        public DbSet<ECommerce> ECommerceData { get; set; }
        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<YearModel> Years { get; set; }
        // tutaj pozniej inne dbset dla danych

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var yearList = new List<YearModel>();
            var countryList = new List<CountryModel>();
            int idCounter = 1;
            foreach (var country in CountryCodes.Countries)
            {
                countryList.Add(new CountryModel
                    { Id = idCounter, CountryCode = country.Key, CountryName = country.Value });
                idCounter++;
            }
            for (var i = 2002; i <= 2022; i++)
            { 
                yearList.Add(new YearModel { Year = i, Id = i-2001 });
            }

            // For example, to add a unique constraint on the Username property of User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<CountryModel>()
                .HasData(countryList);

            modelBuilder.Entity<YearModel>()
                .HasData(yearList);
        }

    }
}
