using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;
using EwBD_WebApi.Models;

namespace EwBD_WebApi.Controllers
{
    public class LevelController : ApiController
    {
        [HttpGet]
        [Route("api/level")]
        public IHttpActionResult GetLevel(int levelNr = 1)
        {
            var result = new Models.Level
            {
                LevelNr = levelNr
            };
            using (var db = new MyModelContainer())
            {
                var level = db.Levels.FirstOrDefault(l => l.LevelNr == levelNr);

                if (level == null)
                {
                    return NotFound();
                }

                result = new JavaScriptSerializer().
                    Deserialize<Models.Level>(level.LevelConfiguration);
            }
            //test();

            return Ok(result);
        }

        [HttpPost]
        [Route("api/level")]
        public void PostLevel(Level level)
        {
            using (var db = new MyModelContainer())
            {
                db.Levels.Add(level);
                db.SaveChanges();
            }
        }

        private void InsertLevel1()
        {
            var test = new Models.Level
            {
                LevelNr = 1,
                DiamondPositions = new List<Vector3>(),
                RockPositions = new List<Vector3>()
            };
            test.DiamondPositions.Add(new Vector3(1, 0.5, 0));
            test.DiamondPositions.Add(new Vector3(4, 0.5, 2));
            test.DiamondPositions.Add(new Vector3(6, 0.5, 8));

            test.RockPositions.Add(new Vector3(2, 0.5, 0.95));
            test.RockPositions.Add(new Vector3(4.5, 0.5, 3));
            test.RockPositions.Add(new Vector3(4.5, 0.5, 4));

            var level = new Level
            {
                LevelNr = 666,
                LevelConfiguration = new JavaScriptSerializer().Serialize(test)
            };
            PostLevel(level);
        }
    }
}