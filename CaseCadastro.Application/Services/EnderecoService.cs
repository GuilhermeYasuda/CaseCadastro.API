using CaseCadastro.Application.Interfaces.Services;
using CaseCadastro.Domain.Domain;
using CaseCadastro.Domain.Interfaces.ExternalServices;

namespace CaseCadastro.Application.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IViaCepExternalService _viaCepExternalService;
        public EnderecoService(IViaCepExternalService viaCepExternalService)
        {
            _viaCepExternalService = viaCepExternalService;
        }

        public Task<Endereco> ConsultarCepAsync(string cep) =>
            _viaCepExternalService.ConsultarCepAsync(cep);

        public Task<IEnumerable<Endereco>> ConsultarEnderecoAsync(string uf, string cidade, string logradouro) =>
            _viaCepExternalService.ConsultarEnderecoAsync(uf, cidade, logradouro);
    }
}
