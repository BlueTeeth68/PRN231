using DataAccess.Enum;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess;

public class AppDbContext : DbContext
{
    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<CarProducer> CarProducers { get; set; }
    public virtual DbSet<Car> Cars { get; set; }
    public virtual DbSet<CarRental> CarRentals { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarRental>()
            .HasIndex(nameof(CarRental.CarId), nameof(CarRental.CustomerId),
                nameof(CarRental.PickupDate), nameof(CarRental.ReturnDate))
            .IsUnique();

        modelBuilder.Entity<Review>()
            .HasIndex(nameof(Review.CustomerId),
                nameof(Review.CarId))
            .IsUnique();
        SeedCustomers(modelBuilder);
        SeedCarProducers(modelBuilder);
        SeedCars(modelBuilder);
        SeedCarRentals(modelBuilder);
        SeedReviews(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }
    }

    private static string GetConnectionString()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        IConfiguration configuration = builder.Build();
        return configuration.GetConnectionString("DefaultConnection");

        // string projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\DataAccess"));
        // IConfigurationRoot configuration = new ConfigurationBuilder()
        //     .SetBasePath(projectDirectory)
        //     .AddJsonFile("appsettings.json")
        //     .Build();
        //
        // return configuration.GetConnectionString("DefaultConnection");
    }

    private static void SeedCustomers(ModelBuilder modelBuilder)
    {
        var customers = new[]
        {
            new Customer
            {
                Id = 1,
                CustomerName = "John Doe",
                Mobile = "1234567890",
                Birthday = new DateTime(1990, 1, 1),
                IdentityCard = "ABC123",
                LicenceNumber = "XYZ987",
                LicenceDate = new DateTime(2020, 1, 1),
                Email = "john.doe@example.com",
                Password = "123456"
            },
            new Customer
            {
                Id = 2,
                CustomerName = "Jane Smith",
                Mobile = "0987654321",
                Birthday = new DateTime(1985, 5, 10),
                IdentityCard = "DEF456",
                LicenceNumber = "PQR654",
                LicenceDate = new DateTime(2019, 6, 15),
                Email = "jane.smith@example.com",
                Password = "123456"
            },
            new Customer
            {
                Id = 3,
                CustomerName = "Alice Johnson",
                Mobile = "9876543210",
                Birthday = new DateTime(1992, 3, 20),
                IdentityCard = "GHI789",
                LicenceNumber = "LMN321",
                LicenceDate = new DateTime(2022, 2, 10),
                Email = "alice.johnson@example.com",
                Password = "123456"
            },
            new Customer
            {
                Id = 4,
                CustomerName = "Bob Williams",
                Mobile = "4567890123",
                Birthday = new DateTime(1988, 10, 5),
                IdentityCard = "JKL987",
                LicenceNumber = "STU456",
                LicenceDate = new DateTime(2021, 4, 5),
                Email = "bob.williams@example.com",
                Password = "123456"
            },
            new Customer
            {
                Id = 5,
                CustomerName = "Eva Brown",
                Mobile = "9012345678",
                Birthday = new DateTime(1995, 4, 15),
                IdentityCard = "MNO654",
                LicenceNumber = "VWX789",
                LicenceDate = new DateTime(2023, 8, 20),
                Email = "eva.brown@example.com",
                Password = "123456"
            },
            new Customer
            {
                Id = 6,
                CustomerName = "David Wilson",
                Mobile = "6543210987",
                Birthday = new DateTime(1983, 8, 25),
                IdentityCard = "PQR321",
                LicenceNumber = "YZA987",
                LicenceDate = new DateTime(2020, 11, 30),
                Email = "david.wilson@example.com",
                Password = "123456"
            },
            new Customer
            {
                Id = 7,
                CustomerName = "Sophia Davis",
                Mobile = "8901234567",
                Birthday = new DateTime(1991, 6, 5),
                IdentityCard = "BCD789",
                LicenceNumber = "EFG654",
                LicenceDate = new DateTime(2021, 9, 9),
                Email = "sophia.davis@example.com",
                Password = "123456"
            },
            new Customer
            {
                Id = 8,
                CustomerName = "Michael Johnson",
                Mobile = "3210987654",
                Birthday = new DateTime(1986, 2, 12),
                IdentityCard = "HIJ987",
                LicenceNumber = "KLM321",
                LicenceDate = new DateTime(2018, 12, 25),
                Email = "michael.johnson@example.com",
                Password = "123456"
            },
            new Customer
            {
                Id = 9,
                CustomerName = "Olivia Taylor",
                Mobile = "7654321098",
                Birthday = new DateTime(1993, 9, 8),
                IdentityCard = "NOP654",
                LicenceNumber = "QRS987",
                LicenceDate = new DateTime(2021, 7, 3),
                Email = "olivia.taylor@example.com",
                Password = "123456"
            },
            new Customer
            {
                Id = 10,
                CustomerName = "William Anderson",
                Mobile = "2345678901",
                Birthday = new DateTime(1987, 12, 3),
                IdentityCard = "RST321",
                LicenceNumber = "UVW987",
                LicenceDate = new DateTime(2022, 5, 3),
                Email = "william.anderson@example.com",
                Password = "123456"
            }
        };

        modelBuilder.Entity<Customer>().HasData(customers);
    }

    private static void SeedCarProducers(ModelBuilder modelBuilder)
    {
        var carProducers = new[]
        {
            new CarProducer
            {
                Id = 1,
                ProducerName = "Toyota",
                Address = "123 Main Street",
                Country = "Japan"
            },
            new CarProducer
            {
                Id = 2,
                ProducerName = "Ferrari",
                Address = "456 Park Avenue",
                Country = "Italy"
            },
            new CarProducer
            {
                Id = 3,
                ProducerName = "Mercedes-Benz",
                Address = "789 Broadway",
                Country = "Germany"
            },
            new CarProducer
            {
                Id = 4,
                ProducerName = "Tesla",
                Address = "321 Elm Street",
                Country = "United States"
            }
        };

        modelBuilder.Entity<CarProducer>().HasData(carProducers);
    }

    private static void SeedCars(ModelBuilder modelBuilder)
    {
        var cars = new[]
        {
            new Car
            {
                Id = 1,
                CarName = "Toyota Camry",
                CarModelYear = 2020,
                Color = "Silver",
                Capacity = 5,
                Description = "A comfortable sedan with advanced features",
                ImportDate = new DateTime(2022, 3, 10),
                ProducerId = 1,
                RentPrice = 100.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 2,
                CarName = "Ferrari 488 GTB",
                CarModelYear = 2021,
                Color = "Red",
                Capacity = 2,
                Description = "A powerful and luxurious sports car",
                ImportDate = new DateTime(2022, 5, 15),
                ProducerId = 2,
                RentPrice = 500.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 3,
                CarName = "Mercedes-Benz E-Class",
                CarModelYear = 2019,
                Color = "Black",
                Capacity = 5,
                Description = "A luxurious and elegant sedan",
                ImportDate = new DateTime(2022, 6, 20),
                ProducerId = 3,
                RentPrice = 150.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 4,
                CarName = "Tesla Model S",
                CarModelYear = 2022,
                Color = "White",
                Capacity = 5,
                Description = "An electric car with cutting-edge technology",
                ImportDate = new DateTime(2022, 8, 5),
                ProducerId = 4,
                RentPrice = 200.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 5,
                CarName = "Toyota RAV4",
                CarModelYear = 2021,
                Color = "Blue",
                Capacity = 5,
                Description = "A versatile and spacious SUV",
                ImportDate = new DateTime(2022, 9, 12),
                ProducerId = 1,
                RentPrice = 120.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 6,
                CarName = "Ferrari 812 Superfast",
                CarModelYear = 2023,
                Color = "Yellow",
                Capacity = 2,
                Description = "An incredibly fast and powerful supercar",
                ImportDate = new DateTime(2022, 10, 8),
                ProducerId = 2,
                RentPrice = 600.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 7,
                CarName = "Mercedes-Benz GLE",
                CarModelYear = 2020,
                Color = "Silver",
                Capacity = 5,
                Description = "A stylish and comfortable SUV",
                ImportDate = new DateTime(2022, 11, 15),
                ProducerId = 3,
                RentPrice = 180.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 8,
                CarName = "Tesla Model 3",
                CarModelYear = 2022,
                Color = "Red",
                Capacity = 5,
                Description = "A popular electric car with great range",
                ImportDate = new DateTime(2022, 12, 20),
                ProducerId = 4,
                RentPrice = 180.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 9,
                CarName = "Toyota Highlander",
                CarModelYear = 2021,
                Color = "Gray",
                Capacity = 7,
                Description = "A spacious and reliable SUV",
                ImportDate = new DateTime(2023, 1, 5),
                ProducerId = 1,
                RentPrice = 140.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 10,
                CarName = "Ferrari F8 Tributo",
                CarModelYear = 2023,
                Color = "Black",
                Capacity = 2,
                Description = "A stunning and high-performance sports car",
                ImportDate = new DateTime(2023, 2, 10),
                ProducerId = 2,
                RentPrice = 550.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 11,
                CarName = "Mercedes-Benz S-Class",
                CarModelYear = 2022,
                Color = "White",
                Capacity = 5,
                Description = "A flagship luxury sedan with advanced technology",
                ImportDate = new DateTime(2023, 3, 15),
                ProducerId = 3,
                RentPrice = 200.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 12,
                CarName = "Tesla Model X",
                CarModelYear = 2022,
                Color = "Blue",
                Capacity = 7,
                Description = "An electric SUV with falcon-wing doors",
                ImportDate = new DateTime(2023, 4, 20),
                ProducerId = 4,
                RentPrice = 220.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 13,
                CarName = "Toyota Land Cruiser",
                CarModelYear = 2022,
                Color = "Black",
                Capacity = 8,
                Description = "A rugged and capable off-road SUV",
                ImportDate = new DateTime(2024, 1, 1),
                ProducerId = 1,
                RentPrice = 180.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 14,
                CarName = "Ferrari SF90 Stradale",
                CarModelYear = 2023,
                Color = "Red",
                Capacity = 2,
                Description = "A high-performance hybrid supercar",
                ImportDate = new DateTime(2024, 2, 6),
                ProducerId = 2,
                RentPrice = 700.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 15,
                CarName = "Mercedes-Benz G-Class",
                CarModelYear = 2021,
                Color = "White",
                Capacity = 5,
                Description = "An iconic and luxurious SUV",
                ImportDate = new DateTime(2024, 3, 12),
                ProducerId = 3,
                RentPrice = 250.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 16,
                CarName = "Tesla Roadster",
                CarModelYear = 2023,
                Color = "Red",
                Capacity = 2,
                Description = "An all-electric sports car with incredible speed",
                ImportDate = new DateTime(2024, 4, 18),
                ProducerId = 4,
                RentPrice = 800.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 17,
                CarName = "Toyota Camry",
                CarModelYear = 2020,
                Color = "Gray",
                Capacity = 5,
                Description = "A reliable and fuel-efficient sedan",
                ImportDate = new DateTime(2024, 5, 24),
                ProducerId = 1,
                RentPrice = 110.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 18,
                CarName = "Ferrari 488 Pista",
                CarModelYear = 2023,
                Color = "Yellow",
                Capacity = 2,
                Description = "A track-focused and high-performance supercar",
                ImportDate = new DateTime(2024, 6, 30),
                ProducerId = 2,
                RentPrice = 750.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 19,
                CarName = "Mercedes-Benz CLA",
                CarModelYear = 2022,
                Color = "Black",
                Capacity = 5,
                Description = "A compact and stylish luxury sedan",
                ImportDate = new DateTime(2024, 7, 6),
                ProducerId = 3,
                RentPrice = 140.00m,
                Status = CarStatus.Available
            },
            new Car
            {
                Id = 20,
                CarName = "Tesla Model S Plaid",
                CarModelYear = 2023,
                Color = "White",
                Capacity = 5,
                Description = "An ultra-fast and high-performance electric car",
                ImportDate = new DateTime(2024, 8, 12),
                ProducerId = 4,
                RentPrice = 350.00m,
                Status = CarStatus.Available
            },
        };

        modelBuilder.Entity<Car>().HasData(cars);
    }

    private static void SeedCarRentals(ModelBuilder modelBuilder)
    {
        var carRentals = new[]
        {
            new CarRental
            {
                Id = 1,
                CustomerId = 1,
                CarId = 12,
                PickupDate = new DateTime(2023, 10, 1),
                ReturnDate = new DateTime(2023, 10, 5),
                RentPrice = 480.00m,
                Status = RentingStatus.Success
            },
            new CarRental
            {
                Id = 2,
                CustomerId = 2,
                CarId = 17,
                PickupDate = new DateTime(2023, 10, 3),
                ReturnDate = new DateTime(2023, 10, 7),
                RentPrice = 440.00m,
                Status = RentingStatus.Success
            },
            new CarRental
            {
                Id = 3,
                CustomerId = 3,
                CarId = 13,
                PickupDate = new DateTime(2023, 10, 2),
                ReturnDate = new DateTime(2023, 10, 6),
                RentPrice = 640.00m,
                Status = RentingStatus.Success
            },
            new CarRental
            {
                Id = 4,
                CustomerId = 4,
                CarId = 19,
                PickupDate = new DateTime(2023, 10, 4),
                ReturnDate = new DateTime(2023, 10, 9),
                RentPrice = 900.00m,
                Status = RentingStatus.Success
            },
            new CarRental
            {
                Id = 5,
                CustomerId = 1,
                CarId = 14,
                PickupDate = new DateTime(2023, 10, 6),
                ReturnDate = new DateTime(2023, 10, 8),
                RentPrice = 300.00m,
                Status = RentingStatus.Success
            },
            new CarRental
            {
                Id = 6,
                CustomerId = 3,
                CarId = 11,
                PickupDate = new DateTime(2023, 10, 7),
                ReturnDate = new DateTime(2023, 10, 10),
                RentPrice = 2700.00m,
                Status = RentingStatus.Success
            },
            new CarRental
            {
                Id = 7,
                CustomerId = 2,
                CarId = 19,
                PickupDate = new DateTime(2023, 10, 8),
                ReturnDate = new DateTime(2023, 10, 12),
                RentPrice = 560.00m,
                Status = RentingStatus.Success
            },
            new CarRental
            {
                Id = 8,
                CustomerId = 4,
                CarId = 20,
                PickupDate = new DateTime(2023, 10, 10),
                ReturnDate = new DateTime(2023, 10, 15),
                RentPrice = 2000.00m,
                Status = RentingStatus.Success
            }
        };
        modelBuilder.Entity<CarRental>().HasData(carRentals);
    }

    private static void SeedReviews(ModelBuilder modelBuilder)
    {
        var reviews = new[]
        {
            new Review
            {
                Id = 1,
                CustomerId = 1,
                CarId = 12,
                ReviewStar = 4,
                Comment = "The car was clean and comfortable. The overall experience was great."
            },
            new Review
            {
                Id = 2,
                CustomerId = 2,
                CarId = 17,
                ReviewStar = 5,
                Comment = "I loved the car! It was in excellent condition, and the service provided was outstanding."
            },
            new Review
            {
                Id = 3,
                CustomerId = 3,
                CarId = 13,
                ReviewStar = 3,
                Comment = "The car was average. It could have been cleaner, and the pickup process was a bit slow."
            },
            new Review
            {
                Id = 4,
                CustomerId = 4,
                CarId = 19,
                ReviewStar = 4,
                Comment = "The car was reliable and comfortable. Overall, a good experience."
            },
            new Review
            {
                Id = 5,
                CustomerId = 1,
                CarId = 14,
                ReviewStar = 5,
                Comment = "The car exceeded my expectations! It was clean, fuel-efficient, and perfect for my trip."
            },
            new Review
            {
                Id = 6,
                CustomerId = 3,
                CarId = 11,
                ReviewStar = 2,
                Comment =
                    "I was disappointed with the car's condition. It had several maintenance issues and was not clean."
            }
        };
        modelBuilder.Entity<Review>().HasData(reviews);
    }
}