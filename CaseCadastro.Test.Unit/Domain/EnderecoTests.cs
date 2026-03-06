using CaseCadastro.Domain.Domain;

namespace CaseCadastro.Test.Unit.Domain
{
    public class EnderecoTests
    {
        [Fact]
        public void Deve_Criar_Endereco_Com_Dados_Validos()
        {
            Guid id = Guid.NewGuid();
            // Arrange
            var endereco = new Endereco
            {
                Id = id,
                Cep = "01001-000",
                Logradouro = "Praça da Sé",
                Complemento = "lado ímpar",
                Unidade = "",
                Bairro = "Sé",
                Localidade = "São Paulo",
                Uf = "SP",
                Estado = "São Paulo"
            };

            // Assert
            Assert.Equal(id, endereco.Id);
            Assert.Equal("01001-000", endereco.Cep);
            Assert.Equal("Praça da Sé", endereco.Logradouro);
            Assert.Equal("lado ímpar", endereco.Complemento);
            Assert.Equal("", endereco.Unidade);
            Assert.Equal("Sé", endereco.Bairro);
            Assert.Equal("São Paulo", endereco.Localidade);
            Assert.Equal("SP", endereco.Uf);
            Assert.Equal("São Paulo", endereco.Estado);
        }
    }
}
