using CaseCadastro.Domain.Domain;

namespace CaseCadastro.Domain.Interfaces.Repositories
{
    public interface IPessoaFisicaRepository
    {
        Task<PessoaFisica> ObterPorCpfAsync(string cpf);
        Task<PessoaFisica> CriarAsync(PessoaFisica pessoa);
        Task<PessoaFisica> AtualizarAsync(PessoaFisica pessoa);
        Task RemoverAsync(string cpf);
    }
}
