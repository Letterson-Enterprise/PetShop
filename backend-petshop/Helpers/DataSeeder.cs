using backend_petshop.Data;
using backend_petshop.Entities;

namespace backend_petshop.Helpers
{
    public static class DataSeeder
    {
        public static async Task SeedAdminUser(AppDbContext context)
        {
            if (!context.Usuarios.Any(u => u.Login == "admin"))
            {
                var admin = new Usuario
                {
                    Login = "admin",
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword("admin")
                };

                context.Usuarios.Add(admin);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedDemoData(AppDbContext context)
        {
            if (context.Enderecos.Any())
                return;

            var enderecos = new List<Endereco>
            {
                new() { CEP = "01001000", NumeroCasa = 100, Bairro = "Centro", Cidade = "São Paulo", UF = "SP", Logradouro = "Praça da Sé" },
                new() { CEP = "20040002", NumeroCasa = 200, Bairro = "Centro", Cidade = "Rio de Janeiro", UF = "RJ", Logradouro = "Rua da Assembleia" },
                new() { CEP = "30140071", NumeroCasa = 50, Bairro = "Funcionários", Cidade = "Belo Horizonte", UF = "MG", Logradouro = "Avenida Afonso Pena" },
                new() { CEP = "40020003", NumeroCasa = 75, Bairro = "Comércio", Cidade = "Salvador", UF = "BA", Logradouro = "Rua Chile" },
                new() { CEP = "70040902", NumeroCasa = 15, Bairro = "Zona Cívico-Administrativa", Cidade = "Brasília", UF = "DF", Logradouro = "Praça dos Três Poderes" },
                new() { CEP = "80020020", NumeroCasa = 320, Bairro = "Centro", Cidade = "Curitiba", UF = "PR", Logradouro = "Rua XV de Novembro" },
                new() { CEP = "50020040", NumeroCasa = 180, Bairro = "Santo Amaro", Cidade = "Recife", UF = "PE", Logradouro = "Avenida Guararapes" },
                new() { CEP = "66020030", NumeroCasa = 55, Bairro = "Cidade Velha", Cidade = "Belém", UF = "PA", Logradouro = "Travessa Padre Eutíquio" },
                new() { CEP = "90020030", NumeroCasa = 88, Bairro = "Centro Histórico", Cidade = "Porto Alegre", UF = "RS", Logradouro = "Rua da Praia" },
                new() { CEP = "69005080", NumeroCasa = 42, Bairro = "Centro", Cidade = "Manaus", UF = "AM", Logradouro = "Avenida Eduardo Ribeiro" },
                new() { CEP = "57020000", NumeroCasa = 110, Bairro = "Ponta Verde", Cidade = "Maceió", UF = "AL", Logradouro = "Avenida Dr. Antônio Gouveia" },
                new() { CEP = "78020050", NumeroCasa = 67, Bairro = "Centro", Cidade = "Cuiabá", UF = "MT", Logradouro = "Rua Comandante Costa" },
            };

            context.Enderecos.AddRange(enderecos);
            await context.SaveChangesAsync();

            if (context.Tutores.Any())
                return;

            var tutores = new List<Tutor>
            {
                new() { Nome = "Ana Beatriz Silva", DataNascimento = new DateTime(1985, 3, 15), EnderecoId = enderecos[0].Id },
                new() { Nome = "Carlos Eduardo Santos", DataNascimento = new DateTime(1990, 7, 22), EnderecoId = enderecos[1].Id },
                new() { Nome = "Mariana Oliveira Costa", DataNascimento = new DateTime(1988, 1, 10), EnderecoId = enderecos[2].Id },
                new() { Nome = "Pedro Henrique Almeida", DataNascimento = new DateTime(1975, 11, 5), EnderecoId = enderecos[3].Id },
                new() { Nome = "Juliana Pereira Lima", DataNascimento = new DateTime(1995, 6, 18), EnderecoId = enderecos[4].Id },
                new() { Nome = "Rafael Mendes Souza", DataNascimento = new DateTime(1982, 9, 30), EnderecoId = enderecos[5].Id },
                new() { Nome = "Fernanda Rocha Barbosa", DataNascimento = new DateTime(1993, 4, 12), EnderecoId = enderecos[6].Id },
                new() { Nome = "Lucas Martins Teixeira", DataNascimento = new DateTime(1980, 12, 8), EnderecoId = enderecos[7].Id },
                new() { Nome = "Amanda Carvalho Dias", DataNascimento = new DateTime(1998, 8, 25), EnderecoId = enderecos[8].Id },
                new() { Nome = "Bruno Oliveira Ribeiro", DataNascimento = new DateTime(1987, 5, 14), EnderecoId = enderecos[9].Id },
                new() { Nome = "Larissa Faria Neves", DataNascimento = new DateTime(1991, 2, 20), EnderecoId = enderecos[10].Id },
            };

            context.Tutores.AddRange(tutores);
            await context.SaveChangesAsync();

            if (context.Animais.Any())
                return;

            var random = new Random(42);
            var especies = new[] { "Cachorro", "Gato", "Ave", "Hamster", "Peixe", "Coelho", "Tartaruga" };
            var nomesCachorro = new[] { "Rex", "Bolinha", "Thor", "Mel", "Luke", "Bela", "Toddy", "Nina", "Pitoco", "Luna" };
            var nomesGato = new[] { "Mimi", "Chico", "Frajola", "Lili", "Simba", "Nala", "Sushi", "Bichano", "Tiffany", "Jade" };
            var nomesOutros = new[] { "Pipoca", "Pérola", "Zeca", "Buddy", "Bob", "Fred", "Lola", "Lilica", "Tico", "Pingo" };

            var animais = new List<Animal>();
            for (int i = 0; i < 12; i++)
            {
                var especie = especies[random.Next(especies.Length)];
                var nome = especie switch
                {
                    "Cachorro" => nomesCachorro[random.Next(nomesCachorro.Length)],
                    "Gato" => nomesGato[random.Next(nomesGato.Length)],
                    _ => nomesOutros[random.Next(nomesOutros.Length)]
                };

                animais.Add(new Animal
                {
                    Nome = nome,
                    Especie = especie,
                    Peso = Math.Round((decimal)(random.NextDouble() * 30 + 0.5), 2),
                    DataNascimento = new DateTime(
                        random.Next(2018, 2025),
                        random.Next(1, 13),
                        random.Next(1, 28)
                    ),
                    TutorId = tutores[i % tutores.Count].Id
                });
            }

            context.Animais.AddRange(animais);
            await context.SaveChangesAsync();
        }
    }
}
