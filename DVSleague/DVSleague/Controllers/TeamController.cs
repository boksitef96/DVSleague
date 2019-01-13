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
        // GET: Team
        public ActionResult Index()
        {
            return View();
        }


        [Route("team", Name = "team_details")]
        public ActionResult TeamDetails() //     view/home/Team/PlayerDetails
        {
            TeamRepository teamRepository = new TeamRepository();

            Team team = new Team();

            team=teamRepository.getTeamById(2);


            ViewBag.Message = "Your application description page." + "dsddsd";

            return View(team);
        }
    }
}