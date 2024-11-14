using Proyecto_Gestion.Repositories;
using Proyecto_Gestion.Services;
using Proyecto_Gestion.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Gestion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ReportService _reportService;

        public HomeController()
        {
            _reportService = new ReportService();
        }

        public ActionResult Index()
        {
            return View();
        }

<<<<<<< Updated upstream
=======
        public ActionResult Report()
        {
            return View();
        }

        public ActionResult DownloadReport()
        {
            var reportData = _reportService.GetReportData();
            return ReportUtility.GeneratePdf(this.ControllerContext, reportData);
        }
    

>>>>>>> Stashed changes
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}