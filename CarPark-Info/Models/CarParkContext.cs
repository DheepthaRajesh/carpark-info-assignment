using Microsoft.EntityFrameworkCore;

namespace CarPark_Info.Models
{
public class CarParkContext : DbContext
{
    public CarParkContext(DbContextOptions<CarParkContext> options)
        : base(options)
    {
    }

    public DbSet<CarPark> CarParks { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Favourite> Favourites { get; set; } = null!;

    // We define the many to many relationship using the join entity and define the primary and foreign keys of the entities:
    // We will also explicitly define the primary key of CarPark and foreign keys of Favourite because the key car_park_no does not follow EF conventions:
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<CarPark>()
            .HasKey(c => c.car_park_no);
        
        modelBuilder.Entity<Favourite>()
            .HasKey(c => new { c.UserId, c.car_park_no });
        
        modelBuilder.Entity<CarPark>()
            .HasMany(c => c.Users)
            .WithMany(c => c.CarParks)
            .UsingEntity<Favourite>(
                l => l.HasOne<User>(c => c.User).WithMany(c => c.Favourites).HasForeignKey(c => c.UserId),
                r => r.HasOne<CarPark>(c => c.CarPark).WithMany(c => c.Favourites).HasForeignKey(c => c.car_park_no));

        // We populate the Users table with 3 test users to test the API functionality of a user favouriting a carpark:
        modelBuilder.Entity<User>().HasData(
        new User
        {
            UserId = "a1",
            username = "Test User1",
            email = "testuser1@example.com"
        },
        new User
        {
            UserId = "a2",
            username = "Test User2",
            email = "testuser2@example.com"
        },
        new User
        {
            UserId = "a3",
            username = "Test User3",
            email = "testuser3@example.com"
        });
        }

}
}