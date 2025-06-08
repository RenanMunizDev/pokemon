public class PokemonDetalhadoDto
{
    public string Nome { get; set; } = string.Empty;
    public int Altura { get; set; }
    public int Peso { get; set; }
    public List<string> Tipos { get; set; } = new();
    public List<string> Evolucoes { get; set; } = new();
    public string SpriteBase64 { get; set; } = string.Empty;
}
