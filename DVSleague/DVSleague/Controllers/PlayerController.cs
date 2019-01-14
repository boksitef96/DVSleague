using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DVSleague.Models;
using DVSleague.Repository3;

namespace DVSleague.Controllers
{
    public class PlayerController : Controller
    {
        private TeamRepository TeamRepository = new TeamRepository();
        private PlayerRepository PlayerRepository = new PlayerRepository();
        
        // GET: Player
        public ActionResult Index()
        {
            return View();
        }

        [Route("player/{id}", Name = "player_details")]
        public ActionResult PlayerDetails(int id) //     view/home/Player/PlayerDetails
        {
            Player player = PlayerRepository.GetPlayerById(id);
            return View(player);
        }

        [Route("team/{teamId}/players", Name = "show_players")]
        public ActionResult ShowPlayersByTeam(int teamId)        //  view/home/Player/ShowPlayersByTeam
        {
            List<Player> players = PlayerRepository.GetPlayersByTeam(1);
            return View(players);
        }

        [Route("team/{teamId}/add-new-player", Name = "add_new_player")]
        public ActionResult AddNewPlayer(int teamId)
        {
            ViewBag.TeamId = teamId;
            return View();
        }

        [Route("add-player")]
        public ActionResult AddPlayer(Player player, int teamId)
        {
            Team team = TeamRepository.getTeamById(teamId);
            player.Team = team;
            //function for add new player in neo4j

            return RedirectToAction("PlayerDetails", new { id = player.Id });
        }

    }
}