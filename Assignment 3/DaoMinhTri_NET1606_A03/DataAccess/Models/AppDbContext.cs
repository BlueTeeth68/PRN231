using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CarInformation> CarInformations { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public virtual DbSet<RentingDetail> RentingDetails { get; set; } = null!;
        public virtual DbSet<RentingTransaction> RentingTransactions { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }

        public string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            return configuration.GetConnectionString("DefaultConnection");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarInformation>(entity =>
            {
                entity.HasKey(e => e.CarId);

                entity.ToTable("CarInformation");

                entity.Property(e => e.CarId).HasColumnName("CarID");

                entity.Property(e => e.CarDescription).HasMaxLength(220);

                entity.Property(e => e.CarName).HasMaxLength(50);

                entity.Property(e => e.CarRentingPricePerDay).HasColumnType("money");

                entity.Property(e => e.FuelType).HasMaxLength(20);

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.CarInformations)
                    .HasForeignKey(d => d.ManufacturerId)
                    .HasConstraintName("FK_CarInformation_Manufacturer");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.CarInformations)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_CarInformation_Supplier");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasIndex(e => e.Email, "UQ__Customer__A9D105342217C017")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerBirthday).HasColumnType("date");

                entity.Property(e => e.CustomerName).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Telephone).HasMaxLength(12);
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.ToTable("Manufacturer");

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.ManufacturerCountry)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ManufacturerName).HasMaxLength(50);
            });

            modelBuilder.Entity<RentingDetail>(entity =>
            {
                entity.HasKey(e => new { e.RentingTransactionId, e.CarId });

                entity.ToTable("RentingDetail");

                entity.Property(e => e.RentingTransactionId).HasColumnName("RentingTransactionID");

                entity.Property(e => e.CarId).HasColumnName("CarID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.RentingDetails)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK_RentingDetail_CarInformation");

                entity.HasOne(d => d.RentingTransaction)
                    .WithMany(p => p.RentingDetails)
                    .HasForeignKey(d => d.RentingTransactionId)
                    .HasConstraintName("FK_RentingDetail_RentingTransaction");
            });

            modelBuilder.Entity<RentingTransaction>(entity =>
            {
                entity.HasKey(e => e.RentingTransationId);

                entity.ToTable("RentingTransaction");

                entity.Property(e => e.RentingTransationId)
                    .ValueGeneratedNever()
                    .HasColumnName("RentingTransationID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.RentingDate).HasColumnType("date");

                entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.RentingTransactions)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_RentingTransaction_Customer");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.SupplierAddress).HasMaxLength(80);

                entity.Property(e => e.SupplierDescription).HasMaxLength(250);

                entity.Property(e => e.SupplierName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
