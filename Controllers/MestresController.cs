using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Data;
using PokemonAPI.Models;

namespace PokemonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MestresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MestresController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody] MestrePokemon mestre)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Mestres.Add(mestre);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = mestre.Id }, mestre);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var mestre = _context.Mestres.Find(id);
            if (mestre == null)
                return NotFound();

            return Ok(mestre);
        }
    }
}
