using MongoDB.Bson.Serialization.Attributes;

namespace CaseCadastro.Domain.Domain
{
    public class Endereco
    {
        [BsonIgnore]
        public Guid? Id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Unidade { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Estado { get; set; }
    }
}
