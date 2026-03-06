using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CaseCadastro.Domain.Domain
{
    public class PessoaJuridica
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string RazaoSocial { get; set; }
        [Key]
        public string Cnpj { get; set; }
        public Endereco Endereco { get; set; }
    }
}
