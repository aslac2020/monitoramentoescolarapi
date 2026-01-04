using Microsoft.EntityFrameworkCore;
using MonitoramentoEscolarAPI.Models;

namespace MonitoramentoEscolarAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts) { }

       // --------------------- TABELAS PRINCIPAIS ----------------------
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<AlunoModel> Alunos { get; set; }
        public DbSet<MotoristaModel> Motoristas { get; set; }
        public DbSet<RotaModel> Rotas { get; set; }
        public DbSet<RotaAlunoModel> RotasAlunos { get; set; }
        public DbSet<LocalizacaoModel> Localizacoes { get; set; }
        public DbSet<NotificacaoModel> Notificacoes { get; set; }
        public DbSet<ResetarSenhaModel> ResetarSenhas { get; set; }
        public DbSet<TipoUsuarioModel> TiposUsuarios { get; set; }

        // --------------------- NOVAS TABELAS ----------------------
        public DbSet<EnderecoModel> Enderecos { get; set; }
        public DbSet<UsuarioEnderecoModel> UsuarioEnderecos { get; set; }
        public DbSet<EscolaModel> Escolas { get; set; }
        public DbSet<SerieModel> Series { get; set; }

        public DbSet<StatusAlunoRotaModel> StatusAlunoRota { get; set; }
        public DbSet<AlunoRotaStatusHistoricoModel> AlunoRotaStatusHistorico { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --------------------- MAPEAR NOMES DE TABELAS ----------------------

            modelBuilder.Entity<UsuarioModel>().ToTable("usuarios");
            modelBuilder.Entity<AlunoModel>().ToTable("alunos");
            modelBuilder.Entity<MotoristaModel>().ToTable("motoristas");
            modelBuilder.Entity<RotaModel>().ToTable("rotas");
            modelBuilder.Entity<RotaAlunoModel>().ToTable("roitaalunos");
            modelBuilder.Entity<LocalizacaoModel>().ToTable("localizacoes");
            modelBuilder.Entity<NotificacaoModel>().ToTable("notificacoes");
            modelBuilder.Entity<ResetarSenhaModel>().ToTable("resetarsenhas");
            modelBuilder.Entity<TipoUsuarioModel>().ToTable("tiposusuarios");

            modelBuilder.Entity<EnderecoModel>().ToTable("enderecos");
            modelBuilder.Entity<UsuarioEnderecoModel>().ToTable("usuarioenderecos");
            modelBuilder.Entity<EscolaModel>().ToTable("escolas");
            modelBuilder.Entity<SerieModel>().ToTable("series");

            modelBuilder.Entity<StatusAlunoRotaModel>().ToTable("statusalunorota");
            modelBuilder.Entity<AlunoRotaStatusHistoricoModel>().ToTable("alunorotastatushistorico");

            // --------------------- RELACIONAMENTOS EXISTENTES ----------------------

            // Usuário → Tipo de Usuário
            modelBuilder.Entity<UsuarioModel>()
                .HasOne(u => u.TipoUsuario)
                .WithMany()
                .HasForeignKey(u => u.IdTipoUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // Chave composta da tabela RotaAlunos
            modelBuilder.Entity<RotaAlunoModel>()
                .HasKey(ra => new { ra.RotaId, ra.AlunoId });

            modelBuilder.Entity<RotaAlunoModel>()
                .HasOne(ra => ra.Rota)
                .WithMany(r => r.RotasAlunos)
                .HasForeignKey(ra => ra.RotaId);

            modelBuilder.Entity<RotaAlunoModel>()
                .HasOne(ra => ra.Aluno)
                .WithMany()
                .HasForeignKey(ra => ra.AlunoId);

            // Motorista → Rota
            modelBuilder.Entity<RotaModel>()
                .HasOne(r => r.Motorista)
                .WithMany()
                .HasForeignKey(r => r.MotoristaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Localização → Motorista
            modelBuilder.Entity<LocalizacaoModel>()
                .HasIndex(l => l.DataHora);

            // Aluno → Usuário (Responsável)
            modelBuilder.Entity<AlunoModel>()
                .HasOne(a => a.Responsavel)
                .WithMany()
                .HasForeignKey(a => a.ResponsavelId)
                .OnDelete(DeleteBehavior.Restrict);

            // --------------------- NOVOS RELACIONAMENTOS ----------------------

            // Usuario → Endereco (1:1)
            modelBuilder.Entity<UsuarioEnderecoModel>()
                .HasKey(ue => ue.UsuarioId);

            modelBuilder.Entity<UsuarioEnderecoModel>()
                .HasOne(ue => ue.Usuario)
                .WithOne()
                .HasForeignKey<UsuarioEnderecoModel>(ue => ue.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsuarioEnderecoModel>()
                .HasOne(ue => ue.Endereco)
                .WithMany()
                .HasForeignKey(ue => ue.EnderecoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Motorista → Endereco (N:1)
            modelBuilder.Entity<MotoristaModel>()
                .HasOne(m => m.Endereco)
                .WithMany()
                .HasForeignKey(m => m.EnderecoId)
                .OnDelete(DeleteBehavior.SetNull);

            // Escola → Endereco
            modelBuilder.Entity<EscolaModel>()
                .HasOne(e => e.Endereco)
                .WithMany()
                .HasForeignKey(e => e.EnderecoId)
                .OnDelete(DeleteBehavior.SetNull);

            // Aluno → Escola
            modelBuilder.Entity<AlunoModel>()
                .HasOne(a => a.Escola)
                .WithMany()
                .HasForeignKey(a => a.EscolaId)
                .OnDelete(DeleteBehavior.SetNull);

            // Aluno → Série
            modelBuilder.Entity<AlunoModel>()
                .HasOne(a => a.Serie)
                .WithMany()
                .HasForeignKey(a => a.SerieId)
                .OnDelete(DeleteBehavior.SetNull);

            // StatusAlunoRota → Histórico
            modelBuilder.Entity<AlunoRotaStatusHistoricoModel>()
                .HasOne(h => h.Aluno)
                .WithMany()
                .HasForeignKey(h => h.AlunoId);

            modelBuilder.Entity<AlunoRotaStatusHistoricoModel>()
                .HasOne(h => h.Rota)
                .WithMany()
                .HasForeignKey(h => h.RotaId);

            modelBuilder.Entity<AlunoRotaStatusHistoricoModel>()
                .HasOne(h => h.Status)
                .WithMany()
                .HasForeignKey(h => h.StatusId);

            base.OnModelCreating(modelBuilder);
        }
    }

}
       
         
