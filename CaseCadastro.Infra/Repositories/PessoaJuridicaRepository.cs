using CaseCadastro.Domain.Domain;
using CaseCadastro.Domain.Interfaces.Repositories;
using CaseCadastro.Infra.DbContext;
using Microsoft.EntityFrameworkCore;

namespace CaseCadastro.Infra.Repositories
{
    public class PessoaJuridicaRepository : IPessoaJuridicaRepository
    {
        private readonly CaseDbContext _context;

        public PessoaJuridicaRepository(CaseDbContext context)
        {
            _context = context;
        }

        public async Task<PessoaJuridica> ObterPorCnpjAsync(string cnpj) =>
            await _context.PessoasJuridicas.Include(x => x.Endereco).FirstOrDefaultAsync(x => x.Cnpj == cnpj);

        public async Task<PessoaJuridica> CriarAsync(PessoaJuridica pessoa)
        {
            _context.PessoasJuridicas.Add(pessoa);
            await _context.SaveChangesAsync();
            return pessoa;
        }

        public async Task<PessoaJuridica> AtualizarAsync(PessoaJuridica pessoa)
        {
            var existente = await _context.PessoasJuridicas.FindAsync(pessoa.Cnpj);
            if (existente == null) return null;
            existente.RazaoSocial = pessoa.RazaoSocial;
            existente.Cnpj = pessoa.Cnpj;
            existente.Endereco = pessoa.Endereco;
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task RemoverAsync(string cnpj)
        {
            var pessoa = await _context.PessoasJuridicas.FindAsync(cnpj);
            if (pessoa != null)
            {
                _context.PessoasJuridicas.Remove(pessoa);
                await _context.SaveChangesAsync();
            }
        }
    }
}
