using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DVSleague.Models;

namespace DVSleague.Repository3
{
    public class TeamRepository
    {

        public Team getTeamById(int id)
        {
            Player zigara = new Player
            {
                Id = 1,
                FirstName = "Marko",
                LastName = "Zigic",
                DateOfBirth = new DateTime(2010, 02, 02),
                Position = "CF",
                Nationality = "Srbin",
                Height = 190,
                Weight = 80,
                Goals = 20,
                Assists = 5
            };


            Player tadic = new Player
            {
                Id = 2,
                FirstName = "Dusan",
                LastName = "Tadic",
                DateOfBirth = new DateTime(1991, 02, 02),
                Position = "SS",
                Nationality = "Srbin",
                Height = 180,
                Weight = 70,
                Goals = 13,
                Assists = 13
            };

            List<Player> players = new List<Player>();
            players.Add(zigara);
            players.Add(tadic);

            Team monaco = new Team
            {
                Id = 1,
                Name = "Monaco",
                City = "Monaco",
                Country = "France",
                Squad=players,
                League = new League()
            };

            return monaco;
        }
    }

 
}