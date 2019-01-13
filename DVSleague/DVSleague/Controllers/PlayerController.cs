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
        // GET: Player
        public ActionResult Index()
        {
            return View();
        }

        [Route("player/{id}", Name = "player_details")]
        public ActionResult PlayerDetails(int id) //     view/home/Player/PlayerDetails
        {

            TeamRepository teamrep = new TeamRepository();

            Team team = new Team();
            team = teamrep.getTeamById(2);

            List<Player> listOfPlayers = new List<Player>();
            Player player = new Player();

            foreach (Player ply in team.Squad)
            {
                if(ply.Id == id)
                {
                    player.FirstName = ply.FirstName;
                    player.LastName = ply.LastName;
                    player.DateOfBirth = ply.DateOfBirth;
                    player.Nationality = ply.Nationality;
                    player.Height = ply.Height;
                    player.Weight = ply.Weight;
                    player.Goals = ply.Goals;
                    player.Assists = ply.Assists;
                    break;
                }
            }
            


            return View(player);
        }


        [Route("show-players/{id}", Name = "show_players")]
        public ActionResult ShowPlayersByTeam(int id)        //  view/home/Player/ShowPlayersByTeam
        {
            TeamRepository teamrep = new TeamRepository();

            Team team = new Team();
            team = teamrep.getTeamById(2);

            List<Player> listOfPlayers = new List<Player>();

            foreach(Player ply in team.Squad)
            {
                listOfPlayers.Add(ply);
            }

            return View(listOfPlayers);
        }



    }
}