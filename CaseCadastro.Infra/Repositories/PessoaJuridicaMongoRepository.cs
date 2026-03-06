using CaseCadastro.Domain.Domain;
using CaseCadastro.Domain.Interfaces.Repositories;
using CaseCadastro.Infra.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CaseCadastro.Infra.Repositories
{
    public class PessoaJuridicaMongoRepository : IPessoaJuridicaRepository
    {
        private readonly IMongoCollection<PessoaJuridica> _collection;

        public PessoaJuridicaMongoRepository(IMongoCollection<PessoaJuridica> collection)
        {
            _collection = collection;
        }

        public PessoaJuridicaMongoRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.Database);
            _collection = database.GetCollection<PessoaJuridica>("pessoa_juridica");
        }

        public async Task<PessoaJuridica> ObterPorCnpjAsync(string cnpj) => await _collection.Find(x => x.Cnpj == cnpj).FirstOrDefaultAsync();

        public async Task<PessoaJuridica> CriarAsync(PessoaJuridica pessoa)
        {
            await _collection.InsertOneAsync(pessoa);
            return pessoa;
        }

        public async Task<PessoaJuridica> AtualizarAsync(PessoaJuridica pessoa)
        {
            await _collection.ReplaceOneAsync(x => x.Cnpj == pessoa.Cnpj, pessoa);
            return pessoa;
        }

        public async Task RemoverAsync(string cnpj) => await _collection.DeleteOneAsync(x => x.Cnpj == cnpj);
    }
}
