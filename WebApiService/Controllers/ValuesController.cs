using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Persistence.Sqlite;
using Service;

namespace WebApiSerivce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private const string dbFileName = "Scores.sqlite";
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ValuesController()
        {
            // todo: Use dependency injection
            _dbConnectionFactory = new SqliteDbConnectionFactory(dbFileName);
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {            
            ScoreService scoreService = new ScoreService(_dbConnectionFactory);
            var highscores = scoreService.GetHighscores();
            return highscores.Select(x => x.Name).ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            ScoreService scoreService = new ScoreService(_dbConnectionFactory);
            var highscores = scoreService.GetHighscores();
            var highscore = highscores.Single(x => x.Id == id);

            return highscore.Name;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
