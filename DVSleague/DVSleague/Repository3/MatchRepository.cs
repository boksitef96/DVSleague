using DVSleague.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVSleague.Repository3
{
    public class MatchRepository
    {
        public Match GetMatchById(int matchId)
        {
            return new Match();
        }

        public List<Match> GetAllMatchesByLeagueId(int leagueId)
        {
            return new List<Match>();
        }
    }
}