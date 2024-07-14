using NarutoDatabookApp.Data;
using NarutoDatabookApp.Interfaces;
using NarutoDatabookApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace NarutoDatabookApp.Repository
{
    public class VillageRepository : IVillageInterface
    {
        private readonly DataContext _context;

        public VillageRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateVillage(Village village)
        {
            _context.Add(village);
            return Save();
        }

        public bool DeleteVillage(Village village)
        {
            _context.Remove(village);
            return Save();
        }

        public Village GetVillage(int villageId)
        {
            return _context.Villages.Where(v => v.VillageId == villageId).FirstOrDefault();
        }

        public Village GetVillage(string name)
        {
            return _context.Villages.Where(v => v.Name == name).FirstOrDefault();
        }

        public ICollection<Village> GetVillages()
        {
            return _context.Villages.OrderBy(v => v.VillageId).ToList();
        }

        public bool VillageExists(int villageId)
        {
            return _context.Villages.Any(v => v.VillageId == villageId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateVillage(Village village)
        {
            _context.Update(village);
            return Save();
        }
    }
}
