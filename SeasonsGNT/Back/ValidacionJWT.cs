using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SeasonsGNT.Back
{
    public class ValidacionJWT
    {
        Seasons.JWT.JWTValidacion auth = new Seasons.JWT.JWTValidacion();
        Seasons.Modelos.StringConect st = new Seasons.Modelos.StringConect();

        public string JWT(Seasons.JWT.JWTModel jwt)
        {
            string salida = null;
            st.server = ConfigurationManager.AppSettings["server"];
            st.uid = ConfigurationManager.AppSettings["uid"];
            st.pwd = ConfigurationManager.AppSettings["pwd"];
            st.database = ConfigurationManager.AppSettings["database"];
            salida = auth.validacionMDB(jwt, st);
            return salida;
        }
    }
}