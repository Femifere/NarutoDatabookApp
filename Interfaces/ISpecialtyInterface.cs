using NarutoDatabookApp.Models;
using System.Collections.Generic;

namespace NarutoDatabookApp.Interfaces
{
    public interface ISpecialtyInterface
    {
        ICollection<Specialty> GetSpecialties();
        Specialty GetSpecialty(int specialtyId);
        Specialty GetSpecialty(string name);
        bool SpecialtyExists(int specialtyId);
        bool CreateSpecialty(Specialty specialty);
        bool UpdateSpecialty(Specialty specialty);
        bool DeleteSpecialty(Specialty specialty);
        bool Save();
    }
}
