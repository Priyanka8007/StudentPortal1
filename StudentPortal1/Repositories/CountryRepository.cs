using AutoMapper;
using StudentPortal1.Data;
using Microsoft.EntityFrameworkCore;
using StudentPortal1.Models.Dtos;
using StudentPortal1.Models.Domain;

namespace StudentPortal1.Repositories
{

    public class CountryRepository : ICountryRepository
    {
        private readonly EmployeeDbContext _context;

        public CountryRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        
         
        
        
        
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> GetCountryNameByCountryIdAsync(int id)
        {
            return await _context.Countries.FindAsync(id);
        }
    }
}
