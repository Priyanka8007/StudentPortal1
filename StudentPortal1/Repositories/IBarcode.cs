using StudentPortal1.Models.Domain;

namespace StudentPortal1.Repositories
{
    public interface IBarcode
    {

        Task CreateAsync(Barcode barcode); // Create a new barcode
    }
}
