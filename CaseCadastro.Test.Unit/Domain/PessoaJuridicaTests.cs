using CaseCadastro.Domain.Domain;

namespace CaseCadastro.Test.Unit.Domain
{
    public class PessoaJuridicaTests
    {
        [Fact]
        public void Deve_Criar_PessoaJuridica_Com_Dados_Validos()
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
            var pessoa = new PessoaJuridica
            {
                RazaoSocial = "Empresa de Tal",
                Cnpj = "12345678901234",
                Endereco = endereco
            };

            // Assert
            Assert.Equal("Empresa de Tal", pessoa.RazaoSocial);
            Assert.Equal("12345678901234", pessoa.Cnpj);
            Assert.Equal(endereco, pessoa.Endereco);
        }
    }
}
