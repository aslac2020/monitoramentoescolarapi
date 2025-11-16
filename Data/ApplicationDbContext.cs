using Microsoft.EntityFrameworkCore;
using MonitoramentoEscolarAPI.Models;

namespace MonitoramentoEscolarAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts) { }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<AlunoModel> Alunos { get; set; }
        public DbSet<MotoristaModel> Motoristas { get; set; }
        public DbSet<RotaModel> Rotas { get; set; }
        public DbSet<RotaAlunoModel> RotasAlunos { get; set; }
        public DbSet<LocalizacaoModel> Localizacoes { get; set; }
        public DbSet<NotificacaoModel> Notificacoes { get; set; }
        public DbSet<ResetarSenhaModel> ResetarSenhas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<UsuarioModel>().ToTable("usuarios");
                modelBuilder.Entity<AlunoModel>().ToTable("alunos");
                modelBuilder.Entity<MotoristaModel>().ToTable("motoristas");
                modelBuilder.Entity<RotaModel>().ToTable("rotas");
                modelBuilder.Entity<RotaAlunoModel>().ToTable("rotaaulunos");
                modelBuilder.Entity<LocalizacaoModel>().ToTable("localizacoes");
                modelBuilder.Entity<NotificacaoModel>().ToTable("notificacoes");
                modelBuilder.Entity<ResetarSenhaModel>().ToTable("resetarsenhas");


             // 🔹 Chave composta para tabela de junção
            modelBuilder.Entity<RotaAlunoModel>()
                .HasKey(ra => new { ra.RotaId, ra.AlunoId });

            // 🔹 Relacionamentos Rota ↔ RotaAluno ↔ Aluno
            modelBuilder.Entity<RotaAlunoModel>()
                .HasOne(ra => ra.Rota)
                .WithMany(r => r.RotasAlunos)
                .HasForeignKey(ra => ra.RotaId);

            modelBuilder.Entity<RotaAlunoModel>()
                .HasOne(ra => ra.Aluno)
                .WithMany()
                .HasForeignKey(ra => ra.AlunoId);

            // 🔹 Relacionamento Motorista → Rota
            modelBuilder.Entity<RotaModel>()
                .HasOne<MotoristaModel>()
                .WithMany()
                .HasForeignKey(r => r.MotoristaId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Relacionamento Motorista → Localizacao
            modelBuilder.Entity<LocalizacaoModel>()
                .HasIndex(l => l.DataHora);

            // 🔹 Relacionamento Aluno → Usuario (Responsável)
            modelBuilder.Entity<AlunoModel>()
                .HasOne<UsuarioModel>()
                .WithMany()
                .HasForeignKey(a => a.ResponsavelId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }
    }


}
       
         
