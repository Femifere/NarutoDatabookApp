using NarutoDatabookApp.Models;
using System;
using System.Collections.Generic;

namespace NarutoDatabookApp.Interfaces
{
    public interface IFanInterface
    {
        ICollection<Fan> GetFans();
        Fan GetFan(int fanId);
        Fan GetFan(string name);
        Fan GetFan(DateTime dateWatched);
        bool FanExists(int fanId);
        bool CreateFan(int rankingId, Fan fan);
        bool UpdateFan(int rankingId, Fan fan);
        bool DeleteFan(Fan fan);
        bool Save();
    }
}
