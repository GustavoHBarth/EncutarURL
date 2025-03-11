using EncutarURL.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using EncutarURL.Models;

namespace EncurtadorDeURL.Controllers
{
    public class UrlController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UrlController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Ação para criar o encurtamento
        public IActionResult Create(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
                return BadRequest("A URL original não pode ser vazia.");

            var shortenedUrl = GenerateShortenedUrl();

            // Corrigido: Passar o parâmetro para o construtor (se for necessário)
            var url = new Url(originalUrl)
            {
                ShortenedUrl = shortenedUrl
            };

            _context.Urls.Add(url);
            _context.SaveChanges();

            return RedirectToAction("Shortened", new { shortenedUrl = url.ShortenedUrl });
        }


        // Ação para exibir a URL encurtada
        public IActionResult Shortened(string shortenedUrl)
        {
            var url = _context.Urls.FirstOrDefault(u => u.ShortenedUrl == shortenedUrl);

            if (url == null)
                return NotFound();

            ViewData["ShortenedUrl"] = url.ShortenedUrl;
            ViewData["OriginalUrl"] = url.OriginalUrl;

            return View();
        }

        // Método para gerar a URL encurtada
        private string GenerateShortenedUrl()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Range(0, 6).Select(_ => chars[random.Next(chars.Length)]).ToArray());
        }
    }
}
