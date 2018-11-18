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
    public class HomeController : TarefasAcademicas
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly TarefasRepository _tarefasRepository;

        public HomeController()
        {
            _usuarioRepository = new UsuarioRepository();
            _tarefasRepository = new TarefasRepository();
        }

        public ActionResult Index()

        {
            if (User.Identity.IsAuthenticated)
            {
                var numero = _tarefasRepository.ObterNumeroTotalTarefas(ObterUserId());
                ViewBag.NumeroTotal = numero;

                var foraprazo = _tarefasRepository.ObterNumeroTotalTarefasForadoPrazo(ObterUserId());
                ViewBag.TotalFora = foraprazo;

                var dentroprazo = _tarefasRepository.ObterNumeroTotalTarefasDentrodoPrazo(ObterUserId());
                ViewBag.TotalDentro = dentroprazo;
                
                var igualprazo = _tarefasRepository.ObterNumeroTotalTarefasIgualPrazo(ObterUserId());
                ViewBag.TotalIgual = igualprazo;

                return View();

            }

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