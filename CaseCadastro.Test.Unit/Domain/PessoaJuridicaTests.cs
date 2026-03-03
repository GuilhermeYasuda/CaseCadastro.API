using CaseCadastro.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Estado = "São Paulo",
                Regiao = "Sudeste",
                Ibge = "3550308",
                Gia = "1004",
                Ddd = "11",
                Siafi = "7107"
            };

            Guid id = Guid.NewGuid();
            var pessoa = new PessoaJuridica
            {
                Id = id,
                RazaoSocial = "Empresa de Tal",
                Cnpj = "12345678901234",
                Endereco = endereco
            };

            // Assert
            Assert.Equal(id, pessoa.Id);
            Assert.Equal("Empresa de Tal", pessoa.RazaoSocial);
            Assert.Equal("12345678901234", pessoa.Cnpj);
            Assert.Equal(endereco, pessoa.Endereco);
        }
    }
}
