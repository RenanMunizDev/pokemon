using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Models
{
    public class MestrePokemon
    {
        public int Id { get; set; }

        [Required]
        public string? Nome { get; set; }

        [Range(10, 120)]
        public int Idade { get; set; }

        [Required]
        [RegularExpression(@"\d{3}\.\d{3}\.\d{3}-\d{2}", ErrorMessage = "CPF inválido")]
        public string? CPF { get; set; }
    }
}
