using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Model
{
    [Table("Locacao")]
    public class LocationCreation
    {
        [Column("Id_Cliente")]
        public int ClientId { get; set; }

        [Column("Id_Filme")]
        public int MovieId { get; set; }

        [Column("DataLocacao")]
        public DateTime DataLocacao { get; set; }
    }
}
