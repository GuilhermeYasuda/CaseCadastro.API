using CaseCadastro.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace CaseCadastro.Infra.DbContext
{
    public class CaseDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public CaseDbContext(DbContextOptions<CaseDbContext> options) : base(options)
        {
        }

        public DbSet<PessoaFisica> PessoasFisicas { get; set; }
        public DbSet<PessoaJuridica> PessoasJuridicas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configuração para PessoaFisica
            modelBuilder.Entity<PessoaFisica>(entity =>
            {
                entity.HasKey(e => e.Cpf);
                entity.Property(e => e.Cpf).IsRequired().HasMaxLength(11);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.HasOne(e => e.Endereco)
                      .WithMany()
                      .HasForeignKey("EnderecoId")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // configuração para PessoaJuridica
            modelBuilder.Entity<PessoaJuridica>(entity =>
            {
                entity.HasKey(e => e.Cnpj);
                entity.Property(e => e.Cnpj).IsRequired().HasMaxLength(18);
                entity.Property(e => e.RazaoSocial).IsRequired().HasMaxLength(200);
                entity.HasOne(e => e.Endereco)
                      .WithMany()
                      .HasForeignKey("EnderecoId")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // configuração para Endereco
            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Cep).IsRequired().HasMaxLength(9);
                entity.Property(e => e.Logradouro).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Complemento).HasMaxLength(50);
                entity.Property(e => e.Unidade).HasMaxLength(10);
                entity.Property(e => e.Bairro).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Localidade).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Uf).IsRequired().HasMaxLength(2);
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(20);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
