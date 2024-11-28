using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal1.Data;
using StudentPortal1.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPortal1.Controllers
{
    public class EngineerController : Controller
    {
        private readonly EngineerDbContext _context;

        public EngineerController(EngineerDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetEngineers()
        {
            var engineers = await _context.Engineers.ToListAsync();
            return Json(engineers); // Return all engineers as JSON
        }

        [HttpGet]
        public async Task<IActionResult> GetAssignedEngineers()
        {
            var assignedEngineers = await _context.DesignEngineers.ToListAsync();
            return Json(assignedEngineers); // Return assigned engineers as JSON
        }

        [HttpPost]
        public async Task<IActionResult> AssignEngineer([FromBody] List<int> engineerIds)
        {
            try
            {
                if (engineerIds == null || !engineerIds.Any())
                {
                    return BadRequest("No engineer IDs were provided.");
                }

                var assignedEngineers = new List<DesignEngineer>();
                var alreadyAssignedEngineers = new List<int>();

                foreach (var engineerId in engineerIds)
                {
                    var engineer = await _context.Engineers.FindAsync(engineerId);

                    if (engineer == null)
                    {
                        continue;
                    }
                    engineer.IsAssigned = false;
                    if (engineer.IsAssigned)
                    {
                        alreadyAssignedEngineers.Add(engineerId);
                        continue;
                    }

                    // Mark as assigned and create a new DesignEngineer entry
                    engineer.IsAssigned = true;
                    var designEngineer = new DesignEngineer
                    {
                        Name = engineer.EName,
                        Department = engineer.EDepartment,
                        AssignedDate = DateTime.Now
                    };

                    _context.DesignEngineers.Add(designEngineer);
                    assignedEngineers.Add(designEngineer);
                }

                await _context.SaveChangesAsync();

                // If some engineers were already assigned, return a partial success response
                if (alreadyAssignedEngineers.Any())
                {
                    return BadRequest(new
                    {
                        message = "Some engineers were already assigned.",
                        alreadyAssigned = alreadyAssignedEngineers
                    });
                }

                return Json(assignedEngineers); // Return assigned engineers
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during assignment: {ex.Message}");
                return StatusCode(500, "Internal server error while assigning engineers.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeassignEngineer([FromBody] List<DesignEngineer> engineers)
        {
            try
            {
                if (engineers == null || !engineers.Any())
                {
                    return BadRequest("No engineers were provided.");
                }

                var deassignedEngineers = new List<DesignEngineer>();

                foreach (var engineerDto in engineers)
                {
                    // Find the engineer in the DesignEngineer table by Name and Department
                    var designEngineer = await _context.DesignEngineers
                                                       .FirstOrDefaultAsync(de => de.Name.ToLower() == engineerDto.Name.ToLower() &&
                                                                                  de.Department.ToLower() == engineerDto.Department.ToLower());

                    if (designEngineer != null)
                    {
                        // Remove from DesignEngineer table (deassign)
                        _context.DesignEngineers.Remove(designEngineer);
                        deassignedEngineers.Add(designEngineer);

                        // Now update the IsAssigned field in the Engineers table to allow re-assignment
                        var engineer = await _context.Engineers
                                                     .FirstOrDefaultAsync(e => e.EName.ToLower() == engineerDto.Name.ToLower() &&
                                                                               e.EDepartment.ToLower() == engineerDto.Department.ToLower());

                        if (engineer != null)
                        {
                            engineer.IsAssigned = false; // Mark as not assigned, allowing reassignment
                        }
                    }
                }

                // Save changes to the database after removal and update
                await _context.SaveChangesAsync();

                // Return the list of deassigned engineers as a response
                return Json(deassignedEngineers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}
