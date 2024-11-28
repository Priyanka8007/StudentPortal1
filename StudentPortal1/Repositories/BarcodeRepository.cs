using Microsoft.EntityFrameworkCore;
using StudentPortal1.Data;
using StudentPortal1.Models.Domain;
using System.Threading.Tasks;
using static StudentPortal1.Repositories.IBarcode;

namespace StudentPortal1.Repositories
{
    public class BarcodeRepository : IBarcode
    {
        private readonly BarcodeDbContext _context;

        public BarcodeRepository(BarcodeDbContext context)
        {
            _context = context;
        }

        // Implement the method to get a Barcode by its barcode value


        public async Task CreateAsync(Barcode barcode)
        {
            // Add the new barcode to the context
            _context.barcodes.Add(barcode);
            await _context.SaveChangesAsync(); // Save changes to the database
        }
    }
}
