using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TarefasAcademicas.DataAccess.Repository;

namespace TarefasAcademicas.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;

        public HomeController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        public ActionResult Index()

        {


            return View();
        }

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