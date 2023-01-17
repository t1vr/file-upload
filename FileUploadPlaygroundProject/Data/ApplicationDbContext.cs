using FileUploadPlaygroundProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FileUploadPlaygroundProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ImageData> ImageData { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
