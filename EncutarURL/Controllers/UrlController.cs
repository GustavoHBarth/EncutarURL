using Microsoft.AspNetCore.Mvc;
using SeuProjeto.Models;
using System;
using System.Linq;

namespace SeuProjeto.Controllers
{
    public class UrlController : Controller
    {
        private readonly UrlContext _context;

        public UrlController(UrlContext context)
        {
            _context = context;
        }

        // Exibir formulário para inserir URL
        public IActionResult Index()
        {
            return View();
        }

        // Encurtar URL
        [HttpPost]
        public IActionResult Create(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
            {
                return View("Index");
            }

            // Gerar um código aleatório para a URL encurtada
            var shortenedUrl = GenerateShortenedUrl();

            // Salvar no banco de dados
            var url = new UrlShortener
            {
                OriginalUrl = originalUrl,
                ShortenedUrl = shortenedUrl,
                CreatedAt = DateTime.Now
            };

            _context.Urls.Add(url);
            _context.SaveChanges();

            // Mostrar a URL encurtada para o usuário
            return View("Result", shortenedUrl);
        }

        // Redirecionar para a URL original
        public IActionResult Redirect(string id)
        {
            var url = _context.Urls.FirstOrDefault(u => u.ShortenedUrl == id);
            if (url == null)
            {
                return NotFound();
            }

            return Redirect(url.OriginalUrl);
        }

        // Gerar código aleatório para a URL encurtada
        private string GenerateShortenedUrl()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Range(0, 6).Select(_ => chars[random.Next(chars.Length)]).ToArray());
        }
    }
}
