using CaseCadastro.Application.Services;
using CaseCadastro.Domain.Domain;
using CaseCadastro.Domain.Interfaces.ExternalServices;
using CaseCadastro.Domain.Interfaces.Repositories;
using Moq;

namespace CaseCadastro.Test.Unit.Application
{
    public class PessoaJuridicaServiceTests
    {
        private readonly Mock<IPessoaJuridicaRepository> _repositoryMock;
        private readonly Mock<IViaCepExternalService> _viaCepServiceMock;
        private readonly PessoaJuridicaService _service;

        public PessoaJuridicaServiceTests()
        {
            _repositoryMock = new Mock<IPessoaJuridicaRepository>();
            _viaCepServiceMock = new Mock<IViaCepExternalService>();
            _service = new PessoaJuridicaService(_repositoryMock.Object, _viaCepServiceMock.Object);
        }

        [Fact]
        public async Task ObterPorCnpjAsync_DeveRetornarPessoa()
        {
            // Arrange
            var cnpj = "12345678901234";
            var pessoa = new PessoaJuridica { Cnpj = cnpj, RazaoSocial = "Empresa Tal" };
            _repositoryMock.Setup(r => r.ObterPorCnpjAsync(cnpj)).ReturnsAsync(pessoa);

            // Act
            var result = await _service.ObterPorCnpjAsync(cnpj);

            // Assert
            Assert.Equal(cnpj, result.Cnpj);
            Assert.Equal("Empresa Tal", result.RazaoSocial);
        }

        [Fact]
        public async Task CriarAsync_DeveConsultarCepEAtribuirEndereco()
        {
            // Arrange
            var pessoa = new PessoaJuridica
            {
                Cnpj = "12345678901234",
                RazaoSocial = "Empresa Tal",
                Endereco = new Endereco { Cep = "01001000" }
            };
            var enderecoViaCep = new Endereco { Cep = "01001000", Logradouro = "Rua Teste" };

            _viaCepServiceMock.Setup(v => v.ConsultarCepAsync("01001000")).ReturnsAsync(enderecoViaCep);
            _repositoryMock.Setup(r => r.CriarAsync(It.IsAny<PessoaJuridica>())).ReturnsAsync((PessoaJuridica p) => p);

            // Act
            var result = await _service.CriarAsync(pessoa);

            // Assert
            Assert.Equal("Rua Teste", result.Endereco.Logradouro);
            _viaCepServiceMock.Verify(v => v.ConsultarCepAsync("01001000"), Times.Once);
            _repositoryMock.Verify(r => r.CriarAsync(It.Is<PessoaJuridica>(p => p.Endereco.Logradouro == "Rua Teste")), Times.Once);
        }

        [Fact]
        public async Task AtualizarAsync_DeveConsultarCepEAtribuirEndereco()
        {
            // Arrange
            var pessoa = new PessoaJuridica
            {
                Cnpj = "12345678901234",
                RazaoSocial = "Empresa Tal",
                Endereco = new Endereco { Cep = "02002000" }
            };
            var enderecoViaCep = new Endereco { Cep = "02002000", Logradouro = "Rua Atualizada" };

            _viaCepServiceMock.Setup(v => v.ConsultarCepAsync("02002000")).ReturnsAsync(enderecoViaCep);
            _repositoryMock.Setup(r => r.AtualizarAsync(It.IsAny<PessoaJuridica>())).ReturnsAsync((PessoaJuridica p) => p);

            // Act
            var result = await _service.AtualizarAsync(pessoa);

            // Assert
            Assert.Equal("Rua Atualizada", result.Endereco.Logradouro);
            _viaCepServiceMock.Verify(v => v.ConsultarCepAsync("02002000"), Times.Once);
            _repositoryMock.Verify(r => r.AtualizarAsync(It.Is<PessoaJuridica>(p => p.Endereco.Logradouro == "Rua Atualizada")), Times.Once);
        }

        [Fact]
        public async Task RemoverAsync_DeveChamarRepositorio()
        {
            // Arrange
            var cnpj = "12345678901234";
            _repositoryMock.Setup(r => r.RemoverAsync(cnpj)).Returns(Task.CompletedTask);

            // Act
            await _service.RemoverAsync(cnpj);

            // Assert
            _repositoryMock.Verify(r => r.RemoverAsync(cnpj), Times.Once);
        }
    }
}