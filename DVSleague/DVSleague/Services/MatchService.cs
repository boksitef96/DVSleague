using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DVSleague.Models;
using DVSleague.Repository;

namespace DVSleague.Services
{
    public class MatchService
    {   
        private TeamRepository TeamRepository = new TeamRepository();
        private MatchRepository MatchRepository = new MatchRepository();
        private PlayerRepository PlayerRepository = new PlayerRepository();

        public IEnumerable<SelectListItem> GetTeamsForChoices(int leagueId)
        {
            List<Team> teamsList = TeamRepository.GetTeamsByLeagueId(leagueId);
            var teams = teamsList
                        .Select(t =>
                                new SelectListItem
                                {
                                    Value = t.Id.ToString(),
                                    Text = t.Name
                                });

            return new SelectList(teams, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetPlayersForChoices(int matchId)
        {
            Match match = MatchRepository.GetMatchById(matchId);
            List<Player> homePlayers = PlayerRepository.GetPlayersByTeamId(match.HomeTeam.Id);
            List<Player> awayPlayers = PlayerRepository.GetPlayersByTeamId(match.AwayTeam.Id);

            List<Player> playersList = new List<Player>();

            foreach(Player player in homePlayers)
            {
                playersList.Add(player);
            }
            foreach(Player player in awayPlayers)
            {
                playersList.Add(player);
            }

            var players = playersList
                        .Select(p =>
                                new SelectListItem
                                {
                                    Value = p.Id.ToString(),
                                    Text = p.FirstName + " " + p.LastName + " - \"" + p.Team.Name + "\"" 
                                });

            return new SelectList(players, "Value", "Text");
        }
    }
}