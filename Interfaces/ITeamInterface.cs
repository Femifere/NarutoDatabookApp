using NarutoDatabookApp.Models;
using System.Collections.Generic;

namespace NarutoDatabookApp.Interfaces
{
    public interface ITeamInterface
    {
        ICollection<Team> GetTeams();
        Team GetTeam(int teamId);
        Team GetTeam(string name);
        bool TeamExists(int teamId);
        bool CreateTeam(int villageId, Team team);
        bool UpdateTeam(int villageId, Team team);
        bool DeleteTeam(Team team);
        bool Save();
    }
}
