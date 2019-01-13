﻿using DVSleague.Models;
using DVSleague.Repository2;
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
            League league = leagueRepository.GetLeagueById(1);
            return View(league);
        }

    }
}