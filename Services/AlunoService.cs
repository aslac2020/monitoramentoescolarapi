using Microsoft.EntityFrameworkCore;
using MonitoramentoEscolarAPI.Data;
using MonitoramentoEscolarAPI.DTOs;
using MonitoramentoEscolarAPI.Models;
using MonitoramentoEscolarAPI.Repository;

namespace MonitoramentoEscolarAPI.Services
{
    public class AlunoService : IAlunoRepository
    {
        public readonly ApplicationDbContext _db;
        public readonly IConfiguration _configuration;

        public AlunoService(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;

        }

        public async Task<IEnumerable<AlunoModel>> ListarTodosAlunos()
        {
            return await _db.Alunos.AsNoTracking().ToListAsync();
        }

        public async Task<AlunoModel?> BuscarAlunoPorId(Guid id)
        {
            return await _db.Alunos.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<AlunoResponseDto> CadastrarAluno(AlunoCreateDto request)
        {
            var aluno = new AlunoModel
            {
                Id = Guid.NewGuid(),
                Nome = request.nome,
                ResponsavelId = request.responsavelId,
                Lat = request.latitude,
                Lon = request.longitude
            };

            _db.Alunos.Add(aluno);
            await _db.SaveChangesAsync();
            return new AlunoResponseDto(aluno.Id, aluno.Nome, aluno.ResponsavelId, aluno.Lat, aluno.Lon);
        }


        public async Task<(bool Sucess, string Message)> AtualizarAluno(Guid id, AlunoUpdateDto request)
        {
            var aluno = await _db.Alunos.FindAsync(id);
            if(aluno == null)
            return (false, "Aluno não encontrado.");

            aluno.Nome = request.nome;
            aluno.ResponsavelId = request.responsavelId;
            aluno.Lat = request.latitude;
            aluno.Lon = request.longitude;

            _db.Entry(aluno).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return (true, "Aluno atualizado com sucesso.");
        }


       
        public async Task<(bool Sucess, string Message)> DeletarAluno(Guid id)
        {
             var aluno = await _db.Alunos.FindAsync(id);
            if(aluno == null)
            return (false, "Aluno não encontrado.");

            _db.Alunos.Remove(aluno);
            await _db.SaveChangesAsync();

            return (true, "Aluno removido com sucesso.");
        }

       
    }
}
