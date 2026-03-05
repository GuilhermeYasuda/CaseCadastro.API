using CaseCadastro.Domain.Domain;

namespace CaseCadastro.Domain.Interfaces.Repositories
{
    public interface IPessoaJuridicaRepository
    {
        Task<PessoaJuridica> ObterPorCnpjAsync(string cnpj);
        Task<PessoaJuridica> CriarAsync(PessoaJuridica pessoa);
        Task<PessoaJuridica> AtualizarAsync(PessoaJuridica pessoa);
        Task RemoverAsync(string cnpj);
    }
}
