using Compline.Teste.Commons.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compline.Teste.Commons.Interfaces;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using Newtonsoft.Json;

namespace Compline.Teste.MVC.Services
{
    public class ListaTarefasService : IService<ListaDeTarefas>
    {
        public async Task Add(ListaDeTarefas t)
        {
            using(HttpClient client = new HttpClient())
            {
                var endpoint = ConfigurationManager.AppSettings["apiListaTarefas"];

                using (HttpResponseMessage response = await client.PostAsJsonAsync<ListaDeTarefas>(endpoint, t))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new Exception("Falha em adicionar uma nova lista de tarefas");               
                }
            }
        }

        public async Task<ListaDeTarefas> FindById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var endpoint = string.Format("{0}/{1}", ConfigurationManager.AppSettings["apiListaTarefas"], id);

                using (HttpResponseMessage response = await client.GetAsync(endpoint))
                {
                    var jsonLista = response.Content.ReadAsStringAsync().Result;

                    var lista =  JsonConvert.DeserializeObject<ListaDeTarefas>(jsonLista);
                    lista.Tarefas = new List<Tarefa>();

                    return lista;
                }
            }
        }

        public async Task<IEnumerable<ListaDeTarefas>> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                var endpoint = ConfigurationManager.AppSettings["apiListaTarefas"];

                using (HttpResponseMessage response = await client.GetAsync(endpoint))
                {
                    var jsonListas = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<List<ListaDeTarefas>>(jsonListas);
                }
            }
        }

        public async Task Remove(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var endpoint = string.Format("{0}/{1}", ConfigurationManager.AppSettings["apiListaTarefas"], id);

                using (HttpResponseMessage reponse = await client.DeleteAsync(endpoint))
                {
                    if (!reponse.IsSuccessStatusCode)
                        throw new Exception("Falha ao remover");
                }
            }
        }

        public async Task Update(ListaDeTarefas t)
        {
            using (HttpClient client = new HttpClient())
            {
                var endpoint = ConfigurationManager.AppSettings["apiListaTarefas"];

                using (HttpResponseMessage response = await client.PutAsJsonAsync<ListaDeTarefas>(endpoint,t))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new Exception("Falha ao atualizar os dados");
                }
            }
        }
    }
}