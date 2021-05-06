using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SeasonsGNT.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace SeasonsGNT.Back
{
    public class GNTMDBTrophys
    {

        private MySqlConnection conn;
        public String mysqlString;

        public GNTMDBTrophys()
        {
            //mysqlString = "Server=127.0.0.1;uid=root;pwd=admin;database=gnt_mdb;SslMode=none;Max Pool Size=50000;Pooling=True;";
            mysqlString = ConfigurationManager.AppSettings["GNTMDBTrophys"];
            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = mysqlString;
                conn.Open();
                conn.Close();
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.ToString());
            }
        }

        public string CRUD_gnt_seasons_trophys(CRUD_gnt_seasons_trophys crud)
        {
            string result = null;
            try
            {
                MySqlConnection mySqlConnection = new MySqlConnection(this.mysqlString);
                MySqlCommand mySqlCommand = new MySqlCommand("CRUD_gnt_seasons_trophys", new MySqlConnection(this.mysqlString));
                mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("idgnt_seasons_trophys", crud.idgnt_seasons_trophys));
                mySqlCommand.Parameters.Add(new MySqlParameter("idgnt_seasons", crud.idgnt_seasons));
                mySqlCommand.Parameters.Add(new MySqlParameter("lotes", crud.lotes));
                mySqlCommand.Parameters.Add(new MySqlParameter("lotes_tc", crud.lotes_tc));
                mySqlCommand.Parameters.Add(new MySqlParameter("balance", crud.balance));
                mySqlCommand.Parameters.Add(new MySqlParameter("posiciones", crud.posiciones));
                mySqlCommand.Parameters.Add(new MySqlParameter("simbolos", crud.simbolos));
                mySqlCommand.Parameters.Add(new MySqlParameter("operacion", crud.operacion));
                mySqlCommand.Connection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                mySqlCommand.Connection = conn;
                while (mySqlDataReader.Read())
                {
                    result = mySqlDataReader["mensaje"].ToString();
                }
                mySqlDataReader.Close();
                mySqlConnection.Close();
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }

        public string SELECT_gnt_seasons_trophys()
        {
            string result = null;
            GNTMDBTrophys.Trophys trophys = new GNTMDBTrophys.Trophys();
            string text = ConfigurationManager.AppSettings["LotsDescESP"];
            string text2 = ConfigurationManager.AppSettings["LotsDescENG"];
            string text3 = ConfigurationManager.AppSettings["LotsAccountsDescESP"];
            string text4 = ConfigurationManager.AppSettings["LotsAccountsDescENG"];
            string text5 = ConfigurationManager.AppSettings["BalanceDescESP"];
            string text6 = ConfigurationManager.AppSettings["BalanceDescENG"];
            string text7 = ConfigurationManager.AppSettings["PositionsDescESP"];
            string text8 = ConfigurationManager.AppSettings["PositionsDescENG"];
            try
            {
                MySqlConnection mySqlConnection = new MySqlConnection(this.mysqlString);
                MySqlCommand mySqlCommand = new MySqlCommand("SELECT_gnt_seasons_trophys", new MySqlConnection(this.mysqlString));
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Connection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                mySqlCommand.Connection = conn;
                while (mySqlDataReader.Read())
                {
                    trophys = new GNTMDBTrophys.Trophys();
                    trophys.idgnt_seasons = Convert.ToInt32(mySqlDataReader["idgnt_seasons"].ToString());
                    trophys.Lots = Convert.ToDouble(mySqlDataReader["Lots"].ToString());
                    trophys.lotes_es = text.Replace("%", trophys.Lots.ToString());
                    trophys.lotes_en = text2.Replace("%", trophys.Lots.ToString());
                    trophys.LotsAccounts = Convert.ToDouble(mySqlDataReader["LotsAccounts"].ToString());
                    trophys.lotesTC_es = text3.Replace("%", trophys.LotsAccounts.ToString());
                    trophys.lotesTC_en = text4.Replace("%", trophys.LotsAccounts.ToString());
                    trophys.Balance = Convert.ToDouble(mySqlDataReader["Balance"].ToString());
                    trophys.balanceT_en = text6.Replace("%", trophys.Balance.ToString());
                    trophys.balanceT_es = text5.Replace("%", trophys.Balance.ToString());
                    trophys.Positions = Convert.ToDouble(mySqlDataReader["Balance"].ToString());
                    trophys.posiciones_en = text8.Replace("%", trophys.Positions.ToString());
                    trophys.posiciones_es = text7.Replace("%", trophys.Positions.ToString());
                    trophys.Symbol_Es = mySqlDataReader["Symbol_Es"].ToString();
                    trophys.Symbol_En = mySqlDataReader["Symbol_En"].ToString();
                    trophys.pro_es = mySqlDataReader["Pro_Es"].ToString();
                    trophys.pro_en = mySqlDataReader["Pro_En"].ToString();
                    trophys.season_es = mySqlDataReader["Season_Es"].ToString();
                    trophys.season_en = mySqlDataReader["Season_En"].ToString();
                    trophys.DateINI = mySqlDataReader["DateINI"].ToString();
                    trophys.DateEND = mySqlDataReader["DateEND"].ToString();
                }
                mySqlDataReader.Close();
                mySqlConnection.Close();
                string text9 = JsonConvert.SerializeObject(trophys);
                result = text9;
            }
            catch (Exception e)
            {
            }
            return result;
        }

        public string setJSON(MySqlDataReader mdr)
        {
            string empty = string.Empty;
            DataTable dataTable = new DataTable();
            dataTable.Load(mdr);
            return JsonConvert.SerializeObject(dataTable);
        }

        public class Trophys
        {
            public double Balance { get; set; }
            public string balanceT_en { get; set; }
            public string balanceT_es { get; set; }
            public string DateEND { get; set; }
            public string DateINI { get; set; }
            public int idgnt_seasons { get; set; }
            public string lotesTC_en { get; set; }
            public string lotesTC_es { get; set; }
            public string lotes_en { get; set; }
            public string lotes_es { get; set; }
            public double Lots { get; set; }
            public double LotsAccounts { get; set; }
            public string posiciones_en { get; set; }
            public string posiciones_es { get; set; }
            public double Positions { get; set; }
            public string pro_en { get; set; }
            public string pro_es { get; set; }
            public string season_en { get; set; }
            public string season_es { get; set; }
            public string Symbol_En { get; set; }
            public string Symbol_Es { get; set; }
        }
    }
}