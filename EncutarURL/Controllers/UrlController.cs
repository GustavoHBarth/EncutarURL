using EncutarURL.Data;
using EncutarURL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace EncutarURL.Controllers
{
    public class UrlController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _baseUrl = "https://localhost:7121/";  // Base URL do seu sistema

        public UrlController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Ação para mostrar o formulário de criação
        [HttpGet]
        public IActionResult Index()
        {
            return View(); // Exibe o formulário de entrada da URL original
        }

        // Ação para criar a URL encurtada
        [HttpPost]
        public IActionResult Create(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
            {
                ViewData["ErrorMessage"] = "A URL original não pode ser vazia.";
                return View("Index"); // Retorna para a página inicial com a mensagem de erro
            }

            var shortenedUrl = GenerateShortenedUrl();

            // Cria a URL no banco de dados
            var url = new Url(originalUrl)
            {
                ShortenedUrl = shortenedUrl,
                CreatedAt = DateTime.Now
            };

            _context.Urls.Add(url);
            _context.SaveChanges();

            // Redireciona para a página de URL encurtada
            ViewData["OriginalUrl"] = originalUrl;
            ViewData["ShortenedUrl"] = $"{_baseUrl}{shortenedUrl}";  // Exibe a URL encurtada completa

            return View("Shortened"); // Exibe a view "Shortened"
        }

        // Ação para exibir a URL encurtada
        public IActionResult Shortened()
        {
            return View(); // Exibe a view de URL encurtada com os dados no ViewData
        }

        // Método para gerar a URL encurtada
        private string GenerateShortenedUrl()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Range(0, 6).Select(_ => chars[random.Next(chars.Length)]).ToArray());
        }

        // Ação para redirecionar para a URL original
        [HttpGet("{shortenedUrl}")]
        public IActionResult RedirectToOriginal(string shortenedUrl)
        {
            var url = _context.Urls.FirstOrDefault(u => u.ShortenedUrl == shortenedUrl);

            if (url == null)
            {
                return NotFound(); // Se não encontrar a URL, retorna 404
            }

            // Redireciona para a URL original
            return Redirect(url.OriginalUrl);
        }
    }
}
