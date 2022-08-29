using backend.Model.Base;

namespace backend.Model
{
    public class LocationFormatted : BaseEntity
    {
        public string Cliente { get; set; }

        public string Filme { get; set; }

        public DateTime DataLocacao { get; set; }

        public DateTime? DataDevolucao { get; set; }
    }
}
