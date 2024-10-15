using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace tp2tuller.Models
{
    public class BL
    {
        public BL() { Numero = ""; Consignee = ""; Navio = ""; }
        public int ID { get; set; }
        public string Numero { get; set; }
        public string Consignee { get; set; }
        public string Navio { get; set; }

        [AllowNull]
        public ICollection<Conteiner> Containers { get; set; } = new List<Conteiner>();
    }
}
