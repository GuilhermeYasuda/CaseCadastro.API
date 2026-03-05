using Xunit;
using Microsoft.EntityFrameworkCore;
using CaseCadastro.Infra.DbContext;
using CaseCadastro.Domain.Domain;

namespace CaseCadastro.Test.Unit.Infra.DbContext
{
    public class CaseDbContextTests
    {
        private CaseDbContext CreateDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<CaseDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new CaseDbContext(options);
        }

        [Fact]
        public void Deve_Criar_DbContext_Com_DbSets()
        {
            using var context = CreateDbContext("DbContextTest");
            Assert.NotNull(context.PessoasFisicas);
            Assert.NotNull(context.PessoasJuridicas);
            Assert.NotNull(context.Enderecos);
        }

        [Fact]
        public void Deve_Permitir_Adicionar_PessoaFisica()
        {
            using var context = CreateDbContext("DbContextTest");
            var pessoa = new PessoaFisica { Nome = "Teste", Cpf = "00011122233" };
            context.PessoasFisicas.Add(pessoa);
            context.SaveChanges();

            var salvo = context.PessoasFisicas.FirstOrDefault(p => p.Cpf == "00011122233");
            Assert.NotNull(salvo);
            Assert.Equal("Teste", salvo.Nome);
        }

        [Fact]
        public void Deve_Permitir_Adicionar_PessoaJuridica()
        {
            using var context = CreateDbContext("DbContextTest");
            var pessoa = new PessoaJuridica { RazaoSocial = "Empresa", Cnpj = "12345678000199" };
            context.PessoasJuridicas.Add(pessoa);
            context.SaveChanges();

            var salvo = context.PessoasJuridicas.FirstOrDefault(p => p.Cnpj == "12345678000199");
            Assert.NotNull(salvo);
            Assert.Equal("Empresa", salvo.RazaoSocial);
        }

        [Fact]
        public void Deve_Permitir_Adicionar_Endereco()
        {
            using var context = CreateDbContext("DbContextTest");
            Guid id = Guid.NewGuid();
            Endereco endereco = new()
            {
                Id = id,
                Cep = "12345678",
                Logradouro = "Rua Teste",
                Complemento = "",
                Unidade = "",
                Bairro = "Bairro Teste",
                Localidade = "Localidade Teste",
                Uf = "SP",
                Estado = "Estado Teste",
            };
            context.Enderecos.Add(endereco);
            context.SaveChanges();

            var salvo = context.Enderecos.FirstOrDefault(e => e.Cep == "12345678");
            Assert.NotNull(salvo);
            Assert.Equal(endereco, salvo);
        }
    }
}