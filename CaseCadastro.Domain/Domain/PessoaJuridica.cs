using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseCadastro.Domain.Domain
{
    public class PessoaJuridica
    {
        public Guid Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public Endereco Endereco { get; set; }
    }
}
