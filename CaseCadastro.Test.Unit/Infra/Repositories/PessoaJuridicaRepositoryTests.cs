using CaseCadastro.Domain.Domain;
using CaseCadastro.Infra.DbContext;
using CaseCadastro.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CaseCadastro.Test.Unit.Infra.Repositories
{
    public class PessoaJuridicaRepositoryTests
    {
        private CaseDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<CaseDbContext>()
                .UseInMemoryDatabase(databaseName: $"PessoaJuridicaTestDb{Guid.NewGuid()}")
                .Options;
            return new CaseDbContext(options);
        }

        [Fact]
        public async Task Adicionar_DevePersistirPessoaJuridica()
        {
            using var context = CreateDbContext();
            var repo = new PessoaJuridicaRepository(context);
            var pessoa = new PessoaJuridica { RazaoSocial = "Empresa Tal", Cnpj = "12345678901234" };

            await repo.CriarAsync(pessoa);
            var salvo = context.PessoasJuridicas.FirstOrDefault(p => p.Cnpj == "12345678901234");

            Assert.NotNull(salvo);
            Assert.Equal("Empresa Tal", salvo.RazaoSocial);
        }

        [Fact]
        public async Task ObterPorCnpj_DeveRetornarPessoaJuridica()
        {
            using var context = CreateDbContext();

            Guid id = Guid.NewGuid();
            Endereco endereco = new Endereco()
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

            context.PessoasJuridicas.Add(new PessoaJuridica { RazaoSocial = "Empresa Tal", Cnpj = "98765432109876", Endereco = endereco });
            context.SaveChanges();

            var repo = new PessoaJuridicaRepository(context);
            var pessoa = await repo.ObterPorCnpjAsync("98765432109876");

            Assert.NotNull(pessoa);
            Assert.Equal("Empresa Tal", pessoa.RazaoSocial);
        }

        [Fact]
        public async Task AtualizarAsync_DeveAtualizarPessoaJuridica()
        {
            using var context = CreateDbContext();
            var repo = new PessoaJuridicaRepository(context);
            var pessoa = new PessoaJuridica { RazaoSocial = "Antigo", Cnpj = "11111111111111" };
            context.PessoasJuridicas.Add(pessoa);
            context.SaveChanges();

            pessoa.RazaoSocial = "Novo";
            var atualizado = await repo.AtualizarAsync(pessoa);

            Assert.NotNull(atualizado);
            Assert.Equal("Novo", atualizado.RazaoSocial);
        }

        [Fact]
        public async Task AtualizarAsync_DeveRetornarNull_SeNaoExistir()
        {
            using var context = CreateDbContext();
            var repo = new PessoaJuridicaRepository(context);
            var pessoa = new PessoaJuridica { RazaoSocial = "Inexistente", Cnpj = "22222222222222" };

            var atualizado = await repo.AtualizarAsync(pessoa);

            Assert.Null(atualizado);
        }

        [Fact]
        public async Task RemoverAsync_DeveRemoverPessoaJuridica()
        {
            using var context = CreateDbContext();
            var repo = new PessoaJuridicaRepository(context);
            var pessoa = new PessoaJuridica { RazaoSocial = "Remover", Cnpj = "33333333333333" };
            context.PessoasJuridicas.Add(pessoa);
            context.SaveChanges();

            await repo.RemoverAsync("33333333333333");
            var removido = context.PessoasJuridicas.FirstOrDefault(p => p.Cnpj == "33333333333333");

            Assert.Null(removido);
        }

        [Fact]
        public async Task RemoverAsync_NaoFazNada_SeNaoExistir()
        {
            using var context = CreateDbContext();
            var repo = new PessoaJuridicaRepository(context);

            // Não deve lançar exceção
            await repo.RemoverAsync("44444444444444");
            Assert.Empty(context.PessoasJuridicas);
        }
    }
}