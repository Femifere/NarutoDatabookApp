using NarutoDatabookApp.Data;
using NarutoDatabookApp.Interfaces;
using NarutoDatabookApp.Models;
using System.Linq;

namespace NarutoDatabookApp.Repository
{
    public class SpecialtyRepository : ISpecialtyInterface
    {
        private readonly DataContext _context;

        public SpecialtyRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateSpecialty(Specialty specialty)
        {
            _context.Add(specialty);
            return Save();
        }

        public bool DeleteSpecialty(Specialty specialty)
        {
            _context.Remove(specialty);
            return Save();
        }

        public Specialty GetSpecialty(int specialtyId)
        {
            return _context.Specialties.Where(s => s.SpecialtyId == specialtyId).FirstOrDefault();
        }

        public Specialty GetSpecialty(string name)
        {
            return _context.Specialties.Where(s => s.Name == name).FirstOrDefault();
        }

        public ICollection<Specialty> GetSpecialties()
        {
            return _context.Specialties.OrderBy(s => s.SpecialtyId).ToList();
        }

        public bool SpecialtyExists(int specialtyId)
        {
            return _context.Specialties.Any(s => s.SpecialtyId == specialtyId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateSpecialty(Specialty specialty)
        {
            _context.Update(specialty);
            return Save();
        }
    }
}
