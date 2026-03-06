using CaseCadastro.Domain.Domain;
using CaseCadastro.Infra.Configurations;
using CaseCadastro.Infra.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;

namespace CaseCadastro.Test.Unit.Infra.Repositories
{
    public class PessoaJuridicaMongoRepositoryTests
    {
        private readonly Mock<IMongoCollection<PessoaJuridica>> _mockCollection;
        private readonly Mock<IOptions<MongoDbSettings>> _mockOptions;
        private readonly MongoDbSettings _settings;

        public PessoaJuridicaMongoRepositoryTests()
        {
            _mockCollection = new Mock<IMongoCollection<PessoaJuridica>>();
            _mockOptions = new Mock<IOptions<MongoDbSettings>>();
            _settings = new MongoDbSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                Database = "TestDb"
            };
            _mockOptions.Setup(x => x.Value).Returns(_settings);
        }

        [Fact]
        public async Task Deve_Criar_PessoaJuridicaMongoRepository()
        {
            var repo = new PessoaJuridicaMongoRepository(_mockOptions.Object);

            Assert.NotNull(repo);
        }

        [Fact]
        public async Task ObterPorCnpjAsync_DeveRetornarPessoa()
        {
            // Arrange
            var pessoa = new PessoaJuridica { Cnpj = "12345678901234" };
            var mockCursor = new Mock<IAsyncCursor<PessoaJuridica>>();
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
                  .ReturnsAsync(true)
                  .ReturnsAsync(false);
            mockCursor.Setup(x => x.Current).Returns([pessoa]);

            _mockCollection.Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<PessoaJuridica>>(),
                It.IsAny<FindOptions<PessoaJuridica, PessoaJuridica>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            var repo = new PessoaJuridicaMongoRepository(_mockCollection.Object);

            // Act
            var result = await repo.ObterPorCnpjAsync("12345678901234");

            // Assert
            Assert.Equal("12345678901234", result.Cnpj);
        }

        [Fact]
        public async Task CriarAsync_DeveInserirPessoa()
        {
            // Arrange
            var pessoa = new PessoaJuridica { Cnpj = "12345678901234" };
            _mockCollection.Setup(x => x.InsertOneAsync(pessoa, null, default))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var repo = new PessoaJuridicaMongoRepository(_mockCollection.Object);

            // Act
            var result = await repo.CriarAsync(pessoa);

            // Assert
            Assert.Equal(pessoa, result);
            _mockCollection.Verify();
        }

        [Fact]
        public async Task AtualizarAsync_DeveAtualizarPessoa()
        {
            // Arrange
            var pessoa = new PessoaJuridica { Cnpj = "12345678901234" };
            _mockCollection.Setup(x => x.ReplaceOneAsync(
                    It.IsAny<FilterDefinition<PessoaJuridica>>(),
                    pessoa,
                    It.IsAny<ReplaceOptions>(),
                    default))
                .ReturnsAsync((ReplaceOneResult)null)
                .Verifiable();

            var repo = new PessoaJuridicaMongoRepository(_mockCollection.Object);

            // Act
            var result = await repo.AtualizarAsync(pessoa);

            // Assert
            Assert.Equal(pessoa, result);
            _mockCollection.Verify();
        }

        [Fact]
        public async Task RemoverAsync_DeveRemoverPessoa()
        {
            // Arrange
            _mockCollection.Setup(x => x.DeleteOneAsync(
                    It.IsAny<FilterDefinition<PessoaJuridica>>(),
                    default))
                .ReturnsAsync((DeleteResult)null)
                .Verifiable();

            var repo = new PessoaJuridicaMongoRepository(_mockCollection.Object);

            // Act
            await repo.RemoverAsync("12345678901234");

            // Assert
            _mockCollection.Verify();
        }
    }
}
