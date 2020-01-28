using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LigaSurTulcan.Models.ViewModels
{
    public class CampeonatoViewModel
    {
        public int Id_campeonato { get; set; }
        [Required]
        [Display(Name = "Campeonato")]
        public string Nom_Campeonato { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Inicio")]
        public DateTime fecha_ini { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Fin")]
        public DateTime fecha_fin { get; set; }
        [Display(Name ="Estado Campeonato")]
        public string Estado_campeonato { get; set; }
    }
}
    
