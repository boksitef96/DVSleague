using DVSleague.Models;
using DVSleague.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVSleague.Controllers
{
    public class LeagueController : Controller
    {
        private LeagueRepository leagueRepository = new LeagueRepository();

        // GET: League
        public ActionResult Index()
        {
            return View();
        }           
   
        [Route("league/{id}", Name = "show_league_details")]
        public ActionResult ShowLeagueDetails(int id)
        {
            League league = leagueRepository.GetLeagueById(id);
            return View(league);
        }

        [Route("league/{id}/teams", Name = "show_teams_in_league")]
        public ActionResult ShowAllTeamsInLeague(int id)
        {
            League league = leagueRepository.GetLeagueById(id);
            return View("~/Views/Team/ShowTeamsInLeague.cshtml", league.Teams);
        }

        [Route("show-all-leagues", Name = "show_all_leagues")]
        public ActionResult ShowAllLeagues()
        {
            List<League> list = new List<League>();
            list = leagueRepository.GetAllLeagues();
            return View(list);
        }

        [Route("add-new-league")]
        public ActionResult AddNewLeague()
        {
            return View();
        }

        [HttpPost]
        [Route("add-league", Name = "add_league")]
        public ActionResult AddLeague(League league)
        {
            leagueRepository.AddNewLeague(league);
            return RedirectToAction("ShowLeagueDetails", new { id = league.Id });
        }
    }
}