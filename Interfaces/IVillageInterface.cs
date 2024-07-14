using NarutoDatabookApp.Models;
using System.Collections.Generic;

namespace NarutoDatabookApp.Interfaces
{
    public interface IVillageInterface
    {
        ICollection<Village> GetVillages();
        Village GetVillage(int villageId);
        Village GetVillage(string name);
        bool VillageExists(int villageId);
        bool CreateVillage(Village village);
        bool UpdateVillage(Village village);
        bool DeleteVillage(Village village);
        bool Save();
    }
}
