
using System.Net.Http;
using Newtonsoft.Json;
using PokemonAPI.DTOs;
using PokemonAPI.Services;

namespace PokemonAPI.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly ILogger<PokemonService> _logger;

        public PokemonService(HttpClient httpClient, IHttpClientFactory httpClientFactory, ILogger<PokemonService> logger)
        {
            _httpClient = httpClient;
            _httpClientFactory = httpClientFactory;
            _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _logger = logger;
        }

        public async Task<List<PokemonDto?>> GetRandomPokemonsAsync(int count = 10)
        {
            var random = new Random();
            var tasks = new List<Task<PokemonDto?>>();
            for (int i = 0; i < count; i++)
            {
                int randomId = random.Next(1, 151); // 1 a 150 (1ª geração)
                tasks.Add(GetPokemonByIdAsync(randomId));
            }

            var results = await Task.WhenAll(tasks);
            return results.ToList();
        }

        public async Task<PokemonDetalhadoDto?> GetPokemonDetalhadoAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();

            // 1. Dados básicos
            var response = await httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            dynamic dados = JsonConvert.DeserializeObject(json)!;

            string nome = dados.name;
            int altura = dados.height;
            int peso = dados.weight;
            List<string> tipos = ((IEnumerable<dynamic>)dados.types).Select(t => (string)t.type.name).ToList();
            string spriteUrl = dados.sprites.front_default;

            // 2. Obter a espécie (para pegar evoluções)
            var especieResponse = await httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon-species/{id}");
            if (!especieResponse.IsSuccessStatusCode) return null;

            var especieJson = await especieResponse.Content.ReadAsStringAsync();
            dynamic especie = JsonConvert.DeserializeObject(especieJson)!;

            string urlCadeia = especie.evolution_chain.url;

            // 3. Buscar cadeia evolutiva
            var cadeiaResponse = await httpClient.GetAsync(urlCadeia);
            if (!cadeiaResponse.IsSuccessStatusCode) return null;

            var cadeiaJson = await cadeiaResponse.Content.ReadAsStringAsync();
            dynamic cadeia = JsonConvert.DeserializeObject(cadeiaJson)!;

            List<string> evolucoes = new();
            dynamic atual = cadeia.chain;
            while (atual != null)
            {
                evolucoes.Add((string)atual.species.name);
                atual = (atual.evolves_to.Count > 0) ? atual.evolves_to[0] : null;
            }

            // 4. Converter sprite para base64
            var spriteBytes = await httpClient.GetByteArrayAsync(spriteUrl);
            string spriteBase64 = Convert.ToBase64String(spriteBytes);

            return new PokemonDetalhadoDto
            {
                Nome = nome,
                Altura = altura,
                Peso = peso,
                Tipos = tipos,
                Evolucoes = evolucoes,
                SpriteBase64 = spriteBase64
            };
        }

        public async Task<PokemonDto?> GetPokemonByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"pokemon/{id}");
                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();
                dynamic dados = JsonConvert.DeserializeObject(json)!;

                string nome = dados.name;
                string spriteUrl = dados.sprites.front_default;

                var spriteBytes = await _httpClient.GetByteArrayAsync(spriteUrl);
                var spriteBase64 = Convert.ToBase64String(spriteBytes);

                return new PokemonDto
                {
                    Nome = nome,
                    SpriteBase64 = spriteBase64,
                    Evolucoes = new List<string>() // Placeholder
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar Pokémon {id}");
                return null;
            }
        }
    }
}
