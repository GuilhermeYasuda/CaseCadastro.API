using CaseCadastro.Application.Interfaces.Services;
using CaseCadastro.Domain.Domain;
using CaseCadastro.Domain.Interfaces.ExternalServices;
using CaseCadastro.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseCadastro.Application.Services
{
    public class PessoaFisicaService : IPessoaFisicaService
    {
        private readonly IPessoaFisicaRepository _repository;
        private readonly IViaCepExternalService _viaCepService;

        public PessoaFisicaService(IPessoaFisicaRepository repository, IViaCepExternalService viaCepService)
        {
            _repository = repository;
            _viaCepService = viaCepService;
        }

        public async Task<PessoaFisica> ObterPorCpfAsync(string cpf) =>
            await _repository.ObterPorCpfAsync(cpf);

        public async Task<PessoaFisica> CriarAsync(PessoaFisica pessoa)
        {
            var endereco = await _viaCepService.ConsultarCepAsync(pessoa.Endereco.Cep);
            pessoa.Endereco = endereco;
            return await _repository.CriarAsync(pessoa);
        }

        public async Task<PessoaFisica> AtualizarAsync(PessoaFisica pessoa)
        {
            var endereco = await _viaCepService.ConsultarCepAsync(pessoa.Endereco.Cep);
            pessoa.Endereco = endereco;
            return await _repository.AtualizarAsync(pessoa);
        }

        public async Task RemoverAsync(string cpf) =>
            await _repository.RemoverAsync(cpf);
    }
}
