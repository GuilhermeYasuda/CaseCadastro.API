using CaseCadastro.Domain.Domain;

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
