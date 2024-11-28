using Microsoft.AspNetCore.Mvc;
using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;
using StudentPortal1.Repositories;
namespace StudentPortal1.Controllers
{
    public class BarcodeController : Controller
    {
        private readonly IBarcode _barcodeRepository;

        public BarcodeController(IBarcode barcodeRepository)
        {
            _barcodeRepository = barcodeRepository;
        }

        // GET: Barcodes
        public async Task<IActionResult> Index()
        {
            // var barcodes = await _barcodeRepository.GetAllAsync();
            return View();
        }

        // GET: Barcodes/Create
        public IActionResult CreateBarcode()
        {
            return View();
        }

        // POST: Barcodes/CreateBarcode
        // POST: Barcodes/CreateBarcode
        [HttpPost]
        public async Task<IActionResult> CreateBarcode([FromBody] BarcodeDto barcodeDto)
        {
            if (ModelState.IsValid)
            {
                // Map BarcodeDto to Barcode model
                var barcode = new Barcode
                {
                    Name = barcodeDto.Name,
                    Age = barcodeDto.Age,
                    Barcd = barcodeDto.Barcd,
                    Address = barcodeDto.Address,
                    PhoneNumber = barcodeDto.PhoneNumber
                };

                await _barcodeRepository.CreateAsync(barcode);

                // If needed, return the created barcode or a success view
                return RedirectToAction(nameof(CreateBarcode)); // Redirect to Index after creation
            }

            // If we reach here, something failed, redisplay form with errors
            return View(barcodeDto); // Return the same view with validation errors
        }

        // GET: Barcodes/Edit/5

    }
}