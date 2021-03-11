using Newtonsoft.Json;
using SeasonsGNT.Back;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SeasonsGNT.Controllers
{
    public class TrophyController : ApiController
    {

        Seasons.JWT.ValidateToken valD = new Seasons.JWT.ValidateToken();
        Seasons.JWT.JWTModel jwt = new Seasons.JWT.JWTModel();
        ValidacionJWT auth = new ValidacionJWT();

        /*GET CTAS TROPHYS*/
        [HttpGet, Route("apiv1/gnt/get/trophys")]
        public HttpResponseMessage SELECT_gnt_seasons([FromUri] int idgnt_seasons,[FromUri] int idLogin)
        {
            string host = Request.Headers.Host;
            Logger log = new Logger();
            log.LogMessageToFile("HOST : " + host);
            string r = null;
            TrophyHelper helper = new TrophyHelper();
            Seasons.SeasonMDB.Trophy cs = new Seasons.SeasonMDB.Trophy();
            Seasons.Modelos.StringConect st = new Seasons.Modelos.StringConect();
            st.server = ConfigurationManager.AppSettings["server"];
            st.uid = ConfigurationManager.AppSettings["uid"];
            st.pwd = ConfigurationManager.AppSettings["pwd"];
            st.database = ConfigurationManager.AppSettings["database"];
            int posit = 0;
            if(idLogin != 0)
            {
                r = cs.GETTROFEOS(idgnt_seasons, 0, st);
                r = helper.postTrofeos(r, 0);
                //r = helper.getUsersTrophy(r, 0, 0);
                dynamic dynJsonP = JsonConvert.DeserializeObject(r);
                foreach(var p in dynJsonP)
                {
                    if(p.login == idLogin)
                    {
                        posit = p.id;
                    }                                        
                }
                r = cs.GETTROFEOS(idgnt_seasons, idLogin, st);
                r = helper.getUsersTrophy(r, idLogin, posit);
            }
            else
            {
                r = cs.GETTROFEOS(idgnt_seasons, idLogin, st);
                r = helper.getUsersTrophy(r, idLogin, 0);
            }            
            return Request.CreateResponse(HttpStatusCode.OK, r);
            /*valD = new Seasons.JWT.ValidateToken();
            jwt = new Seasons.JWT.JWTModel();
            jwt.hashCode = valD.validateJWT(Request.Headers.Authorization.Parameter);
            jwt.cuentasMeta = Convert.ToInt32(idLogin);
            jwt.tipo = "SEASON";
            r = auth.JWT(jwt);
            if (r.Equals("Acceso denegado"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, r);
            }
            else
            {
                TrophyHelper helper = new TrophyHelper();
                Seasons.SeasonMDB.Trophy cs = new Seasons.SeasonMDB.Trophy();
                Seasons.Modelos.StringConect st = new Seasons.Modelos.StringConect();
                st.server = ConfigurationManager.AppSettings["server"];
                st.uid = ConfigurationManager.AppSettings["uid"];
                st.pwd = ConfigurationManager.AppSettings["pwd"];
                st.database = ConfigurationManager.AppSettings["database"];
                r = cs.GETTROFEOS(idgnt_seasons, idLogin, st);
                r = helper.getUsersTrophy(r, idLogin);
                return Request.CreateResponse(HttpStatusCode.OK, r);
            }*/
        }
    }
}
