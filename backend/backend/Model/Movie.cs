using backend.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Model
{
    [Table("Filme")]
    public class Movie : BaseEntity
    {
        [Column("Titulo")]
        public string Titulo { get; set; }

        [Column("ClassificacaoIndicativa")]
        public int ClassificacaoIndicativa { get; set; }

        [Column("Lancamento")]
        public int Lancamento { get; set; }
    }
}
