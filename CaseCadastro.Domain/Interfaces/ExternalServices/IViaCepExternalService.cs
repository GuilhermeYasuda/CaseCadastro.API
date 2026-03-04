using CaseCadastro.Domain.Domain;

namespace CaseCadastro.Domain.Interfaces.ExternalServices
{
    public interface IViaCepExternalService
    {
        Task<Endereco> ConsultarCepAsync(string cep);
        Task<IEnumerable<Endereco>> ConsultarEnderecoAsync(string uf, string cidade, string logradouro);
    }
}
