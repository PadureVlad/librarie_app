using System.ComponentModel.DataAnnotations;

namespace Library_App.Models
{
    public class Carte
    {
        [Key]
        public int CodCarte { get; set; }
        public string? Titlu { get; set; }
        public string? Autor { get; set; }
        public string? Editor { get; set; }
        public double Pret { get; set; }
        public string? Imagine { get; set; }
    }
}
