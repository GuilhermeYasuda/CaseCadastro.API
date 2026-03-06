using CaseCadastro.Domain.Domain;

namespace CaseCadastro.Test.Unit.Domain
{
    public class PessoaFisicaTests
    {
        [Fact]
        public void Deve_Criar_PessoaFisica_Com_Dados_Validos()
        {
            // Arrange
            var endereco = new Endereco
            {
                Cep = "01001-000",
                Logradouro = "Praça da Sé",
                Complemento = "lado ímpar",
                Unidade = "",
                Bairro = "Sé",
                Localidade = "São Paulo",
                Uf = "SP",
                Estado = "São Paulo"
            };

            Guid id = Guid.NewGuid();
            var pessoa = new PessoaFisica
            {
                Nome = "Fulano de Tal",
                Cpf = "12345678909",
                Endereco = endereco
            };

            // Assert
            Assert.NotNull(pessoa);
            Assert.Null(pessoa.Id);
            Assert.Equal("Fulano de Tal", pessoa.Nome);
            Assert.Equal("12345678909", pessoa.Cpf);
            Assert.Equal(endereco, pessoa.Endereco);
        }
    }
}
