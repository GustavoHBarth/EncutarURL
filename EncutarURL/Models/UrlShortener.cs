namespace SeuProjeto.Models
{
    public class UrlShortener
    {
        public int Id { get; set; } // Id único para cada URL
        public string OriginalUrl { get; set; } // URL original
        public string ShortenedUrl { get; set; } // URL encurtada
        public DateTime CreatedAt { get; set; } // Data de criação
    }
}
