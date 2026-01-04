using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoramentoEscolarAPI.Models
{
    public class AlunoModel
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = null!;

        public bool? Ativo { get; set; } = true;

        [Column("responsavelId")]
        public Guid ResponsavelId { get; set; }

        public UsuarioModel? Responsavel { get; set; }

        public Guid? EscolaId { get; set; }
        public EscolaModel? Escola { get; set; }

        public Guid? SerieId { get; set; }
        public SerieModel? Serie { get; set; }

        [Column("lat")]
        public double? Lat { get; set; }

        [Column("lon")]
        public double? Lon { get; set; }



    }
}
