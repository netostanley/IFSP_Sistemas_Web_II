using System.ComponentModel.DataAnnotations;

namespace tp2tuller.Models
{
    public class Conteiner
    {
        public int ID { get; set; }
        public int ID_BL { get; set; }
        public string Numero { get; set; }

        [Required(ErrorMessage = "O tipo é obrigatório.")]
        [RegularExpression("^(Dry|Reefer)$", ErrorMessage = "O tipo deve ser 'Dry' ou 'Reefer'.")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "O tamanho é obrigatório.")]
        [Range(20, 40, ErrorMessage = "O tamanho deve ser 20 ou 40.")]
        public int Tamanho { get; set; }

        public BL BL { get; set; } = new BL();

    }
}
