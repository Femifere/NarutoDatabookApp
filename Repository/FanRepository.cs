using NarutoDatabookApp.Data;
using NarutoDatabookApp.Interfaces;
using NarutoDatabookApp.Models;
using System.Linq;

namespace NarutoDatabookApp.Repository
{
    public class FanRepository : IFanInterface
    {
        private readonly DataContext _context;

        public FanRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateFan(int rankingId, Fan fan)
        {
            var ranking = _context.Rankings.Where(r => r.RankingId == rankingId).FirstOrDefault();

            fan.Ranking = ranking;

            _context.Add(fan);
            return Save();
        }

        public bool DeleteFan(Fan fan)
        {
            _context.Remove(fan);
            return Save();
        }

        public Fan GetFan(int fanId)
        {
            return _context.Fans.Where(f => f.FanId == fanId).FirstOrDefault();
        }

        public Fan GetFan(string name)
        {
            return _context.Fans.Where(f => f.Name == name).FirstOrDefault();
        }

        public Fan GetFan(DateTime dateWatched)
        {
            return _context.Fans.Where(f => f.DateWatched == dateWatched).FirstOrDefault();
        }

        public ICollection<Fan> GetFans()
        {
            return _context.Fans.OrderBy(f => f.FanId).ToList();
        }

        public bool FanExists(int fanId)
        {
            return _context.Fans.Any(f => f.FanId == fanId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateFan(int rankingId, Fan fan)
        {
            fan.Ranking = _context.Rankings.Where(r => r.RankingId == rankingId).FirstOrDefault();
            _context.Update(fan);
            return Save();
        }
    }
}
