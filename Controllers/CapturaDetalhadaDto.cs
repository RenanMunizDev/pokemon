namespace PokemonAPI.DTOs
{
    public class CapturaDetalhadaDto
    {
        public int CapturaId { get; set; }
        public DateTime DataCaptura { get; set; }

        public int MestreId { get; set; }
        public string NomeMestre { get; set; } = string.Empty;

        public int PokemonId { get; set; }
        public string NomePokemon { get; set; } = string.Empty;
        public List<string> Tipos { get; set; } = new();
        public List<string> Evolucoes { get; set; } = new();
        public string SpriteBase64 { get; set; } = string.Empty;
    }
}
