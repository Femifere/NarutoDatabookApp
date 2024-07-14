using NarutoDatabookApp.Data;
using NarutoDatabookApp.Interfaces;
using NarutoDatabookApp.Models;
using System.Linq;

namespace NarutoDatabookApp.Repository
{
    public class RankingRepository : IRankingInterface
    {
        private readonly DataContext _context;

        public RankingRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateRanking(Ranking ranking)
        {
            _context.Add(ranking);
            return Save();
        }

        public bool DeleteRanking(Ranking ranking)
        {
            _context.Remove(ranking);
            return Save();
        }

        public Ranking GetRanking(int rankingId)
        {
            return _context.Rankings.Where(r => r.RankingId == rankingId).FirstOrDefault();
        }

        public Ranking GetRanking(string name)
        {
            return _context.Rankings.Where(r => r.Name == name).FirstOrDefault();
        }

        public ICollection<Ranking> GetRankings()
        {
            return _context.Rankings.OrderBy(r => r.RankingId).ToList();
        }

        public bool RankingExists(int rankingId)
        {
            return _context.Rankings.Any(r => r.RankingId == rankingId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateRanking(Ranking ranking)
        {
            _context.Update(ranking);
            return Save();
        }
    }
}
