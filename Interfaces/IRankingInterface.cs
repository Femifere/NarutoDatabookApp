using NarutoDatabookApp.Models;
using System.Collections.Generic;

namespace NarutoDatabookApp.Interfaces
{
    public interface IRankingInterface
    {
        ICollection<Ranking> GetRankings();
        Ranking GetRanking(int rankingId);
        Ranking GetRanking(string name);
        bool RankingExists(int rankingId);
        bool CreateRanking(Ranking ranking);
        bool UpdateRanking(Ranking ranking);
        bool DeleteRanking(Ranking ranking);
        bool Save();
    }
}
