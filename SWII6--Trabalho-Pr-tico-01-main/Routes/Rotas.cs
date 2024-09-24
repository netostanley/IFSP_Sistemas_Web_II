using Microsoft.AspNetCore.Http;
using TP01.Models;

namespace TP01.Routes
{
    public class Rotas
    {
        private Repositorio _repositorio;
        public Rotas()
        {
            _repositorio = new Repositorio(); // Initialize Repositorio
            Task task = _repositorio.preencheRepositorioAsync(); // Initialize asynchronously if necessary
        }
        public Task Roteamento(HttpContext context)
        {

            var caminhosAtendidos = new Dictionary<string, RequestDelegate>()
            {
                {"/livro/ApresentarLivro", _repositorio.DetalhesLivro},
                {"/livro",  _repositorio.nomeDoLivro },
                {"/livro/ToString", _repositorio.ToStringLivro  },
                {"/livro/GetAutores", _repositorio.GetAutorLivro },
            };

            if (caminhosAtendidos.ContainsKey(context.Request.Path))
            {
                var metodo = caminhosAtendidos[context.Request.Path];
                return metodo.Invoke(context);
            }

            return context.Response.WriteAsync("Caminho Inexistente!");
        }

        public void Configure(IApplicationBuilder app)
        {
            try
            {
                app.Run(Roteamento);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
