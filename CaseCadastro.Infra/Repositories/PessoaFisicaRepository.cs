using CaseCadastro.Domain.Domain;
using CaseCadastro.Domain.Interfaces.Repositories;
using CaseCadastro.Infra.DbContext;
using Microsoft.EntityFrameworkCore;

namespace CaseCadastro.Infra.Repositories
{
    public class PessoaFisicaRepository : IPessoaFisicaRepository
    {
        private readonly CaseDbContext _context;

        public PessoaFisicaRepository(CaseDbContext context)
        {
            _context = context;
        }

        public async Task<PessoaFisica> ObterPorCpfAsync(string cpf) =>
            await _context.PessoasFisicas.Include(x => x.Endereco).FirstOrDefaultAsync(x => x.Cpf == cpf);

        public async Task<PessoaFisica> CriarAsync(PessoaFisica pessoa)
        {
            _context.PessoasFisicas.Add(pessoa);
            await _context.SaveChangesAsync();
            return pessoa;
        }

        public async Task<PessoaFisica> AtualizarAsync(PessoaFisica pessoa)
        {
            var existente = await _context.PessoasFisicas.FindAsync(pessoa.Cpf);
            if (existente == null) return null;
            existente.Nome = pessoa.Nome;
            existente.Cpf = pessoa.Cpf;
            existente.Endereco = pessoa.Endereco;
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task RemoverAsync(string cpf)
        {
            var pessoa = await _context.PessoasFisicas.FindAsync(cpf);
            if (pessoa != null)
            {
                _context.PessoasFisicas.Remove(pessoa);
                await _context.SaveChangesAsync();
            }
        }
    }
}
