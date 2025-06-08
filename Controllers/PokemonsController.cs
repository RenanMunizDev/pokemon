
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Services;

namespace PokemonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonsController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonsController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandom()
        {
            var pokemons = await _pokemonService.GetRandomPokemonsAsync();
            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pokemon = await _pokemonService.GetPokemonDetalhadoAsync(id);
            if (pokemon == null) return NotFound();

            return Ok(pokemon);
        }
    }
}
