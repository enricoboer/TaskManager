using System;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.Web.Mvc;
using TarefasAcademicas.DataAccess.Model;
using TarefasAcademicas.DataAccess.Repository;
using X.PagedList;

namespace TarefasAcademicas.UI.Controllers
{
    [Authorize]
    public class TarefasController : TarefasAcademicas
    {
        private readonly TarefasRepository _tarefasRepository;

        public TarefasController()
        {
            _tarefasRepository = new TarefasRepository();
        }
        // GET: Tarefas
        public ActionResult Index(int pagina = 1   )
        {
            var tarefas = _tarefasRepository.ObterPorUsuarioId(ObterUserId()).ToPagedList(pagina,5);

            var todos = _tarefasRepository.ObterNumeroTotalTarefas(ObterUserId());

            var foraprazo = _tarefasRepository.ObterNumeroTotalTarefasForadoPrazo(ObterUserId());
            ViewBag.TotalFora = foraprazo;

            foreach (var tarefa in tarefas)
            {
                if (tarefa.DataInicio > tarefa.DataFinal)
                {
                    tarefa.Status = "Fora do Prazo";
                    tarefa.StatusClasse = "ForaPrazo";
                }
                else if(tarefa.DataInicio == tarefa.DataFinal)
                {
                    tarefa.Status = "Dentro do Prazo";
                    tarefa.StatusClasse = "DentroPrazoAviso";
                }
                else
                {
                    tarefa.Status = "Dentro do Prazo";
                    tarefa.StatusClasse = "DentroPrazo";
                }

               
            }

            return View(tarefas);
        }

    

        // GET: Tarefas/Details/5
        public ActionResult Details(Guid id)
        {
            Tarefas tarefas =_tarefasRepository.ObterPorId(id);

            if (tarefas == null)
            {
                return HttpNotFound();
            }
            return View(tarefas);
        }

        // GET: Tarefas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tarefas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tarefas tarefas)
        {
            if (ModelState.IsValid)
            {
                tarefas.UsuarioId = ObterUserId();
               _tarefasRepository.Adicionar(tarefas);
                return RedirectToAction("Index");
            }

            return View(tarefas);
        }

        // GET: Tarefas/Edit/5
        public ActionResult Edit(Guid id)
        {
            Tarefas tarefas =_tarefasRepository.ObterPorId(id);
            if (tarefas == null)
            {
                return HttpNotFound();
            }
            return View(tarefas);
        }

        // POST: Tarefas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tarefas tarefas)
        {
            if (ModelState.IsValid)
            {
               _tarefasRepository.Atualizar(tarefas);
                return RedirectToAction("Index");
            }
            return View(tarefas);
        }

        // GET: Tarefas/Delete/5
        public ActionResult Delete(Guid id)
        {
            Tarefas tarefas =_tarefasRepository.ObterPorId(id);
            if (tarefas == null)
            {
                return HttpNotFound();
            }
            return View(tarefas);
        }

        // POST: Tarefas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
           _tarefasRepository.Deletar(id);
            return RedirectToAction("Index");
        }

       

      
    }
}
