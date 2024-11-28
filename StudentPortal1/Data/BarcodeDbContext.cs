using Microsoft.EntityFrameworkCore;
using StudentPortal1.Models.Domain;

namespace StudentPortal1.Data
{
    public class BarcodeDbContext : DbContext
    {
        public BarcodeDbContext(DbContextOptions<BarcodeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Barcode> barcodes { get; set; }  // Creates a table for persons
    }
}