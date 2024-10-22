using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Gestion.Dtos
{
    public class UserDto
    {
            public int Id_usuario { get; set; }
            public int Nit { get; set; } 
            public string Tipo_docu { get; set; } = string.Empty;
            public string Nombres { get; set; } = string.Empty;
            public string Apellidos { get; set; } = string.Empty;
            public string Contrasenia { get; set; } = string.Empty;
            public int Response { get; set; }
            public string Mensaje { get; set; } = string.Empty;
     }
}