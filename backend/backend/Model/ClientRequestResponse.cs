using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Model
{
    public class ClientRequestResponse
    {
        public string Nome { get; set; }

        [MaxLength(11)]
        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        static public ClientRequestResponse ConvertToClientResponse(Client client)
        {
            return new ClientRequestResponse
            {
                Nome = client.Nome,
                CPF = client.CPF,
                DataNascimento = client.DataNascimento
            };
        }
    }
}
