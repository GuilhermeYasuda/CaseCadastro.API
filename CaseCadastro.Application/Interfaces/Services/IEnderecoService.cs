using CaseCadastro.Domain.Domain;

namespace CaseCadastro.Application.Interfaces.Services
{
    public interface IEnderecoService
    {
        Task<Endereco> ConsultarCepAsync(string cep);
        Task<IEnumerable<Endereco>> ConsultarEnderecoAsync(string uf, string cidade, string logradouro);
    }
}