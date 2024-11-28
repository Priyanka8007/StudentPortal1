using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal1.Data;
using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;

namespace StudentPortal1.Controllers
{
    public class ChallanController : Controller
    {
        private readonly ChallanDbContext _context;

        public ChallanController(ChallanDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GridChallan(int challanno)
        {
           // challanno = 1001;
            var results = _context.ChallanSplitResults
          .FromSqlRaw("EXEC QPK01ChallanSplit @challanno = {0}", challanno)
          .ToList();

            return Json(results); // Return the results as JSON
        }

        [HttpPost]
        public IActionResult SplitQuantity([FromBody] SplitQuantityRequest request)
        {
            if (request.Rows == null || request.Rows.Count == 0 || request.SplitQuantity <= 0)
            {
                return BadRequest("Invalid input data.");
            }

            foreach (var row in request.Rows)
            {
                row.labelQty -= request.SplitQuantity;
               // row.LabelQty -= request.SplitQuantity;
            }

            return Json(new
            {
              updatesplit= request.SplitQuantity,
              updatedRows = request.Rows  // Include updated quantities in response
            });
        }

        [HttpPost]
        public IActionResult SaveChallanSplitData([FromBody] List<LabelTran> gridData)
        {
            if (gridData == null || !gridData.Any())
            {
                return BadRequest("No data to save.");
            }
            Guid newGuid = Guid.NewGuid();
            var GeneratedGuid = newGuid.ToString();
            try
            {
                foreach (var data in gridData)
                {
                    var newLabelran = new LabelTran
                    {
                        ChallanNo = data.ChallanNo,
                        ChallanSrNo = data.ChallanSrNo,
                        PONo = data.PONo,
                        POSrNo = data.POSrNo,
                        ItemCode = data.ItemCode,
                        Qty = data.Qty,
                        LabelNo = data.LabelNo,
                        SessionId = GeneratedGuid
                    };

                    _context.LabelTrans.Add(newLabelran);
                }

                _context.SaveChanges();
                return Json("Data saved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetLabelTranData()
        {
            var data = await _context.LabelTrans
                .Select(x => new
                {
                    x.LabelSrNo, // Replace with actual column
                    x.PONo,      // Replace with actual column
                    x.ItemCode,  // Replace with actual column
                    x.Qty   // Replace with actual column
                })
                .ToListAsync();

            return Json(data);
        }
        [HttpGet]
        public async Task<IActionResult> GetChartData()
        {
            var data = await _context.LabelTrans
                .Select(x => new
                {
                    x.LabelSrNo, // Replace with actual column
                    x.Qty   // Replace with actual column
                })
                .ToListAsync();

            return Json(data);
        }







    }
}
