using backend.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Model
{
    [Table("Locacao")]
    public class Location : BaseEntity
    {
        [Column("Id_Cliente")]
        public int ClientId { get; set; }

        [Column("Id_Filme")]
        public int MovieId { get; set; }

        [Column("DataLocacao")]
        public DateTime DataLocacao { get; set; }

        [Column("DataDevolucao")]
        public DateTime? DataDevolucao { get; set; }
    }
}
