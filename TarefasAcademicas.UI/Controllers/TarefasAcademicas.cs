using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace TarefasAcademicas.UI.Controllers
{
    public class TarefasAcademicas : Controller
    {
        protected Guid ObterUserId()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var usuario = userManager.FindByEmail(User.Identity.Name);

            return new Guid(usuario.Id);
        }
    }
}