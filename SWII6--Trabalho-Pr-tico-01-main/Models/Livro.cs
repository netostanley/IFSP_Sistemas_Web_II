using System.Text;

namespace TP01.Models
{
    public class Livro
    {

        public int Id { get; set; }
        public string Titulo { get; set; }
        public Autor[] Autor { get; set; }
        public double Preco { get; set; }
        public int Quantidade { get; set; }

        public Livro(string nome, Autor[] autores, double preco)
        {
            this.Titulo = nome;
            this.Autor = autores;
            this.Preco = preco;
        }

        public Livro(string nome, Autor[] autores, double preco, int quantidade)
        {
            this.Titulo = nome;
            this.Autor = autores;
            this.Preco = preco;
            this.Quantidade = quantidade;
        }

        public string getNome()
        {
            return this.Titulo;
        }

        public Autor[] getAutores()
        {
            return this.Autor;
        }

        public string getNomesAutores()
        {
            string retorno = string.Empty;

            foreach (var item in this.Autor)
            {
                retorno += item.Nome + ", ";
            }

            retorno = retorno.Substring(0, retorno.Length - 1);

            return retorno;
        }

        public double getPreco()
        {
            return this.Preco;
        }

        public int getQuantidade()
        {
            return this.Quantidade;
        }

        public void setPrice(double preco)
        {
            this.Preco = preco;
        }

        public void setQuantidade(int quantidade)
        {
            this.Quantidade = quantidade;
        }


        public string Detalhes()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Detalhes do Livro");
            stringBuilder.AppendLine("=====");
            stringBuilder.AppendLine($"Título: {Titulo}");
            stringBuilder.AppendLine($"     Autores:");
            foreach(Autor a in this.Autor)
            {
                stringBuilder.AppendLine($"           -  {a.Nome}");
            }
            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            string retorno = $"Livro[nome={this.Titulo}, autores=";

            foreach (var item in this.Autor)
            {
                retorno += $"[nome={item.Nome}, email={item.Email}, gênero={item.Genero}],";
            }

            retorno += $" preço={this.Preco}, quantiade={this.Quantidade}]";

            return retorno;

        }
    }
}
