using Compline.Teste.Commons.Entidades;
using Compline.Teste.Commons.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Compline.Teste.MVC.Services
{
    public class TarefasService : IService<Tarefa>
    {
        public async Task Add(Tarefa t)
        {
            using (HttpClient client = new HttpClient())
            {
                var endpoint = ConfigurationManager.AppSettings["apiTarefas"];

                using (HttpResponseMessage response = await client.PostAsJsonAsync<Tarefa>(endpoint, t))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new Exception("Falha em adicionar uma nova Tarefa");
                }
            }
        }

        public async Task<Tarefa> FindById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var endpoint = string.Format("{0}/{1}", ConfigurationManager.AppSettings["apiTarefas"], id);

                using (HttpResponseMessage response = await client.GetAsync(endpoint))
                {
                    var jsonLista = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<Tarefa>(jsonLista);
                }
            }
        }

        public async Task<IEnumerable<Tarefa>> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                var endpoint = ConfigurationManager.AppSettings["apiTarefas"];

                using (HttpResponseMessage response = await client.GetAsync(endpoint))
                {
                    var jsonListas = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<List<Tarefa>>(jsonListas);
                }
            }
        }

        public async Task Remove(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var endpoint = string.Format("{0}/{1}", ConfigurationManager.AppSettings["apiTarefas"], id);

                using (HttpResponseMessage reponse = await client.DeleteAsync(endpoint))
                {
                    if (!reponse.IsSuccessStatusCode)
                        throw new Exception("Falha ao remover");
                }
            }
        }

        public async Task Update(Tarefa t)
        {
            using (HttpClient client = new HttpClient())
            {
                var endpoint = ConfigurationManager.AppSettings["apiTarefas"];

                using (HttpResponseMessage response = await client.PutAsJsonAsync<Tarefa>(endpoint, t))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new Exception("Falha ao atualizar os dados");
                }
            }
        }
    }
}