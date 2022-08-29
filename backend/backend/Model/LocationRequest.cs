using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Model
{
    [Table("Locacao")]
    public class LocationRequest
    {
        [Column("Id_Cliente")]
        public int ClientId { get; set; }

        [Column("Id_Filme")]
        public int MovieId { get; set; }

        [Column("DataLocacao")]
        public DateTime DataLocacao { get; set; }

        [Column("DataDevolucao")]
        public DateTime? DataDevolucao { get; set; } = null;
    }
}
