using backend.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Model
{
    [Table("Cliente")]
    public class Client : BaseEntity
    {
        [Column("Nome")]
        public string Nome { get; set; }

        [Column("CPF")]
        [MaxLength(11)]
        public string CPF { get; set; }

        [Column("DataNascimento")]
        public DateTime DataNascimento { get; set; }
    }
}
