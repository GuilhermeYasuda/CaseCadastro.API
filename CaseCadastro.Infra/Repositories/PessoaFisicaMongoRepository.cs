using CaseCadastro.Domain.Domain;
using CaseCadastro.Domain.Interfaces.Repositories;
using CaseCadastro.Infra.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CaseCadastro.Infra.Repositories
{
    public class PessoaFisicaMongoRepository : IPessoaFisicaRepository
    {
        private readonly IMongoCollection<PessoaFisica> _collection;

        public PessoaFisicaMongoRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.Database);
            _collection = database.GetCollection<PessoaFisica>("pessoa_fisica");
        }

        public async Task<PessoaFisica> ObterPorCpfAsync(string cpf) => await _collection.Find(x => x.Cpf == cpf).FirstOrDefaultAsync();

        public async Task<PessoaFisica> CriarAsync(PessoaFisica pessoa)
        {
            await _collection.InsertOneAsync(pessoa);
            return pessoa;
        }

        public async Task<PessoaFisica> AtualizarAsync(PessoaFisica pessoa)
        {
            await _collection.ReplaceOneAsync(x => x.Cpf == pessoa.Cpf, pessoa);
            return pessoa;
        }

        public async Task RemoverAsync(string cpf) => await _collection.DeleteOneAsync(x => x.Cpf == cpf);
    }
}
