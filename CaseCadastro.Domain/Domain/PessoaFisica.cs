using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CaseCadastro.Domain.Domain
{
    public class PessoaFisica
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Nome { get; set; }
        [Key]
        public string Cpf { get; set; }
        public Endereco Endereco { get; set; }
    }
}
