using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;

namespace StudentPortal1.Repositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAllCitiesAsync();
       // Task<IEnumerable<City>> GetCitiesByCountryIdAsync(int countryId);
        Task<IEnumerable<City>> GetCitiesByStateIdAsync(int stateId);
        Task<City> GetByIdAsync(int cityId);
    }
}
