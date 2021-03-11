using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SeasonsGNT.Controllers
{
    public class RewardController : ApiController
    {
        /*CREAR REWARD*/
        [HttpPost, Route("apiv1/gnt/reward/crear")]
        public HttpResponseMessage CRUD_gnt_rewards([FromBody] Seasons.Modelos.CRUD_gnt_rewards crud)
        {
            string r = null;
            Seasons.SeasonMDB.CrearReward cs = new Seasons.SeasonMDB.CrearReward();
            Seasons.Modelos.StringConect st = new Seasons.Modelos.StringConect();
            st.server = ConfigurationManager.AppSettings["server"];
            st.uid = ConfigurationManager.AppSettings["uid"];
            st.pwd = ConfigurationManager.AppSettings["pwd"];
            st.database = ConfigurationManager.AppSettings["database"];
            r = cs.CRUD_gnt_rewards(crud, st);
            return Request.CreateResponse(HttpStatusCode.OK, r);
        }

        /*GET REWARDS*/
        [HttpGet, Route("apiv1/gnt/reward")]
        public HttpResponseMessage SELECT_gnt_seasons()
        {
            string r = null;
            Seasons.SeasonMDB.CrearReward cs = new Seasons.SeasonMDB.CrearReward();
            Seasons.Modelos.StringConect st = new Seasons.Modelos.StringConect();
            st.server = ConfigurationManager.AppSettings["server"];
            st.uid = ConfigurationManager.AppSettings["uid"];
            st.pwd = ConfigurationManager.AppSettings["pwd"];
            st.database = ConfigurationManager.AppSettings["database"];
            r = cs.SELECT_gnt_rewards(st);
            return Request.CreateResponse(HttpStatusCode.OK, r);
        }
    }
}
