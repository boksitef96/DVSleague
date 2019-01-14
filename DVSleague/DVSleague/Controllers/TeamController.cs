using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DVSleague.Models;
using DVSleague.Repository3;

namespace DVSleague.Controllers
{
    public class TeamController : Controller
    {
        private TeamRepository TeamRepository = new TeamRepository();
        
        // GET: Team
        public ActionResult Index()
        {
            return View();
        }

        [Route("team/{id}", Name = "team_details")]
        public ActionResult TeamDetails(int id) //     view/home/Team/PlayerDetails
        {
            Team team = TeamRepository.getTeamById(2);
            return View(team);
        }
    }
}