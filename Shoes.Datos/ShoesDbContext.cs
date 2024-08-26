using Microsoft.EntityFrameworkCore;
using Shoes.Entidades;

namespace Shoes.Datos
{
    public class ShoesDbContext:DbContext 
    {
        public ShoesDbContext(DbContextOptions<ShoesDbContext> options)
        : base(options)
        {
        }
        public ShoesDbContext()
        {
                
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ShoeSize> ShoeSizes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog = ShoesTP1.2024; Trusted_Connection = true; TrustServerCertificate = true; ");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Color>().HasData(
                new Color
                {
                    ColorId = 1,
                    ColorName = "Azul"
                });

            var colorLList = new List<Color>()
            {
                new Color {ColorId=2, ColorName="Blanco"},
                new Color {ColorId=3, ColorName="Negro"},
                new Color {ColorId=4, ColorName="Rojo"},
            };
            modelBuilder.Entity<Color>().HasData(colorLList);

            modelBuilder.Entity<Shoe>(entity => {
                entity.Property(s => s.Price).HasColumnType("Decimal(10,2)");
                entity.HasIndex(s => s.Description).IsUnique();

            });

            modelBuilder.Entity<Color>(entity =>
            {
            entity.HasIndex(c => c.ColorName).IsUnique();
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasIndex(b => b.BrandName).IsUnique();
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasIndex(g => g.GnereName).IsUnique();
            });

            modelBuilder.Entity<Sport>(entity =>
            {
                entity.HasIndex(s => s.SportName).IsUnique();
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.Property(s => s.SizeNumber).HasColumnType("decimal(3,1)");
            });


			modelBuilder.Entity<ShoeSize>()
			.HasKey(ss => ss.ShoeSizeId);

			modelBuilder.Entity<ShoeSize>()
				.HasIndex(ss => new { ss.ShoeId, ss.SizeId })  
				.IsUnique();

			modelBuilder.Entity<ShoeSize>()
				.HasOne(ss => ss.ShoeN)
				.WithMany(s => s.ShoesSizes)
				.HasForeignKey(ss => ss.ShoeId);

			modelBuilder.Entity<ShoeSize>()
				.HasOne(ss => ss.SizeN)
				.WithMany(s => s.ShoesSizes)
				.HasForeignKey(ss => ss.SizeId);


			


		}
    }
}
