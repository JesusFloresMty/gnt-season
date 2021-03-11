using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SeasonsGNT.Back
{
    public class TrophyHelper
    {
        public string getUsersTrophy(string entrada, int idLogin, int posit)
        {
            string salida = null;            
            var json = "";
            int tLotes = 0;
            int tLotesTC = 0;
            int tBalance = 0;
            int tPosiciones = 0;
            int tSimbolos = 0;
            int tPro = 0;
            int tSeason = 0;
            int totalT = 0;
            double lotes = Convert.ToDouble(ConfigurationManager.AppSettings["lotes"]);
            double lotesTC = Convert.ToDouble(ConfigurationManager.AppSettings["lotesTC"]);
            double balance = Convert.ToDouble(ConfigurationManager.AppSettings["balance"]);
            int posiciones = Convert.ToInt32(ConfigurationManager.AppSettings["posiciones"]);
            string simbolos = ConfigurationManager.AppSettings["simbolos"];
            string pro = ConfigurationManager.AppSettings["pro"];
            string season = ConfigurationManager.AppSettings["season"];
            Usuarios us = new Usuarios();
            List<Usuarios> lista = new List<Usuarios>();
            MyTrophy my = new MyTrophy();
            MyTrophyU myU = new MyTrophyU();
            List<MyTrophyU> listaT = new List<MyTrophyU>();
            lista.Clear();
            listaT.Clear();
            dynamic dynJson = JsonConvert.DeserializeObject(entrada);
            if(idLogin == 0)
            {
                foreach (var s in dynJson)
                {
                    /*us = new Usuarios();
                    us.usuario = s.Name;*/
                    myU = new MyTrophyU();
                    myU.login = s.Login;
                    myU.nombre = s.Name;
                    myU.balanceU = s.BalanceU;
                    myU.equidad = s.Equity;
                    if (s.Lotes >= lotes)
                    {
                        myU.lotes = true;
                        tLotes = 1;
                    }
                    else
                    {
                        myU.lotes = false;
                    }
                    /*TROFEO NUEVO*/
                    if (s.OpCTAS >= lotesTC)
                    {
                        myU.lotesTC = true;
                        tLotesTC = 1;
                    }
                    else
                    {
                        myU.lotes = false;
                    }
                    /*TROFEO NUEVO*/
                    if (s.Balance >= balance)
                    {
                        myU.balanceT = true;
                        tBalance = 1;
                    }
                    else
                    {
                        myU.balanceT = false;
                    }
                    if (s.Posiciones >= posiciones)
                    {
                        myU.posiciones = true;
                        tPosiciones = 1;
                    }
                    else
                    {
                        myU.posiciones = false;
                    }
                    if (s.Simbolos == simbolos)
                    {
                        myU.simbolos = true;
                        tSimbolos = 1;
                    }
                    else
                    {
                        myU.simbolos = false;
                    }
                    if (s.Pro == pro)
                    {
                        myU.pro = true;
                        tPro = 1;
                    }
                    else
                    {
                        myU.pro = false;
                    }
                    if (s.Season == season)
                    {
                        myU.season = true;
                        tSeason = 1;
                    }
                    else
                    {
                        myU.season = false;
                    }
                    myU.totalT = tLotes + tLotesTC + tBalance + tPosiciones + tSimbolos + tPro + tSeason;
                    tLotes = 0;
                    tLotesTC = 0;
                    tBalance = 0;
                    tPosiciones = 0;
                    tSimbolos = 0;
                    tPro = 0;
                    tSeason = 0;
                    listaT.Add(myU);
                }

                var listaUT = listaT.GroupBy(x => new { x.login, x.nombre, x.equidad, x.totalT, x.balanceU})
                    .Select(grp => new
                    {
                        nombre = grp.Key.nombre,
                        balance = grp.Key.balanceU,
                        equidad = grp.Key.equidad,
                        total = grp.Key.totalT,
                        login = grp.Key.login
                    }).OrderByDescending(item => item.total).Take(25).ToList();
                var listaUTB = (from lut in listaUT orderby lut.total descending, lut.balance descending select lut).ToList();
                trphysPosition pos = new trphysPosition();
                List<trphysPosition> listaPos = new List<trphysPosition>();
                listaPos.Clear();
                int positionT = 0;
                foreach (var p in listaUTB)
                {
                    positionT = positionT + 1;
                    pos = new trphysPosition();
                    pos.id = positionT;
                    pos.nombre = p.nombre;
                    pos.balance = p.balance;
                    pos.equidad = p.equidad;
                    pos.total = p.total;
                    pos.login = p.login;
                    listaPos.Add(pos);
                }
                json = JsonConvert.SerializeObject(listaPos);
            }
            else
            {
                my = new MyTrophy();
                foreach (var z in dynJson)
                {
                    my.id = posit; 
                    my.login = z.Login;
                    my.balanceU = z.BalanceU;
                    my.equidad = z.Equity;
                    if(z.Lotes >= lotes)
                    {
                        my.lotes = true;
                    }
                    else
                    {
                        my.lotes = false;
                    }
                    /*TROFEO NUEVO*/
                    if (z.OpCTAS >= lotesTC)
                    {
                        my.lotesTC = true;
                    }
                    else
                    {
                        myU.lotes = false;
                    }
                    /*TROFEO NUEVO*/
                    if (z.Balance >= balance)
                    {
                        my.balanceT = true;
                    }
                    else
                    {
                        my.balanceT = false;
                    }
                    if (z.Posiciones >= posiciones)
                    {
                        my.posiciones = true;
                    }
                    else
                    {
                        my.posiciones = false;
                    }
                    if (z.Simbolos == simbolos)
                    {
                        my.simbolos = true;
                    }
                    else
                    {
                        my.simbolos = false;
                    }
                    if (z.Pro == pro)
                    {
                        my.pro = true;
                    }
                    else
                    {
                        my.pro = false;
                    }
                    if (z.Season == season)
                    {
                        my.season = true;
                    }
                    else
                    {
                        my.season = false;
                    }
                }
                /*
                 *  La variable lu
                 */

                json = JsonConvert.SerializeObject(my);
            }                                     
            salida = json;
            return salida;
        }

        public string postTrofeos(string entrada, int idLogin)
        {
            string salida = null;
            var json = "";
            int tLotes = 0;
            int tBalance = 0;
            int tPosiciones = 0;
            int tSimbolos = 0;
            int tPro = 0;
            int tSeason = 0;
            int totalT = 0;
            double lotes = Convert.ToDouble(ConfigurationManager.AppSettings["lotes"]);
            double balance = Convert.ToDouble(ConfigurationManager.AppSettings["balance"]);
            int posiciones = Convert.ToInt32(ConfigurationManager.AppSettings["posiciones"]);
            string simbolos = ConfigurationManager.AppSettings["simbolos"];
            string pro = ConfigurationManager.AppSettings["pro"];
            string season = ConfigurationManager.AppSettings["season"];
            Usuarios us = new Usuarios();
            List<Usuarios> lista = new List<Usuarios>();
            MyTrophy my = new MyTrophy();
            MyTrophyU myU = new MyTrophyU();
            List<MyTrophyU> listaT = new List<MyTrophyU>();
            lista.Clear();
            listaT.Clear();
            dynamic dynJson = JsonConvert.DeserializeObject(entrada);
            if (idLogin == 0)
            {
                foreach (var s in dynJson)
                {
                    /*us = new Usuarios();
                    us.usuario = s.Name;*/
                    myU = new MyTrophyU();
                    myU.login = s.Login;
                    myU.nombre = s.Name;
                    myU.balanceU = s.BalanceU;
                    myU.equidad = s.Equity;
                    if (s.Lotes >= lotes)
                    {
                        myU.lotes = true;
                        tLotes = 1;
                    }
                    else
                    {
                        myU.lotes = false;
                    }
                    if (s.Balance >= balance)
                    {
                        myU.balanceT = true;
                        tBalance = 1;
                    }
                    else
                    {
                        myU.balanceT = false;
                    }
                    if (s.Posiciones >= posiciones)
                    {
                        myU.posiciones = true;
                        tPosiciones = 1;
                    }
                    else
                    {
                        myU.posiciones = false;
                    }
                    if (s.Simbolos == simbolos)
                    {
                        myU.simbolos = true;
                        tSimbolos = 1;
                    }
                    else
                    {
                        myU.simbolos = false;
                    }
                    if (s.Pro == pro)
                    {
                        myU.pro = true;
                        tPro = 1;
                    }
                    else
                    {
                        myU.pro = false;
                    }
                    if (s.Season == season)
                    {
                        myU.season = true;
                        tSeason = 1;
                    }
                    else
                    {
                        myU.season = false;
                    }
                    myU.totalT = tLotes + tBalance + tPosiciones + tSimbolos + tPro + tSeason;
                    tLotes = 0;
                    tBalance = 0;
                    tPosiciones = 0;
                    tSimbolos = 0;
                    tPro = 0;
                    tSeason = 0;
                    listaT.Add(myU);
                }

                var listaUT = listaT.GroupBy(x => new { x.login, x.nombre, x.equidad, x.totalT, x.balanceU })
                    .Select(grp => new
                    {
                        nombre = grp.Key.nombre,
                        balance = grp.Key.balanceU,
                        equidad = grp.Key.equidad,
                        total = grp.Key.totalT,
                        login = grp.Key.login
                    }).OrderByDescending(item => item.total).ToList();
                trphysPosition pos = new trphysPosition();
                List<trphysPosition> listaPos = new List<trphysPosition>();
                listaPos.Clear();
                int positionT = 0;
                foreach (var p in listaUT)
                {
                    positionT = positionT + 1;
                    pos = new trphysPosition();
                    pos.id = positionT;
                    pos.nombre = p.nombre;
                    pos.balance = p.balance;
                    pos.equidad = p.equidad;
                    pos.total = p.total;
                    pos.login = p.login;
                    listaPos.Add(pos);
                }
                json = JsonConvert.SerializeObject(listaPos);
            }
            salida = json;
            return salida;
        }

        public string getUsersTrophys(int idgnt_seasons, int idLogin)
        {
            string r = null;
            Seasons.SeasonMDB.Trophy cs = new Seasons.SeasonMDB.Trophy();
            Seasons.Modelos.StringConect st = new Seasons.Modelos.StringConect();
            st.server = ConfigurationManager.AppSettings["server"];
            st.uid = ConfigurationManager.AppSettings["uid"];
            st.pwd = ConfigurationManager.AppSettings["pwd"];
            st.database = ConfigurationManager.AppSettings["database"];
            r = cs.GETTROFEOS(idgnt_seasons, idLogin, st);
            r = getUsersTrophy(r, idLogin, 0);
            return r;
        }

        public class Usuarios
        {
            public string usuario { get; set; }
        }

        public class MyTrophy
        {
            public int id { get; set; }
            public int login { get; set; }
            public double balanceU { get; set; }
            public double equidad { get; set; }
            public bool lotes { get; set; }
            public bool lotesTC { get; set; }
            public bool balanceT { get; set; }
            public bool posiciones { get; set; }
            public bool simbolos { get; set; }
            public bool pro { get; set; }
            public bool season { get; set; }
        }

        public class MyTrophyU
        {
            public int login { get; set; }
            public string nombre { get; set; }
            public double balanceU { get; set; }
            public double equidad { get; set; }
            public bool lotes { get; set; }
            public bool lotesTC { get; set; }
            public bool balanceT { get; set; }
            public bool posiciones { get; set; }
            public bool simbolos { get; set; }
            public bool pro { get; set; }
            public bool season { get; set; }
            public int totalT { get; set; }
        }

        public class trphysPosition
        {
            /*nombre = grp.Key.nombre,
                        balance = grp.Key.balanceU,
                        equidad = grp.Key.equidad,
                        total = grp.Key.totalT*/
            public int id { get; set; }
            public string nombre { get; set; }
            public double balance { get; set; }
            public double equidad { get; set; }
            public int total { get; set; }
            public int login { get; set; }
        }
    }
}