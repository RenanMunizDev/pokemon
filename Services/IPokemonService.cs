using System.Threading.Tasks;
using PokemonAPI.DTOs;

namespace PokemonAPI.Services
{
    public interface IPokemonService
    {
        Task<List<PokemonDto?>> GetRandomPokemonsAsync(int count = 10);
        Task<PokemonDetalhadoDto?> GetPokemonDetalhadoAsync(int id);
    }
}
