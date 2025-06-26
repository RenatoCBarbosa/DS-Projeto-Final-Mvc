using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoFinalMvc.Models;
using System.Collections.Generic;


namespace ProjetoFinalMvc.Controllers
{
    
    
    public class LocacaoController : Controller
    {
        private readonly string _apiUrl = "https://APIFINAL-OS-RENATO.msagi.somex.com/api/Locacao/";


        private readonly ApplicationDbContext? _context;

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(_apiUrl + "GetAll");

                if (!response.IsSuccessStatusCode)
                {
                    TempData["MensagemErro"] = "Erro ao acessar a API";
                    return View(new List<LocacaoViewModel>());
                }

                var content = await response.Content.ReadAsStringAsync();
                var locacoes = JsonConvert.DeserializeObject<List<LocacaoViewModel>>(content)
                    ?? new List<LocacaoViewModel>();

                return View(locacoes);
            }
            catch (Exception ex)
            {

                TempData["MensagemErro"] = $"Erro: {ex.Message}";
                return View(new List<LocacaoViewModel>());
            }
        }

        [HttpPost]
        public IActionResult Create()
        {
            // Isso procura por Views/Locacao/Create.cshtml
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocacaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var locacao = new Locacao
                {
                    Nome = model.Nome,
                    IdUsuario = model.IdUsuario,

                };

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(LocacaoViewModel locacao)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Create", locacao);

                using var client = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(locacao),
                    Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_apiUrl + "Registrar", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["MensagemSucesso"] = $"Locação {locacao.Nome} registrada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                TempData["MensagemErro"] = $"Erro na API: {errorContent}";
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro: {ex.Message}";
            }

            return View("Create", locacao);
        }
    }

    internal class Locacao
    {
        public string Nome { get; set; }
        public int IdUsuario { get; set; }
    }

    internal class ApplicationDbContext
    {
        internal async Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}