
namespace PokemonAPI.DTOs
{
    public class PokemonDto
    {
        public string? Nome { get; set; }
        public string? SpriteBase64 { get; set; }
        public List<string>? Evolucoes { get; set; }
    }
}
