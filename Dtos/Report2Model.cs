using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Gestion.Dtos
{
    public class Report2Model
    {
        public List<Report2Model> List { get; set; }
        public string Generador { get; set; }
        public string Fecha { get; set; }
    } 
}