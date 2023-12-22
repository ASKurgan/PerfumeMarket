using Microsoft.EntityFrameworkCore;
using PerfumeMarket.Domain.Entity;
using PerfumeMarket.Domain.Enum;
using PerfumeMarket.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = PerfumeShop.db");
        }

        public DbSet<Perfume> Perfumes { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.Id);

                builder.HasData(new User[]
                {
                    new User()
                    {
                        Id = 1,
                        Name = "Admin",
                        Password = HashPasswordHelper.HashPassowrd("123456"),
                        Role = Role.Admin
                    },
                    new User()
                    {
                        Id = 2,
                        Name = "Moderator",
                        Password = HashPasswordHelper.HashPassowrd("654321"),
                        Role = Role.Moderator
                    }
                });

                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

                builder.HasOne(x => x.Profile)
                    .WithOne(x => x.User)
                    .HasPrincipalKey<User>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(x => x.Basket)
                    .WithOne(x => x.User)
                    .HasPrincipalKey<User>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Perfume>(builder =>
            {
                builder.ToTable("Perfumes").HasKey(x => x.Id);

                builder.HasData(new Perfume
                {
                    Id = 1,
                    Name = "Versace Man Eau Fraiche",
                    Description = new ("Аромат для мужчин, он принадлежит к группе древесные водяные. Выпущен в 2006 году." +
                    "Парфюмер: Olivier Cresp. Верхние ноты: Лимон, Бергамот, Карамбола, Кардамон и Розовое дерево; " +
                    "средние ноты: Кедр, Тархун, Шалфей и Перец; базовые ноты: Мускус, Древесные ноты, Шафран, Амбра и " +
                    "Сикомор (американский платан)."),
                    DateCreate = DateTime.Now,
                    GroupOfFragrances = "Aquatica",
                    Brand = "Versace",
                    Avatar = null,
                    TypePerfume = TypePerfume.EDT
                });
            });

            modelBuilder.Entity<Profile>(builder =>
            {
                builder.ToTable("Profiles").HasKey(x => x.Id);

                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Age);
                builder.Property(x => x.Address).HasMaxLength(200).IsRequired(false);

                builder.HasData(new Profile()
                {
                    Id = 1,
                    UserId = 1
                });
            });

            modelBuilder.Entity<Basket>(builder =>
            {
                builder.ToTable("Baskets").HasKey(x => x.Id);

                builder.HasData(new Basket()
                {
                    Id = 1,
                    UserId = 1
                });
            });

            modelBuilder.Entity<Order>(builder =>
            {
                builder.ToTable("Orders").HasKey(x => x.Id);

                builder.HasOne(r => r.Basket).WithMany(t => t.Orders)
                    .HasForeignKey(r => r.BasketId);
            });
        }
    }




}
        

    

