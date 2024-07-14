using NarutoDatabookApp.Data;
using NarutoDatabookApp.Interfaces;
using NarutoDatabookApp.Models;
using System.Linq;

namespace NarutoDatabookApp.Repository
{
    public class TeamRepository : ITeamInterface
    {
        private readonly DataContext _context;

        public TeamRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateTeam(int villageId, Team team)
        {
            var village = _context.Villages.Where(v => v.VillageId == villageId).FirstOrDefault();
            team.Village = village;

            _context.Add(team);
            return Save();
        }

        public bool DeleteTeam(Team team)
        {
            _context.Remove(team);
            return Save();
        }

        public Team GetTeam(int teamId)
        {
            return _context.Teams.Where(t => t.TeamId == teamId).FirstOrDefault();
        }

        public Team GetTeam(string name)
        {
            return _context.Teams.Where(t => t.Name == name).FirstOrDefault();
        }

        public ICollection<Team> GetTeams()
        {
            return _context.Teams.OrderBy(t => t.TeamId).ToList();
        }

        public bool TeamExists(int teamId)
        {
            return _context.Teams.Any(t => t.TeamId == teamId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateTeam(int villageId, Team team)
        {
            team.Village = _context.Villages.Where(v => v.VillageId == villageId).FirstOrDefault();
            _context.Update(team);
            return Save();
        }
    }
}
