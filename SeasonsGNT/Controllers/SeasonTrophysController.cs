using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SeasonsGNT.Back;
using SeasonsGNT.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SeasonsGNT.Controllers
{
    public class SeasonTrophysController : ApiController
    {
        [HttpPost, Route("apiv1/gnt/season/trofeos/crear")]
        public HttpResponseMessage CRUD_gnt_seasons_trophys([FromBody] CRUD_gnt_seasons_trophys crud)
        {
            GNTMDBTrophys gNTMDBTrophys = new GNTMDBTrophys();
            string text = gNTMDBTrophys.CRUD_gnt_seasons_trophys(crud);
            return Request.CreateResponse(HttpStatusCode.OK, text);
        }

        [HttpGet, Route("apiv1/gnt/season/trofeos/get")]
        public HttpResponseMessage SELECT_gnt_seasons_trophys([FromBody] CRUD_gnt_seasons_trophys crud)
        {
            GNTMDBTrophys gNTMDBTrophys = new GNTMDBTrophys();
            string text = gNTMDBTrophys.SELECT_gnt_seasons_trophys();
            dynamic dyn = JsonConvert.DeserializeObject(text);
            string proEs = ConfigurationManager.AppSettings["proEs"];
            string proEn = ConfigurationManager.AppSettings["proEn"];
            string seasonEs = ConfigurationManager.AppSettings["seasonEs"];
            string seasonEn = ConfigurationManager.AppSettings["seasonEn"];
            string balanceEs = ConfigurationManager.AppSettings["balanceEs"];
            string balanceEn = ConfigurationManager.AppSettings["balanceEn"];
            string lotesEs = ConfigurationManager.AppSettings["lotesEs"];
            string lotesEn = ConfigurationManager.AppSettings["lotesEn"];
            string lotesTCEs = ConfigurationManager.AppSettings["lotesTCEs"];
            string lotesTCEn = ConfigurationManager.AppSettings["lotesTCEn"];
            string posicionesEs = ConfigurationManager.AppSettings["posicionesEs"];
            string posicionesEn = ConfigurationManager.AppSettings["posicionesEn"];
            string simbolosEs = ConfigurationManager.AppSettings["simbolosEs"];
            string simbolosEn = ConfigurationManager.AppSettings["simbolosEn"];
            JObject jObject = new JObject(new object[]
            {
                new JProperty("Es", new JArray(new JObject(new object[]
                {
                    new JProperty("pro", proEs),
                    new JProperty("season", seasonEs),
                    new JProperty("balanceT", balanceEs),
                    new JProperty("lotes", lotesEs),
                    new JProperty("lotesTC", lotesTCEs),
                    new JProperty("posiciones", posicionesEs),
                    new JProperty("simbolos", simbolosEs)
                }))),
                new JProperty("En", new JArray(new JObject(new object[]
                {
                    new JProperty("pro", proEn),
                    new JProperty("season", seasonEn),
                    new JProperty("balanceT", balanceEn),
                    new JProperty("lotes", lotesEn),
                    new JProperty("lotesTC", lotesTCEn),
                    new JProperty("posiciones", posicionesEn),
                    new JProperty("simbolos", simbolosEn)
                })))
            });
            JObject jObject2 = new JObject(new object[]
            {
                new JProperty("Es", new JArray(new JObject(new object[]
                {
                    new JProperty("lotes", dyn.lotes_es),
                    new JProperty("lotesTC", dyn.lotesTC_es),
                    new JProperty("balanceT", dyn.balanceT_es),
                    new JProperty("posiciones", dyn.posiciones_es),
                    new JProperty("simbolos", dyn.Symbol_Es),
                    new JProperty("pro", dyn.pro_es),
                    new JProperty("season", dyn.season_es)
                }))),
                new JProperty("En", new JArray(new JObject(new object[]
                {
                    new JProperty("lotes", dyn.lotes_en),
                    new JProperty("lotesTC", dyn.lotesTC_en),
                    new JProperty("balanceT", dyn.balanceT_en),
                    new JProperty("posiciones", dyn.posiciones_en),
                    new JProperty("simbolos", dyn.Symbol_En),
                    new JProperty("pro", dyn.pro_en),
                    new JProperty("season", dyn.season_en)
                })))
            });
            dynamic final = new JObject();
            final.idgnt_seasons = dyn.idgnt_seasons;
            final.Lots = dyn.Lots;
            final.Balance = dyn.Balance;
            final.DateINI = dyn.DateINI;
            final.DateEND = dyn.DateEND;
            final.trophyName = jObject;
            final.Description = jObject2;
            object ob = final;
            return Request.CreateResponse(HttpStatusCode.OK, ob);
        }
    }
}
