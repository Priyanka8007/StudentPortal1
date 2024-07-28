using StudentPortal1.Models.Domain;
using StudentPortal1.Models.Dtos;

namespace StudentPortal1.Repositories
{
    public interface IStateRepository
    {
        Task<IEnumerable<State>> GetAllStatesAsync();
        Task<IEnumerable<State>> GetStatesByCountryIdAsync(int countryId);
        Task<State> GetStstenameBystateIdAsync(int stateId);
    }
}
