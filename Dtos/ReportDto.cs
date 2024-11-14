using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Gestion.Dtos
{
    public class ReportDto
    {
        public string Title { get; set; }
        public DateTime ReportDate { get; set; }
        public List<UserDto> Aceptados { get; set; }
        public List<UserDto> Rechazados { get; set; }
        public List<UserDto> Candidatos { get; set; }
    }
}
