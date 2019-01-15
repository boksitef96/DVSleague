using DVSleague.Models;
using Neo4jClient;
using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVSleague.Repository
{
    public class MatchRepository : Repository
    {
        PlayerRepository playerRepository = new PlayerRepository();

        public void AddNewMatch(Match match, int leagueId, int homeTeamId,int guestTeamId)
        {
            int maxId = GetMaxId();
            match.Id = ++maxId;

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("MatchTime", match.MatchTime);
            queryDict.Add("HomeScore", match.HomeScore);
            queryDict.Add("AwayScore", match.AwayScore);


            string prepareQuery = "CREATE (m:Match { Id:" + match.Id
                                                    + ", MatchTime:'" + match.MatchTime + "',"
                                                    + " HomeScore:" + match.HomeScore + ","
                                                    + " AwayScore:" + match.AwayScore
                                                    + "}) return m";

            var query = new Neo4jClient.Cypher.CypherQuery(prepareQuery, queryDict, CypherResultMode.Set);
            List<Match> matches = ((IRawGraphClient)client).ExecuteGetCypherResults<Match>(query).ToList();

            string relationQuery = "MATCH (m:Match),(t:Team) WHERE m.Id = " + match.Id + "  AND t.Id = " + homeTeamId
                                 + " CREATE(t) -[h: playsHome]->(m) RETURN type(h)";

            query = new Neo4jClient.Cypher.CypherQuery(relationQuery, queryDict, CypherResultMode.Set);
            List<string> response = ((IRawGraphClient)client).ExecuteGetCypherResults<string>(query).ToList();

            relationQuery = "MATCH (m:Match),(t:Team) WHERE m.Id = " + match.Id + "  AND t.Id = " + guestTeamId
                            + " CREATE(t) -[a: playsAway]->(m) RETURN type(a)";

            query = new Neo4jClient.Cypher.CypherQuery(relationQuery, queryDict, CypherResultMode.Set);
            response = ((IRawGraphClient)client).ExecuteGetCypherResults<string>(query).ToList();

            relationQuery = "MATCH (m:Match),(l:League) WHERE m.Id = " + match.Id + "  AND l.Id = " + leagueId
                          + " CREATE (m) -[b: belongsTo]->(l) RETURN type(b)";

            query = new Neo4jClient.Cypher.CypherQuery(relationQuery, queryDict, CypherResultMode.Set);
            response = ((IRawGraphClient)client).ExecuteGetCypherResults<string>(query).ToList();
        }
        public void AddScorersAndAssistants(int matchId, List<int> scorersIds, List<int> assistenceIds)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            foreach (int id in scorersIds)
            {
                string relationQuery = "MATCH (m:Match),(p:Player) WHERE m.Id = " + matchId + "  AND p.Id = " + id
                                     + " CREATE (p) -[s: scorer]->(m) RETURN type(s)";

                var query = new Neo4jClient.Cypher.CypherQuery(relationQuery, queryDict, CypherResultMode.Set);
                ((IRawGraphClient)client).ExecuteCypher(query);

                playerRepository.IncrementPlayerGoals(id);
            }
            foreach (int id in assistenceIds)
            {
                string relationQuery = "MATCH (m:Match),(p:Player) WHERE m.Id = " + matchId + "  AND p.Id = " + id
                                     + " CREATE (p) -[a: assistent]->(m) RETURN type(a)";

                var query = new Neo4jClient.Cypher.CypherQuery(relationQuery, queryDict, CypherResultMode.Set);
                ((IRawGraphClient)client).ExecuteCypher(query);
                playerRepository.IncrementPlayerAssists(id);
            }
        }
        public List<Match> GetAllMatchesByLeagueId(int leagueId)
        {
            List<Match> leagues;
            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (m:Match)-[b:belongsTo]-(l:League {Id: " + leagueId + "}) RETURN m",
                                                            new Dictionary<string, object>(),
                                                            CypherResultMode.Set);

            leagues = ((IRawGraphClient)client).ExecuteGetCypherResults<Match>(query).ToList();
            return leagues;
        }
        public Match GetMatchById(int matchId)
        {
            Match match = new Match();
            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (m:Match) WHERE m.Id = " + matchId + "  RETURN m",
                                                            new Dictionary<string, object>(),
                                                            CypherResultMode.Set);
            List<Match> matches = ((IRawGraphClient)client).ExecuteGetCypherResults<Match>(query).ToList();
            if (matches.Count != 0)
            {
                match = matches[0];
            }

            //domacin
            query = new Neo4jClient.Cypher.CypherQuery("MATCH (t:Team)-[h:playsHome]->(m:Match { Id:" + match.Id + " }) RETURN t",
                                                       new Dictionary<string, object>(),
                                                       CypherResultMode.Set);
            List<Team> teams = ((IRawGraphClient)client).ExecuteGetCypherResults<Team>(query).ToList();
            if (teams.Count != 0)
            {
                match.HomeTeam = teams[0];
            }

            //gost
            query = new Neo4jClient.Cypher.CypherQuery("MATCH (t:Team)-[a:playsAway]->(m:Match { Id:" + match.Id + " }) RETURN t",
                                                      new Dictionary<string, object>(),
                                                      CypherResultMode.Set);
            teams = ((IRawGraphClient)client).ExecuteGetCypherResults<Team>(query).ToList();
            if (teams.Count != 0)
            {
                match.AwayTeam = teams[0];
            }

            //golgeteri
            query = new Neo4jClient.Cypher.CypherQuery("MATCH (p:Player)-[s:scorer]->(m:Match { Id: " + matchId + " }) RETURN p",
                                                     new Dictionary<string, object>(),
                                                     CypherResultMode.Set);
            List<Player> scorers = ((IRawGraphClient)client).ExecuteGetCypherResults<Player>(query).ToList();
            if (teams.Count != 0)
            {
                match.Scorers = scorers;
            }

            //asistenti
            query = new Neo4jClient.Cypher.CypherQuery("MATCH (p:Player)-[a:assistent]->(m:Match { Id: " + matchId + " }) RETURN p",
                                                    new Dictionary<string, object>(),
                                                    CypherResultMode.Set);
            List<Player> assistants = ((IRawGraphClient)client).ExecuteGetCypherResults<Player>(query).ToList();
            if (teams.Count != 0)
            {
                match.Assistents = assistants;
            }

            return match;
        }
        public void DeleteMatchById(int matchId)
        {
            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (m:Match { Id:" + matchId + " }) DETACH DELETE m",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
            ((IRawGraphClient)client).ExecuteCypher(query);
        }
        private int GetMaxId()
        {
            int maxId;
            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (m:Match) RETURN max(m.Id)",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
            try
            {
                maxId = ((IRawGraphClient)client).ExecuteGetCypherResults<int>(query).ToList().FirstOrDefault();
            }
            catch
            {
                maxId = 0;
            }

            return maxId;
        }
    }
}