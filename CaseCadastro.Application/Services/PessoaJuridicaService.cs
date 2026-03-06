using CaseCadastro.Application.Interfaces.Services;
using CaseCadastro.Domain.Domain;
using CaseCadastro.Domain.Interfaces.ExternalServices;
using CaseCadastro.Domain.Interfaces.Repositories;

namespace CaseCadastro.Application.Services
{
    public class PessoaJuridicaService : IPessoaJuridicaService
    {
        private readonly IPessoaJuridicaRepository _repository;
        private readonly IViaCepExternalService _viaCepService;

        public PessoaJuridicaService(IPessoaJuridicaRepository repository, IViaCepExternalService viaCepService)
        {
            _repository = repository;
            _viaCepService = viaCepService;
        }

        public async Task<PessoaJuridica> ObterPorCnpjAsync(string cnpj) =>
            await _repository.ObterPorCnpjAsync(cnpj);

        public async Task<PessoaJuridica> CriarAsync(PessoaJuridica pessoa)
        {
            var endereco = await _viaCepService.ConsultarCepAsync(pessoa.Endereco.Cep);
            pessoa.Endereco = endereco;
            return await _repository.CriarAsync(pessoa);
        }

        public async Task<PessoaJuridica> AtualizarAsync(PessoaJuridica pessoa)
        {
            var endereco = await _viaCepService.ConsultarCepAsync(pessoa.Endereco.Cep);
            pessoa.Endereco = endereco;
            return await _repository.AtualizarAsync(pessoa);
        }

        public async Task RemoverAsync(string cnpj) =>
            await _repository.RemoverAsync(cnpj);
    }
}
