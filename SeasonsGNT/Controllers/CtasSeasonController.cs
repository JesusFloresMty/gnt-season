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
    public class CtasSeasonController : ApiController
    {

        Seasons.JWT.ValidateToken valD = new Seasons.JWT.ValidateToken();
        Seasons.JWT.JWTModel jwt = new Seasons.JWT.JWTModel();
        ValidacionJWT auth = new ValidacionJWT();


        /*CREAR CTAS SEASON*/
        [HttpPost, Route("apiv1/gnt/ctas/season/crud")]
        public HttpResponseMessage CRUD_gnt_rewards([FromBody] Seasons.Modelos.CRUD_gnt_clientes_season_ctas crud)
        {
            string r = null;
            valD = new Seasons.JWT.ValidateToken();
            jwt = new Seasons.JWT.JWTModel();
            jwt.hashCode = valD.validateJWT(Request.Headers.Authorization.Parameter);
            jwt.idgnt_clientesInfo = Convert.ToInt32(crud.idgnt_clientesInfo);
            jwt.tipo = "INFO";
            r = auth.JWT(jwt);
            if (r.Equals("Acceso denegado"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, r);
            }
            else
            {
                Seasons.SeasonMDB.CrearSeason_ctas cs = new Seasons.SeasonMDB.CrearSeason_ctas();
                Seasons.Modelos.StringConect st = new Seasons.Modelos.StringConect();
                st.server = ConfigurationManager.AppSettings["server"];
                st.uid = ConfigurationManager.AppSettings["uid"];
                st.pwd = ConfigurationManager.AppSettings["pwd"];
                st.database = ConfigurationManager.AppSettings["database"];
                r = cs.CRUD_gnt_clientes_season_ctas(crud, st);
                return Request.CreateResponse(HttpStatusCode.OK, r);
            }            
        }

        /*GET CTAS SEASON*/
        [HttpGet, Route("apiv1/gnt/ctas/season")]
        public HttpResponseMessage SELECT_gnt_seasons()
        {
            string r = null;
            Seasons.SeasonMDB.CrearSeason_ctas cs = new Seasons.SeasonMDB.CrearSeason_ctas();
            Seasons.Modelos.StringConect st = new Seasons.Modelos.StringConect();
            st.server = ConfigurationManager.AppSettings["server"];
            st.uid = ConfigurationManager.AppSettings["uid"];
            st.pwd = ConfigurationManager.AppSettings["pwd"];
            st.database = ConfigurationManager.AppSettings["database"];
            r = cs.SELECT_gnt_clientes_season_ctas(st);
            return Request.CreateResponse(HttpStatusCode.OK, r);
        }

        /*POST CTAS SEASON VALIDAR*/
        [HttpPost, Route("apiv1/gnt/ctas/season/validar")]
        public HttpResponseMessage VALIDATE_SEASON([FromBody] Seasons.Modelos.VALIDATE_SEASON val)
        {
            string r = null;
            Seasons.SeasonMDB.CrearSeason_ctas cs = new Seasons.SeasonMDB.CrearSeason_ctas();
            Seasons.Modelos.StringConect st = new Seasons.Modelos.StringConect();
            st.server = ConfigurationManager.AppSettings["server"];
            st.uid = ConfigurationManager.AppSettings["uid"];
            st.pwd = ConfigurationManager.AppSettings["pwd"];
            st.database = ConfigurationManager.AppSettings["database"];
            r = cs.VALIDATE_SEASON(val, st);
            return Request.CreateResponse(HttpStatusCode.OK, r);
        }

        /*CREAR CTAS SEASON METATRADER*/
        [HttpPost, Route("apiv1/gnt/ctas/season/crear")]
        public HttpResponseMessage cuentaSalida([FromBody] object ob)
        {
            string r = null;
            string rV = null;
            var json = "";
            json = JsonConvert.SerializeObject(ob);
            dynamic dynJson1 = JsonConvert.DeserializeObject(json);
            valD = new Seasons.JWT.ValidateToken();
            jwt = new Seasons.JWT.JWTModel();
            jwt.hashCode = valD.validateJWT(Request.Headers.Authorization.Parameter);
            jwt.idgnt_clientesInfo = Convert.ToInt32(dynJson1.idgnt_clientesInfo);
            jwt.tipo = "INFO";
            r = auth.JWT(jwt);
            if (r.Equals("Acceso denegado"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, r);
            }
            else
            {
                Seasons.SeasonMDB.CrearSeason_ctas cs = new Seasons.SeasonMDB.CrearSeason_ctas();
                Seasons.Modelos.StringConect st = new Seasons.Modelos.StringConect();
                Seasons.Modelos.Conector conectorGNT = new Seasons.Modelos.Conector();
                Seasons.Modelos.CrearCuenta cuenta = new Seasons.Modelos.CrearCuenta();
                Seasons.MT5Ctas.CrearCuenta cc = new Seasons.MT5Ctas.CrearCuenta();
                Seasons.Modelos.CRUD_gnt_clientes_season_ctas crud = new Seasons.Modelos.CRUD_gnt_clientes_season_ctas();
                st.server = ConfigurationManager.AppSettings["server"];
                st.uid = ConfigurationManager.AppSettings["uid"];
                st.pwd = ConfigurationManager.AppSettings["pwd"];
                st.database = ConfigurationManager.AppSettings["database"];
                conectorGNT = new Seasons.Modelos.Conector();
                conectorGNT.server = ConfigurationManager.AppSettings["serverL"];
                conectorGNT.port = Convert.ToInt32(ConfigurationManager.AppSettings["portL"]);
                conectorGNT.login = Convert.ToUInt32(ConfigurationManager.AppSettings["loginL"]);
                conectorGNT.password = ConfigurationManager.AppSettings["passwordL"];
                conectorGNT.servidor = ConfigurationManager.AppSettings["servidorL"];
                conectorGNT.timeout = Convert.ToUInt32(ConfigurationManager.AppSettings["timeoutL"]);
                crud = new Seasons.Modelos.CRUD_gnt_clientes_season_ctas();
                crud.idgnt_clientesInfo = Convert.ToInt32(dynJson1.idgnt_clientesInfo);
                crud.idgnt_seasons = Convert.ToInt32(dynJson1.idgnt_seasons);
                crud.operacion = "V";
                rV = cs.CRUD_gnt_clientes_season_ctas(crud, st);
                r = rV;
                dynamic dynJsonV = JsonConvert.DeserializeObject(r);
                r = dynJsonV[0].mensaje;
                if (r.Equals("Ya tienes una cuenta en esta Seasons"))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, rV);
                }
                else
                {
                    string grupoSeason = dynJson1.group;
                    double balance = dynJson1.balance;
                    uint leverage = dynJson1.leverage;
                    int idgnt_clientesInfo = dynJson1.idgnt_clientesInfo;
                    int idgnt_seasons = dynJson1.idgnt_seasons;
                    string depositText = dynJson1.deposit;
                    string dL = cs.GETDATALIVE(idgnt_clientesInfo, st);
                    dynamic dynJson2 = JsonConvert.DeserializeObject(dL);
                    cuenta.idgnt_clientesInfo = dynJson2[0].idgnt_clientesInfo;
                    cuenta.name = dynJson2[0].Name;
                    cuenta.email = dynJson2[0].Email;
                    cuenta.mainPassword = dynJson2[0].MainPassword;
                    cuenta.phone = dynJson2[0].Phone;
                    cuenta.city = dynJson2[0].City;
                    cuenta.zip = dynJson2[0].ZipCode;
                    cuenta.state = dynJson2[0].State;
                    cuenta.address = dynJson2[0].Address;
                    cuenta.country = dynJson2[0].Country;
                    cuenta.group = grupoSeason;
                    cuenta.balance = balance;
                    cuenta.leverage = leverage;
                    cuenta.tipoServidor = "Live";
                    cuenta.idgnt_seasons = idgnt_seasons;
                    cuenta.depositText = depositText;
                    r = cc.cuentaSalida(cuenta, conectorGNT, st);
                    return Request.CreateResponse(HttpStatusCode.OK, r);
                }                
            }                        
        }

        /*POST CTAS SEASON VALIDAR*/
        [HttpPost, Route("api/real/Cuentas/Password")]
        public HttpResponseMessage cambioPassword([FromBody] Seasons.Modelos.Password pwd)
        {
            string r = null;
            string rV = null;
            var json = "";
            valD = new Seasons.JWT.ValidateToken();
            jwt = new Seasons.JWT.JWTModel();
            jwt.hashCode = valD.validateJWT(Request.Headers.Authorization.Parameter);
            jwt.idgnt_clientesInfo = pwd.idgnt_clientesInfo;
            jwt.tipo = "INFO";
            r = auth.JWT(jwt);
            if (r.Equals("Acceso denegado"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, r);
            }
            else
            {
                Seasons.MT5Ctas.CrearCuenta cs = new Seasons.MT5Ctas.CrearCuenta();
                Seasons.Modelos.StringConect st = new Seasons.Modelos.StringConect();
                Seasons.Modelos.Conector conectorGNT = new Seasons.Modelos.Conector();
                st.server = ConfigurationManager.AppSettings["server"];
                st.uid = ConfigurationManager.AppSettings["uid"];
                st.pwd = ConfigurationManager.AppSettings["pwd"];
                st.database = ConfigurationManager.AppSettings["database"];
                conectorGNT = new Seasons.Modelos.Conector();                
                if (pwd.loginCambio >= 700000)
                {
                    conectorGNT.server = ConfigurationManager.AppSettings["serverL"];
                    conectorGNT.port = Convert.ToInt32(ConfigurationManager.AppSettings["portL"]);
                    conectorGNT.login = Convert.ToUInt32(ConfigurationManager.AppSettings["loginL"]);
                    conectorGNT.password = ConfigurationManager.AppSettings["passwordL"];
                    conectorGNT.servidor = ConfigurationManager.AppSettings["servidorL"];
                    conectorGNT.timeout = Convert.ToUInt32(ConfigurationManager.AppSettings["timeoutL"]);
                    pwd.tipo = "GNTCapital-Live";
                }
                else
                {
                    conectorGNT.server = ConfigurationManager.AppSettings["serverD"];
                    conectorGNT.port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
                    conectorGNT.login = Convert.ToUInt32(ConfigurationManager.AppSettings["login"]);
                    conectorGNT.password = ConfigurationManager.AppSettings["password"];
                    conectorGNT.servidor = ConfigurationManager.AppSettings["servidor"];
                    conectorGNT.timeout = Convert.ToUInt32(ConfigurationManager.AppSettings["timeout"]);
                    pwd.tipo = "GNTCapital-Demo";
                }
                r = cs.cambioPassword(pwd, conectorGNT, st);
                return Request.CreateResponse(HttpStatusCode.OK, r);
            }
            /*Seasons.MT5Ctas.CrearCuenta cs = new Seasons.MT5Ctas.CrearCuenta();
            Seasons.Modelos.StringConect st = new Seasons.Modelos.StringConect();
            Seasons.Modelos.Conector conectorGNT = new Seasons.Modelos.Conector();
            st.server = ConfigurationManager.AppSettings["server"];
            st.uid = ConfigurationManager.AppSettings["uid"];
            st.pwd = ConfigurationManager.AppSettings["pwd"];
            st.database = ConfigurationManager.AppSettings["database"];
            conectorGNT = new Seasons.Modelos.Conector();
            conectorGNT.server = ConfigurationManager.AppSettings["serverD"];
            conectorGNT.port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            conectorGNT.login = Convert.ToUInt32(ConfigurationManager.AppSettings["login"]);
            conectorGNT.password = ConfigurationManager.AppSettings["password"];
            conectorGNT.servidor = ConfigurationManager.AppSettings["servidor"];
            conectorGNT.timeout = Convert.ToUInt32(ConfigurationManager.AppSettings["timeout"]);
            if(pwd.tipo.Equals("real"))
            {
                pwd.tipo = "GNTCapital-Live";
            }
            else
            {
                pwd.tipo = "GNTCapital-Demo";
            }
            r = cs.cambioPassword(pwd, conectorGNT, st);
            return Request.CreateResponse(HttpStatusCode.OK, r);*/
        }
    }
}
