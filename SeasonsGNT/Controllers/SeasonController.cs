using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SeasonsGNT.Controllers
{
    public class SeasonController : ApiController
    {
        /*CREAR SEASON*/
        [HttpPost, Route("apiv1/gnt/season/crear")]
        public HttpResponseMessage CRUD_gnt_seasons([FromBody] Seasons.Modelos.CRUD_gnt_seasons crud)
        {
            string r = null;
            Seasons.SeasonMDB.CrearSeason cs = new Seasons.SeasonMDB.CrearSeason();
            Seasons.Modelos.StringConect st = new Seasons.Modelos.StringConect();
            st.server = ConfigurationManager.AppSettings["server"];
            st.uid = ConfigurationManager.AppSettings["uid"];
            st.pwd = ConfigurationManager.AppSettings["pwd"];
            st.database = ConfigurationManager.AppSettings["database"];
            r = cs.CRUD_gnt_seasons(crud, st);
            return Request.CreateResponse(HttpStatusCode.OK, r);
        }

        /*GET SEASON*/
        [HttpGet, Route("apiv1/gnt/season")]
        public HttpResponseMessage SELECT_gnt_seasons()
        {
            string r = null;
            Seasons.SeasonMDB.CrearSeason cs = new Seasons.SeasonMDB.CrearSeason();
            Seasons.Modelos.StringConect st = new Seasons.Modelos.StringConect();
            st.server = ConfigurationManager.AppSettings["server"];
            st.uid = ConfigurationManager.AppSettings["uid"];
            st.pwd = ConfigurationManager.AppSettings["pwd"];
            st.database = ConfigurationManager.AppSettings["database"];
            r = cs.SELECT_gnt_seasons(st);
            return Request.CreateResponse(HttpStatusCode.OK, r);
        }
    }
}
