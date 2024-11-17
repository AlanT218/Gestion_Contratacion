using Proyecto_Gestion.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Gestion.Services
{
    public class ReportService
    {
        private readonly UserService _userService;

        public ReportService()
        {
            _userService = new UserService();
        }

        public ReportDto GetReportData()
        {
            var (aceptados, rechazados, candidatos, empleados) = _userService.GetCandidatos();
            return new ReportDto
            {
                Title = "Reporte de Usuarios",
                ReportDate = DateTime.Now,
                Aceptados = aceptados,
                Rechazados = rechazados,
                Candidatos = candidatos
            };
        }
    }
}