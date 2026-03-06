using CaseCadastro.Domain.Domain;
using CaseCadastro.Infra.Configurations;
using CaseCadastro.Infra.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;

namespace CaseCadastro.Test.Unit.Infra.Repositories
{
    public class PessoaFisicaMongoRepositoryTests
    {
        private readonly Mock<IMongoCollection<PessoaFisica>> _mockCollection;
        private readonly Mock<IOptions<MongoDbSettings>> _mockOptions;
        private readonly MongoDbSettings _settings;

        public PessoaFisicaMongoRepositoryTests()
        {
            _mockCollection = new Mock<IMongoCollection<PessoaFisica>>();
            _mockOptions = new Mock<IOptions<MongoDbSettings>>();
            _settings = new MongoDbSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                Database = "TestDb"
            };
            _mockOptions.Setup(x => x.Value).Returns(_settings);
        }

        [Fact]
        public async Task Deve_Criar_PessoaFisicaMongoRepository()
        {
            var repo = new PessoaFisicaMongoRepository(_mockOptions.Object);

            Assert.NotNull(repo);
        }

        [Fact]
        public async Task ObterPorCpfAsync_DeveRetornarPessoa()
        {
            // Arrange
            var pessoa = new PessoaFisica { Cpf = "12345678901" };
            var mockCursor = new Mock<IAsyncCursor<PessoaFisica>>();
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
                  .ReturnsAsync(true)
                  .ReturnsAsync(false);
            mockCursor.Setup(x => x.Current).Returns([pessoa]);

            _mockCollection.Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<PessoaFisica>>(),
                It.IsAny<FindOptions<PessoaFisica, PessoaFisica>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            var repo = new PessoaFisicaMongoRepository(_mockCollection.Object);

            // Act
            var result = await repo.ObterPorCpfAsync("12345678901");

            // Assert
            Assert.Equal("12345678901", result.Cpf);
        }

        [Fact]
        public async Task CriarAsync_DeveInserirPessoa()
        {
            // Arrange
            var pessoa = new PessoaFisica { Cpf = "12345678901" };
            _mockCollection.Setup(x => x.InsertOneAsync(pessoa, null, default))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var repo = new PessoaFisicaMongoRepository(_mockCollection.Object);

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
            var pessoa = new PessoaFisica { Cpf = "12345678901" };
            _mockCollection.Setup(x => x.ReplaceOneAsync(
                    It.IsAny<FilterDefinition<PessoaFisica>>(),
                    pessoa,
                    It.IsAny<ReplaceOptions>(),
                    default))
                .ReturnsAsync((ReplaceOneResult)null)
                .Verifiable();

            var repo = new PessoaFisicaMongoRepository(_mockCollection.Object);

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
                    It.IsAny<FilterDefinition<PessoaFisica>>(),
                    default))
                .ReturnsAsync((DeleteResult)null)
                .Verifiable();

            var repo = new PessoaFisicaMongoRepository(_mockCollection.Object);

            // Act
            await repo.RemoverAsync("12345678901");

            // Assert
            _mockCollection.Verify();
        }
    }
}
