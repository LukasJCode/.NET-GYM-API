using GymAppAPI.Models;
using System.Collections.ObjectModel;

namespace GymAppAPI.Interfaces
{
    public interface ISetRepository
    {
        Task<ICollection<Set>> GetAllSetsAsync();
        Task<Set> GetSetByIdAsync(int setId);
        Task AddSetAsync(Set set);
        Task UpdateSetAsync(Set updatedSet);
        Task DeleteSetAsync(int setId);
        Task<bool> SetExistsAsync(int setId);
    }
}
