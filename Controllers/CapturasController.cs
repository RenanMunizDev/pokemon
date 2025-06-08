using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Data;
using PokemonAPI.DTOs;
using PokemonAPI.Models;
using PokemonAPI.Services;

namespace PokemonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CapturasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CapturasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Capturar(CaptureDto dto)
        {
            var mestreExiste = await _context.Mestres.AnyAsync(m => m.Id == dto.MestreId);
            if (!mestreExiste) return NotFound("Mestre Pokémon não encontrado.");

            var captura = new Captura
            {
                MestreId = dto.MestreId,
                PokemonId = dto.PokemonId
            };

            _context.Capturas.Add(captura);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Capturar), new { id = captura.Id }, captura);
        }

        [HttpGet]
        public async Task<IActionResult> ListarCapturas([FromServices] IPokemonService pokemonService)
        {
            var capturas = await _context.Capturas
                .Include(c => c.Mestre)
                .ToListAsync();

            var listaDetalhada = new List<CapturaDetalhadaDto>();

            foreach (var captura in capturas)
            {
                var detalhesPokemon = await pokemonService.GetPokemonDetalhadoAsync(captura.PokemonId);

                if (detalhesPokemon == null) continue;

                listaDetalhada.Add(new CapturaDetalhadaDto
                {
                    CapturaId = captura.Id,
                    DataCaptura = captura.DataCaptura,
                    MestreId = captura.MestreId,
                    NomeMestre = captura.Mestre?.Nome ?? "Desconhecido",
                    PokemonId = captura.PokemonId,
                    NomePokemon = detalhesPokemon.Nome,
                    Tipos = detalhesPokemon.Tipos,
                    Evolucoes = detalhesPokemon.Evolucoes,
                    SpriteBase64 = detalhesPokemon.SpriteBase64
                });
            }
            return Ok(listaDetalhada);
        }
    }
}
