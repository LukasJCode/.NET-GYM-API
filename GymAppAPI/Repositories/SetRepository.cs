using GymAppAPI.Data;
using GymAppAPI.Interfaces;
using GymAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GymAppAPI.Repositories
{
    public class SetRepository : ISetRepository
    {
        private readonly AppDbContext _db;
        public SetRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddSetAsync(Set set)
        {
            await _db.Sets.AddAsync(set);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteSetAsync(int setId)
        {
            var set = await _db.Sets.Where(s => s.Id == setId).FirstOrDefaultAsync();
            _db.Sets.Remove(set);

            await _db.SaveChangesAsync();
        }

        public async Task<ICollection<Set>> GetAllSetsAsync()
        {
            return await _db.Sets.OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<Set> GetSetByIdAsync(int setId)
        {
            return await _db.Sets.FirstOrDefaultAsync(s => s.Id == setId);
        }

        public async Task<bool> SetExistsAsync(int setId)
        {
            if (await _db.Sets.AnyAsync(s => s.Id == setId))
                return true;
            return false;
        }

        public async Task UpdateSetAsync(Set updatedSet)
        {
            _db.Sets.Update(updatedSet);
            await _db.SaveChangesAsync();
        }
    }
}
