using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LigaSurTulcan.Models.ViewModels
{
    public class ListCampeonatoViewModel
    {
        public int Id_campeonato { get; set; }
        public string Nom_Campeonato { get; set; }
        public Nullable<System.DateTime> fecha_ini { get; set; }
        public Nullable<System.DateTime> fecha_fin { get; set; }
        public string Estado_campeonato { get; set; }
    }
}