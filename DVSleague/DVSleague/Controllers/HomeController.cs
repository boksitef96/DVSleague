using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVSleague.Controllers
{
    public class HomeController : Controller
    {
        private GraphClient client;
        public ActionResult Index()
        {
            client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "DVSleague");
            try
            { 
                client.Connect();
            }
            catch (Exception e)
            {
                string error = e.Message;
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}