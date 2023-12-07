using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MangaStore.Data
{
    public class MangaStoreDbContext : DbContext
    {
        public MangaStoreDbContext(DbContextOptions<MangaStoreDbContext> options) : base(options) {}

        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<GenreType> GenreType {get; set;}
        public DbSet<StoreInventory> StoreInventories {get; set;}
        public DbSet<Image> Images {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Manga>()
        .HasOne(m => m.Image)
        .WithMany()
        .HasForeignKey(m => m.ImageId)
        .OnDelete(DeleteBehavior.Cascade);
}

    }
}
