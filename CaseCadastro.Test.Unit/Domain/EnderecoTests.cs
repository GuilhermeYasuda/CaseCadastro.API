using CaseCadastro.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseCadastro.Test.Unit.Domain
{
    public class EnderecoTests
    {
        [Fact]
        public void Deve_Criar_Endereco_Com_Dados_Validos()
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
                Estado = "São Paulo",
                Regiao = "Sudeste",
                Ibge = "3550308",
                Gia = "1004",
                Ddd = "11",
                Siafi = "7107"
            };

            // Assert
            Assert.Equal("01001-000", endereco.Cep);
            Assert.Equal("Praça da Sé", endereco.Logradouro);
            Assert.Equal("lado ímpar", endereco.Complemento);
            Assert.Equal("", endereco.Unidade);
            Assert.Equal("Sé", endereco.Bairro);
            Assert.Equal("São Paulo", endereco.Localidade);
            Assert.Equal("SP", endereco.Uf);
            Assert.Equal("São Paulo", endereco.Estado);
            Assert.Equal("Sudeste", endereco.Regiao);
            Assert.Equal("3550308", endereco.Ibge);
            Assert.Equal("1004", endereco.Gia);
            Assert.Equal("11", endereco.Ddd);
            Assert.Equal("7107", endereco.Siafi);
        }
    }
}
