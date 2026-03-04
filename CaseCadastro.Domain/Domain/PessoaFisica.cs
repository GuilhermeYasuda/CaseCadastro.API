using System.ComponentModel.DataAnnotations;

namespace CaseCadastro.Domain.Domain
{
    public class PessoaFisica
    {
        public string Nome { get; set; }
        [Key]
        public string Cpf { get; set; }
        public Endereco Endereco { get; set; }
    }
}
