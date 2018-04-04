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
    public class ListaDeTarefasController : Controller
    {
        private ListaTarefasService Service;
        private TarefasService TarefasService;

        public ListaDeTarefasController()
        {
            this.Service = new ListaTarefasService();
            this.TarefasService = new TarefasService();
        }

        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ListaDeTarefas lista)
        {
            if (ModelState.IsValid)
            {
                await this.Service.Add(lista);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Form", lista);
            }
            
        }

        // GET: ListaDeTarefas
        public async Task<ActionResult> Index()
        {
            var listas = await this.Service.GetAll();

            return View(listas);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var toUpdate = await this.Service.FindById(id);

            return View(toUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ListaDeTarefas lista)
        {
            if (ModelState.IsValid)
            {
                await this.Service.Update(lista);

                return RedirectToAction("Index");
            }

            return View("Edit",lista);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var lista = await this.Service.FindById(id);
            var tarefas = await this.TarefasService.GetAll();

            foreach (var tarefa in tarefas)
            {
                if (tarefa.ListaDeTarefasId.Equals(lista.Id))
                    lista.Tarefas.Add(tarefa);
            }

            return View(lista);
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmDelete(ListaDeTarefas lista)
        {
            if (lista.Tarefas.Any())
            {
                foreach(var tarefa in lista.Tarefas)
                {
                    await this.TarefasService.Remove(tarefa.Id);
                }
            }

            await this.Service.Remove(lista.Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var lista = await this.Service.FindById(id);
            var tarefas = await this.TarefasService.GetAll();

            foreach(var tarefa in tarefas)
            {
                if (tarefa.ListaDeTarefasId.Equals(lista.Id))
                    lista.Tarefas.Add(tarefa);
            }

            return View(lista);
        }
    }
}