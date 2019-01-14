using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DVSleague.Models;
using DVSleague.Repository3;
using DVSleague.Repository2
    ;
namespace DVSleague.Controllers
{
    public class TeamController : Controller
    {
        private TeamRepository TeamRepository = new TeamRepository();
        private LeagueRepository LeagueRepository = new LeagueRepository(); 
        
        // GET: Team
        public ActionResult Index()
        {
            return View();
        }

        [Route("team/{id}", Name = "team_details")]
        public ActionResult TeamDetails(int id)
        {
            Team team = TeamRepository.getTeamById(2);
            return View(team);
        }
        
        [Route("league/{leagueId}/add-new-team")]
        public ActionResult AddNewTeam(int leagueId)
        {
            ViewBag.LeagueId = leagueId;
            return View();
        }

        [HttpPost]
        [Route("add-team")]
        public ActionResult AddTeam(Team team, int leagueId)
        {
            League league = LeagueRepository.GetLeagueById(leagueId);
            team.League = league;
            //function for add new team in neo4j

            return RedirectToAction("TeamDetails", new { id = team.Id });
        }
    }
}