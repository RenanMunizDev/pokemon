using PokemonAPI.Models;


public class Captura
{
    public int Id { get; set; }
    public int MestreId { get; set; }
    public int PokemonId { get; set; }
    public DateTime DataCaptura { get; set; } = DateTime.UtcNow;

    public MestrePokemon? Mestre { get; set; }
}
