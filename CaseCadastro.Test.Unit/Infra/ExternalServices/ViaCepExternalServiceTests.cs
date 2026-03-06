using AutoMapper;
using CaseCadastro.Domain.Domain;
using CaseCadastro.Infra.ExternalServices;
using CaseCadastro.Infra.ExternalServices.Responses;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;

namespace CaseCadastro.Test.Unit.Infra.ExternalServices
{
    public class ViaCepExternalServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<HttpMessageHandler> _httpHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly ViaCepExternalService _service;

        public ViaCepExternalServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _httpHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpHandlerMock.Object);
            _httpClient.BaseAddress = new System.Uri("http://test.com/");
            _service = new ViaCepExternalService(_httpClient, _mapperMock.Object);
        }

        [Fact]
        public async Task ConsultarCepAsync_DeveRetornarEndereco()
        {
            // Arrange
            var cep = "01001000";
            var viaCepResponse = new ViaCepResponse { Cep = cep, Logradouro = "Praça da Sé" };
            var enderecoEsperado = new Endereco { Cep = cep, Logradouro = "Praça da Sé" };

            var json = JsonConvert.SerializeObject(viaCepResponse);
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };

            _httpHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get &&
                        req.RequestUri == new System.Uri($"http://test.com/ws/{cep}/json/")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(responseMessage);

            _mapperMock.Setup(m => m.Map<Endereco>(It.IsAny<ViaCepResponse>()))
                .Returns(enderecoEsperado);

            // Act
            var result = await _service.ConsultarCepAsync(cep);

            // Assert
            Assert.Equal(cep, result.Cep);
            Assert.Equal("Praça da Sé", result.Logradouro);
            _mapperMock.Verify(m => m.Map<Endereco>(It.Is<ViaCepResponse>(v => v.Cep == cep)), Times.Once);
        }

        [Fact]
        public async Task ConsultarEnderecoAsync_DeveRetornarListaDeEnderecos()
        {
            // Arrange
            var uf = "SP";
            var cidade = "Sao Paulo";
            var logradouro = "Sé";
            var viaCepResponses = new List<ViaCepResponse>
            {
                new ViaCepResponse { Cep = "01001000", Logradouro = "Praça da Sé" },
                new ViaCepResponse { Cep = "01002000", Logradouro = "Rua Direita" }
            };
            var enderecosEsperados = new List<Endereco>
            {
                new Endereco { Cep = "01001000", Logradouro = "Praça da Sé" },
                new Endereco { Cep = "01002000", Logradouro = "Rua Direita" }
            };

            var json = JsonConvert.SerializeObject(viaCepResponses);
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };

            _httpHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get &&
                        req.RequestUri == new System.Uri($"http://test.com/ws/{uf}/{cidade}/{logradouro}/json/")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(responseMessage);

            _mapperMock.Setup(m => m.Map<List<Endereco>>(It.IsAny<List<ViaCepResponse>>()))
                .Returns(enderecosEsperados);

            // Act
            var result = await _service.ConsultarEnderecoAsync(uf, cidade, logradouro);

            // Assert
            Assert.Collection(result,
                e => Assert.Equal("Praça da Sé", e.Logradouro),
                e => Assert.Equal("Rua Direita", e.Logradouro)
            );
            _mapperMock.Verify(m => m.Map<List<Endereco>>(It.IsAny<List<ViaCepResponse>>()), Times.Once);
        }
    }
}