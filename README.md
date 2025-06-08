# PokemonAPI

API para listar, cadastrar mestres Pokémon e registrar capturas, utilizando dados da [PokeAPI](https://pokeapi.co/).

---

## 🔧 Tecnologias Utilizadas

- 💻 Linguagem: C#
- 🚀 Framework: ASP.NET Core 9
- 🧱 Banco de dados: SQLite (via Entity Framework Core)
- 🌐 API externa: [PokeAPI](https://pokeapi.co/)
- 🧪 Swagger para testes e documentação
- 🐱‍💻 Injeção de dependência com IHttpClientFactory

---

## ▶️ Como Executar

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/PokemonAPI.git
cd PokemonAPI
```

### 2. Restaure os pacotes e aplique as migrations

```bash
dotnet restore
dotnet ef database update
```

### 3. Execute o projeto

```bash
dotnet run
```

O Swagger estará disponível em:

```
http://localhost:5000/swagger
```

---

## 📁 Endpoints Disponíveis

### Pokémons

- `GET /api/Pokemons/random` → Lista 10 pokémons aleatórios
- `GET /api/Pokemons/{id}` → Detalha um Pokémon (tipos, evoluções, sprite base64)

### Mestres

- `POST /api/Masters` → Cadastra mestre
- `GET /api/Masters/{id}` → Consulta mestre por ID

### Capturas

- `POST /api/Capturas` → Registra uma captura
- `GET /api/Capturas` → Lista todas as capturas com os dados do Pokémon

---

## 📦 .gitignore

Usei o gerador oficial: [toptal.com/developers/gitignore](https://www.toptal.com/developers/gitignore)

---

## 📌 Observações

This is a challenge by [Coodesh](https://coodesh.com/)
