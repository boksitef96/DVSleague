using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DVSleague.Models;
using DVSleague.ViewModel;
using DVSleague.Services;
using DVSleague.Repository;
using DVSleague.Repository3;

namespace DVSleague.Controllers
{
    public class MatchController : Controller
    {
        private TeamRepository TeamRepository = new TeamRepository();
        private MatchRepository MatchRepository = new MatchRepository();
        private MatchService MatchService = new MatchService();

        // GET: Match
        public ActionResult Index()
        {
            return View();
        }

        [Route("league/{leagueId}/add-new-match", Name = "add_new_match")]
        public ActionResult AddNewMatch(int leagueId)
        {
            ViewBag.LeagueId = leagueId;
            var teams = MatchService.GetTeamsForChoices(leagueId);
            
            AddNewMatchChoices model = new AddNewMatchChoices
            {
               Teams = teams
            };
            return View(model);
        }

        [Route("add-match")]
        public ActionResult AddMatch(AddNewMatchChoices model, int leagueId)
        {
            Match match = model.Match;
            //MatchService.AddNewMatch(match, leagueId, match.HomeTeam.Id, match.AwayTeam.Id);
            return RedirectToAction("Index", "Home");
        }

        [Route("match/{id}/details", Name = "add_match_details")]
        public ActionResult AddDetailsForMatch(int id)
        {
            ViewBag.MatchId = id;

            var players = MatchService.GetPlayersForChoices(id);

            AddNewMatchChoices model = new AddNewMatchChoices
            {
                Players = players
            };
            return View(model); ;
        }

        [Route("add-match-details")]
        public ActionResult AddMatchDetails(AddNewMatchChoices model)
        {

            return RedirectToAction("Index", "Home");
        }

        [Route("league/{leagueId}/matches")]
        public ActionResult ShowMatchesByLeague(int leagueId)
        {
            List<Match> matches = MatchRepository.GetAllMatchesByLeagueId(leagueId);
            return View(matches);
        }
    }
}