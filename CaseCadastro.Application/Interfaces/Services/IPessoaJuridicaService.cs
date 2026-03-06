using CaseCadastro.Domain.Domain;

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
