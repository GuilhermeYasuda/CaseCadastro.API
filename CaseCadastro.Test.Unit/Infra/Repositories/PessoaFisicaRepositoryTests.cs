using CaseCadastro.Domain.Domain;
using CaseCadastro.Infra.DbContext;
using CaseCadastro.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CaseCadastro.Test.Unit.Infra.Repositories
{
    public class PessoaFisicaRepositoryTests
    {
        private CaseDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<CaseDbContext>()
                .UseInMemoryDatabase(databaseName: "PessoaFisicaTestDb")
                .Options;
            return new CaseDbContext(options);
        }

        [Fact]
        public async Task Adicionar_DevePersistirPessoaFisica()
        {
            using var context = CreateDbContext();
            var repo = new PessoaFisicaRepository(context);
            var pessoa = new PessoaFisica { Nome = "Fulano", Cpf = "12345678900" };

            await repo.CriarAsync(pessoa);
            var salvo = context.PessoasFisicas.FirstOrDefault(p => p.Cpf == "12345678900");

            Assert.NotNull(salvo);
            Assert.Equal("Fulano", salvo.Nome);
        }

        [Fact]
        public async Task ObterPorCpf_DeveRetornarPessoaFisica()
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

            context.PessoasFisicas.Add(new PessoaFisica { Nome = "Fulano", Cpf = "98765432100", Endereco = endereco });
            context.SaveChanges();

            var repo = new PessoaFisicaRepository(context);
            var pessoa = await repo.ObterPorCpfAsync("98765432100");

            Assert.NotNull(pessoa);
            Assert.Equal("Fulano", pessoa.Nome);
        }

        [Fact]
        public async Task AtualizarAsync_DeveAtualizarPessoaFisica()
        {
            using var context = CreateDbContext();
            var repo = new PessoaFisicaRepository(context);
            var pessoa = new PessoaFisica { Nome = "Antigo", Cpf = "11111111111" };
            context.PessoasFisicas.Add(pessoa);
            context.SaveChanges();

            pessoa.Nome = "Novo";
            var atualizado = await repo.AtualizarAsync(pessoa);

            Assert.NotNull(atualizado);
            Assert.Equal("Novo", atualizado.Nome);
        }

        [Fact]
        public async Task AtualizarAsync_DeveRetornarNull_SeNaoExistir()
        {
            using var context = CreateDbContext();
            var repo = new PessoaFisicaRepository(context);
            var pessoa = new PessoaFisica { Nome = "Inexistente", Cpf = "22222222222" };

            var atualizado = await repo.AtualizarAsync(pessoa);

            Assert.Null(atualizado);
        }

        [Fact]
        public async Task RemoverAsync_DeveRemoverPessoaFisica()
        {
            using var context = CreateDbContext();
            var repo = new PessoaFisicaRepository(context);
            var pessoa = new PessoaFisica { Nome = "Remover", Cpf = "33333333333" };
            context.PessoasFisicas.Add(pessoa);
            context.SaveChanges();

            await repo.RemoverAsync("33333333333");
            var removido = context.PessoasFisicas.FirstOrDefault(p => p.Cpf == "33333333333");

            Assert.Null(removido);
        }

        [Fact]
        public async Task RemoverAsync_NaoFazNada_SeNaoExistir()
        {
            using var context = CreateDbContext();
            var repo = new PessoaFisicaRepository(context);

            // Não deve lançar exceção
            await repo.RemoverAsync("44444444444");
            Assert.Empty(context.PessoasFisicas);
        }
    }
}