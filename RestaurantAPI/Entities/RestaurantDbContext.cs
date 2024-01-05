using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Entities
{
    public class RestaurantDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=RestaurantDb;Trusted_Connection=True;";

        public DbSet<Restaurant> Restaurants { get; set;}
        public DbSet<Address> Address { get; set;}
        public DbSet<Dish> Dishes { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(24);

            modelBuilder.Entity<Dish>()
                .Property(d => d.Name)
                .HasMaxLength(24)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property("Street")
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Address>()
                .Property("City")
                .IsRequired()
                .HasMaxLength(25);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(_connectionString);
            //}
        }
    }
}
