using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseCadastro.Domain.Domain
{
    public class PessoaFisica
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public Endereco Endereco { get; set; }
    }
}
