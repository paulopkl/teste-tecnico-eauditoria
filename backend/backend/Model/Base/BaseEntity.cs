using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Model.Base
{
    public class BaseEntity
    {
        [Column("Id")]
        public long Id { get; set; }
    }
}
