using CaseCadastro.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseCadastro.Application.Interfaces.Services
{
    public interface IPessoaFisicaService
    {
        Task<PessoaFisica> ObterPorCpfAsync(string cpf);
        Task<PessoaFisica> CriarAsync(PessoaFisica pessoa);
        Task<PessoaFisica> AtualizarAsync(PessoaFisica pessoa);
        Task RemoverAsync(string cpf);
    }
}
