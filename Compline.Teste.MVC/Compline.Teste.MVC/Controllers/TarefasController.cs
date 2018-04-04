using Compline.Teste.Commons.Entidades;
using Compline.Teste.MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Compline.Teste.MVC.Controllers
{
    public class TarefasController : Controller
    {
        private TarefasService TarefasService;
        private ListaTarefasService ListaDeTarefasService;
        private PrioridadesService PrioridadesService;

        public TarefasController()
        {
            this.TarefasService = new TarefasService();
            this.ListaDeTarefasService = new ListaTarefasService();
            this.PrioridadesService = new PrioridadesService();
        }

        // GET: Tarefas
        public async Task<ActionResult> Index(int idLista)
        {
            var lista = await this.ListaDeTarefasService.FindById(idLista);
            var tarefas = await this.TarefasService.GetAll();

            foreach(var tarefa in tarefas)
            {
                tarefa.Prioridade = await this.PrioridadesService.FindById(tarefa.PrioridadeId);

                if (tarefa.ListaDeTarefasId.Equals(lista.Id))
                {
                    lista.Tarefas.Add(tarefa);
                }
            }

            return View(lista);
        }

        [HttpGet]
        public async Task<ActionResult> Form(int idLista)
        {
            var tarefa = new Tarefa
            {
                ListaDeTarefasId = idLista
            };

            ViewBag.Prioridades = await this.PrioridadesService.GetAll();

            return View(tarefa); 
        }

        [HttpPost]
        public async Task<ActionResult> Create(Tarefa tarefa) 
        {
            if (ModelState.IsValid)
            {
                await this.TarefasService.Add(tarefa);

                return RedirectToAction("Index", new { idLista = tarefa.ListaDeTarefasId });
            }
            else
            {
                ViewBag.Prioridades = await this.PrioridadesService.GetAll();

                return View("Form", tarefa);

            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var toUpdate = await this.TarefasService.FindById(id);

            ViewBag.Prioridades = await this.PrioridadesService.GetAll();

            return View(toUpdate);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                await this.TarefasService.Update(tarefa);

                return RedirectToAction("Index", new { idLista = tarefa.ListaDeTarefasId });
            }
            else
            {
                ViewBag.Prioridades = await this.PrioridadesService.GetAll();
                return View("Edit", tarefa);
            }

        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var toDelete = await this.TarefasService.FindById(id);

            toDelete.Prioridade = await this.PrioridadesService.FindById(toDelete.PrioridadeId);
            toDelete.ListaDeTarefas = await this.ListaDeTarefasService.FindById(toDelete.ListaDeTarefasId);

            return View(toDelete);
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmDelete(Tarefa tarefa)
        {
            await this.TarefasService.Remove(tarefa.Id);

            return RedirectToAction("Index", new { idLista = tarefa.ListaDeTarefasId });
        }

        public async Task<ActionResult> Details(int id)
        {
            var tarefa = await this.TarefasService.FindById(id);

            tarefa.Prioridade = await this.PrioridadesService.FindById(tarefa.PrioridadeId);
            tarefa.ListaDeTarefas = await this.ListaDeTarefasService.FindById(tarefa.ListaDeTarefasId);

            return View(tarefa);
        }

    }
}