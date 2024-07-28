using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentPortal1.Data;
using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;

namespace StudentPortal1.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly EmployeeDbContext _context;

        public CityRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        //public async Task<IEnumerable<City>> GetCitiesByCountryIdAsync(int countryId)
        //{
        //    return await _context.Cities
        //        .Include(c => c.State)
        //        .Where(c => c.State.CountryId == countryId)
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<City>> GetCitiesByStateIdAsync(int stateId)
        {
            return await _context.Cities
                .Where(c => c.StateId == stateId)
                .ToListAsync();
        }

        public async Task<City> GetByIdAsync(int cityId)
        {
            return await _context.Cities.FindAsync(cityId);
        }
    }
}