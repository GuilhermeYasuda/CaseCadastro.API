using CaseCadastro.Application.Services;
using CaseCadastro.Domain.Domain;
using CaseCadastro.Domain.Interfaces.ExternalServices;
using Moq;

namespace CaseCadastro.Test.Unit.Application
{
    public class EnderecoServiceTests
    {
        private readonly Mock<IViaCepExternalService> _viaCepServiceMock;
        private readonly EnderecoService _service;

        public EnderecoServiceTests()
        {
            _viaCepServiceMock = new Mock<IViaCepExternalService>();
            _service = new EnderecoService(_viaCepServiceMock.Object);
        }

        [Fact]
        public async Task ConsultarCepAsync_DeveRetornarEndereco()
        {
            // Arrange
            var cep = "01001000";
            var endereco = new Endereco { Cep = cep, Logradouro = "Praça da Sé" };
            _viaCepServiceMock.Setup(v => v.ConsultarCepAsync(cep)).ReturnsAsync(endereco);

            // Act
            var result = await _service.ConsultarCepAsync(cep);

            // Assert
            Assert.Equal(cep, result.Cep);
            Assert.Equal("Praça da Sé", result.Logradouro);
            _viaCepServiceMock.Verify(v => v.ConsultarCepAsync(cep), Times.Once);
        }

        [Fact]
        public async Task ConsultarEnderecoAsync_DeveRetornarListaDeEnderecos()
        {
            // Arrange
            var uf = "SP";
            var cidade = "São Paulo";
            var logradouro = "Sé";
            var enderecos = new List<Endereco>
            {
                new Endereco { Cep = "01001000", Logradouro = "Praça da Sé" },
                new Endereco { Cep = "01002000", Logradouro = "Rua Direita" }
            };
            _viaCepServiceMock.Setup(v => v.ConsultarEnderecoAsync(uf, cidade, logradouro)).ReturnsAsync(enderecos);

            // Act
            var result = await _service.ConsultarEnderecoAsync(uf, cidade, logradouro);

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                e => Assert.Equal("Praça da Sé", e.Logradouro),
                e => Assert.Equal("Rua Direita", e.Logradouro)
            );
            _viaCepServiceMock.Verify(v => v.ConsultarEnderecoAsync(uf, cidade, logradouro), Times.Once);
        }
    }
}