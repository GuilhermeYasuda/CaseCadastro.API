using System.ComponentModel.DataAnnotations;

namespace CaseCadastro.Domain.Domain
{
    public class PessoaJuridica
    {
        public string RazaoSocial { get; set; }
        [Key]
        public string Cnpj { get; set; }
        public Endereco Endereco { get; set; }
    }
}
