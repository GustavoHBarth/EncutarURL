using System.ComponentModel.DataAnnotations;

namespace EncutarURL.Models
{
    public class Url
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        // Construtor que exige o parâmetro "name"
        public Url(string name)
        {
            // Atribui o valor ao campo ou propriedade "name"
            this.OriginalUrl = name;
        }

        // Construtor sem parâmetros
        public Url()
        {
            // Inicialização padrão, se necessário
        }
    }

}
