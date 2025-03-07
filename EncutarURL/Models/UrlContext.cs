using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SeuProjeto.Controllers;

namespace EncutarURL.Models
{
    public class UrlShortener
    {
        public DbSet<UrlShortener> Urls { get; set; }

        public UrlShortener(DbContextOptions<UrlContext> options) : base(options) { }
    }
}
