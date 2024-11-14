using Proyecto_Gestion.Dtos;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Gestion.Utilities
{

    public static class ReportUtility
    {
        public static ActionResult GeneratePdf(ControllerContext context, ReportDto reportData)
        {
            return new ViewAsPdf("Report", reportData)
            {
                FileName = $"Reporte_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Portrait,
                CustomSwitches = "--footer-right [page]/[toPage] --footer-line --footer-font-size 8 --footer-spacing 10 --footer-font-name Arial"
            };
        }
    }
}
