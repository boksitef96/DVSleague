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
            Team team = TeamRepository.getTeamById(2);
            Player modelPlayer = new Player();
            foreach (Player player in team.Squad)
            {
                if(player.Id == id)
                {
                    modelPlayer = player;   
                }
            }

            return View(modelPlayer);
        }

        [Route("team/{teamId}/players", Name = "show_players")]
        public ActionResult ShowPlayersByTeam(int teamId)        //  view/home/Player/ShowPlayersByTeam
        {
            List<Player> players = PlayerRepository.GetPlayersByTeam(1);
            return View(players);
        }

        [Route("add-new-player", Name = "add_new_player")]
        public ActionResult AddNewPlayer()
        {
            return View();
        }

        [Route("add-player")]
        public ActionResult AddPlayer(Player player)
        {
            
            //function for add new league in neo4j
            return RedirectToAction("PlayerDetails", new { id = player.Id });
        }

    }
}