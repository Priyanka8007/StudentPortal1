using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;

namespace StudentPortal1.Repositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
        Task<Country> GetCountryNameByCountryIdAsync(int id);
    }
}
