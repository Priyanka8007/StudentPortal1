using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentPortal1.Data;
using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;

namespace StudentPortal1.Repositories
{

    public class StateRepository : IStateRepository
    {
        private readonly EmployeeDbContext _context;

        public StateRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<State>> GetAllStatesAsync()
        {
            return await _context.States.ToListAsync();
        }

        public async Task<IEnumerable<State>> GetStatesByCountryIdAsync(int countryId)
        {
            return await _context.States
                .Where(s => s.CountryId == countryId)
                .ToListAsync();
        }
        public async Task<State> GetStstenameBystateIdAsync(int stateId)
        {
            return await _context.States.FindAsync(stateId);
        }
    }
}