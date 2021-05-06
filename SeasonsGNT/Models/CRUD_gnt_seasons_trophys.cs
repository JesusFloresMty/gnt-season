using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeasonsGNT.Models
{
    public class CRUD_gnt_seasons_trophys
    {
        public double balance { get; set; }
        public int idgnt_seasons { get; set; }
        public int idgnt_seasons_trophys { get; set; }
        public double lotes { get; set; }
        public double lotes_tc { get; set; }
        public string operacion { get; set; }
        public int posiciones { get; set; }
        public int simbolos { get; set; }
    }
}