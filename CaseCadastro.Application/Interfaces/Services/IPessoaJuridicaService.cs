using CaseCadastro.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseCadastro.Application.Interfaces.Services
{
    public interface IPessoaJuridicaService
    {
        Task<PessoaJuridica> ObterPorCnpjAsync(string cnpj);
        Task<PessoaJuridica> CriarAsync(PessoaJuridica pessoa);
        Task<PessoaJuridica> AtualizarAsync(PessoaJuridica pessoa);
        Task RemoverAsync(string cnpj);
    }
}
