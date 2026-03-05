using CaseCadastro.Application.Services;
using CaseCadastro.Domain.Domain;
using CaseCadastro.Domain.Interfaces.ExternalServices;
using CaseCadastro.Domain.Interfaces.Repositories;
using Moq;

namespace CaseCadastro.Test.Unit.Application
{
    public class PessoaFisicaServiceTests
    {
        private readonly Mock<IPessoaFisicaRepository> _repositoryMock;
        private readonly Mock<IViaCepExternalService> _viaCepServiceMock;
        private readonly PessoaFisicaService _service;

        public PessoaFisicaServiceTests()
        {
            _repositoryMock = new Mock<IPessoaFisicaRepository>();
            _viaCepServiceMock = new Mock<IViaCepExternalService>();
            _service = new PessoaFisicaService(_repositoryMock.Object, _viaCepServiceMock.Object);
        }

        [Fact]
        public async Task ObterPorCpfAsync_DeveRetornarPessoa()
        {
            // Arrange
            var cpf = "12345678900";
            var pessoa = new PessoaFisica { Cpf = cpf, Nome = "Fulano" };
            _repositoryMock.Setup(r => r.ObterPorCpfAsync(cpf)).ReturnsAsync(pessoa);

            // Act
            var result = await _service.ObterPorCpfAsync(cpf);

            // Assert
            Assert.Equal(cpf, result.Cpf);
            Assert.Equal("Fulano", result.Nome);
        }

        [Fact]
        public async Task CriarAsync_DeveConsultarCepEAtribuirEndereco()
        {
            // Arrange
            var pessoa = new PessoaFisica
            {
                Cpf = "12345678900",
                Nome = "Fulano",
                Endereco = new Endereco { Cep = "01001000" }
            };
            var enderecoViaCep = new Endereco { Cep = "01001000", Logradouro = "Rua Teste" };

            _viaCepServiceMock.Setup(v => v.ConsultarCepAsync("01001000")).ReturnsAsync(enderecoViaCep);
            _repositoryMock.Setup(r => r.CriarAsync(It.IsAny<PessoaFisica>())).ReturnsAsync((PessoaFisica p) => p);

            // Act
            var result = await _service.CriarAsync(pessoa);

            // Assert
            Assert.Equal("Rua Teste", result.Endereco.Logradouro);
            _viaCepServiceMock.Verify(v => v.ConsultarCepAsync("01001000"), Times.Once);
            _repositoryMock.Verify(r => r.CriarAsync(It.Is<PessoaFisica>(p => p.Endereco.Logradouro == "Rua Teste")), Times.Once);
        }

        [Fact]
        public async Task AtualizarAsync_DeveConsultarCepEAtribuirEndereco()
        {
            // Arrange
            var pessoa = new PessoaFisica
            {
                Cpf = "12345678900",
                Nome = "Fulano",
                Endereco = new Endereco { Cep = "02002000" }
            };
            var enderecoViaCep = new Endereco { Cep = "02002000", Logradouro = "Rua Atualizada" };

            _viaCepServiceMock.Setup(v => v.ConsultarCepAsync("02002000")).ReturnsAsync(enderecoViaCep);
            _repositoryMock.Setup(r => r.AtualizarAsync(It.IsAny<PessoaFisica>())).ReturnsAsync((PessoaFisica p) => p);

            // Act
            var result = await _service.AtualizarAsync(pessoa);

            // Assert
            Assert.Equal("Rua Atualizada", result.Endereco.Logradouro);
            _viaCepServiceMock.Verify(v => v.ConsultarCepAsync("02002000"), Times.Once);
            _repositoryMock.Verify(r => r.AtualizarAsync(It.Is<PessoaFisica>(p => p.Endereco.Logradouro == "Rua Atualizada")), Times.Once);
        }

        [Fact]
        public async Task RemoverAsync_DeveChamarRepositorio()
        {
            // Arrange
            var cpf = "12345678900";
            _repositoryMock.Setup(r => r.RemoverAsync(cpf)).Returns(Task.CompletedTask);

            // Act
            await _service.RemoverAsync(cpf);

            // Assert
            _repositoryMock.Verify(r => r.RemoverAsync(cpf), Times.Once);
        }
    }
}