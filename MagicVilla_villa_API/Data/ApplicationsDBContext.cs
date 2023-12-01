using MagicVilla_villa_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_villa_API.Data
{
    public class ApplicationsDBContext : DbContext
    {
        public ApplicationsDBContext(DbContextOptions<ApplicationsDBContext> options) : base(options)
        {
            // before connected EF need to install below Packges 
            // Microsoft.EntityFrameworkCore.sqlserver
            //Microsoft.EntityFrameworkCore.Tools
            //Microsoft.Extension.Configuration

        }
        public DbSet<Villa> villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    id = 1,
                    name = "Pool view",
                    detalis = "Pool View Coool palce",
                    rate = 100000,
                    sqft = 1300,
                    imgurl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Funsplash.com%2Fs%2Fphotos%2Fluxury-villa&psig=AOvVaw3-aHod71srN4UaKelBLHRF&ust=1701417603723000&source=images&cd=vfe&ved=0CBIQjRxqFwoTCMiL3tGg64IDFQAAAAAdAAAAABAE",
                    createdate = DateTime.Now,
                    updateddate = DateTime.Now,
                }
                );
        }
    }
}
