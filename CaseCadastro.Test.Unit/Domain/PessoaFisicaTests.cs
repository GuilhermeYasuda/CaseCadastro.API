using CaseCadastro.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Estado = "São Paulo",
                Regiao = "Sudeste",
                Ibge = "3550308",
                Gia = "1004",
                Ddd = "11",
                Siafi = "7107"
            };

            Guid id = Guid.NewGuid();
            var pessoa = new PessoaFisica
            {
                Id = id,
                Nome = "Fulano de Tal",
                Cpf = "12345678909",
                Endereco = endereco
            };

            // Assert
            Assert.Equal(id, pessoa.Id);
            Assert.Equal("Fulano de Tal", pessoa.Nome);
            Assert.Equal("12345678909", pessoa.Cpf);
            Assert.Equal(endereco, pessoa.Endereco);
        }
    }
}
