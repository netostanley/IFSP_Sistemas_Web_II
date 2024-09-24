using System.Text;
using TP01.Models;

namespace TP01.Routes
{
    public class Repositorio
    {
        public List<Livro> Livro = new List<Livro>();

        public async Task preencheRepositorioAsync()
        {
            string csvFilePath = "C:\\Users\\nahas\\OneDrive\\Documentos\\arqTP1.csv";

            // Ler o arquivo CSV linha por linha
            using (var reader = new StreamReader(csvFilePath, Encoding.UTF8))
            {
                // Ignorar a primeira linha do cabeçalho, se existir
                var headerLine = await reader.ReadLineAsync();

                // Ler as demais linhas
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var values = line.Split(',');

                    // Supondo que o CSV tenha as colunas: Id, Titulo, Autores, Preco, Quantidade
                    int id = int.Parse(values[0]);
                    string titulo = values[1];
                    string[] autoresCsv = values[2].Split(';'); // Múltiplos autores separados por ';'

                    // Processar cada autor
                    var autores = autoresCsv.Select(autor =>
                    {
                        var autorInfo = autor.Split(':');

                        // Verificar se há exatamente 3 partes (Nome, Email, Genero)
                        if (autorInfo.Length != 3)
                        {
                            throw new ArgumentException($"Dados do autor inválidos: {autor}");
                        }

                        // Validar se o campo de gênero contém pelo menos 1 caractere
                        if (string.IsNullOrWhiteSpace(autorInfo[2]) || autorInfo[2].Length < 1)
                        {
                            throw new ArgumentException($"Gênero inválido para o autor: {autor}");
                        }

                        return new Autor
                        {
                            Nome = autorInfo[0],
                            Email = autorInfo[1],
                            Genero = autorInfo[2][0] // Assumindo que o gênero é um caractere, como 'M' ou 'F'
                        };
                    }).ToArray();

                    double preco = double.Parse(values[3]);
                    int quantidade = int.Parse(values[4]);

                    // Criar objeto Livro com múltiplos autores
                    var livro = new Livro(titulo, autores, preco, quantidade);
                    Livro.Add(livro);
                }
            }
        }

        public Task nomeDoLivro(HttpContext context)
        {
            context.Response.ContentType = "text/plain; charset=utf-8"; // Define o charset como UTF-8
            return context.Response.WriteAsync(Livro.FirstOrDefault()?.Titulo ?? "Livro não encontrado");
        }

        public Task ToStringLivro(HttpContext context)
        {
            context.Response.ContentType = "text/plain; charset=utf-8"; // Define o charset como UTF-8
            return context.Response.WriteAsync(Livro.FirstOrDefault()?.ToString() ?? "Livro não encontrado");
        }

        public Task GetAutorLivro(HttpContext context)
        {
            context.Response.ContentType = "text/plain; charset=utf-8"; // Define o charset como UTF-8
            return context.Response.WriteAsync(Livro.FirstOrDefault()?.getNomesAutores() ?? "Autores não encontrados");
        }

        public Task DetalhesLivro(HttpContext context)
        {
            context.Response.ContentType = "text/plain; charset=utf-8"; // Define o charset como UTF-8
            return context.Response.WriteAsync(Livro.FirstOrDefault()?.Detalhes() ?? "Autores não encontrados");
        }
    }
}
