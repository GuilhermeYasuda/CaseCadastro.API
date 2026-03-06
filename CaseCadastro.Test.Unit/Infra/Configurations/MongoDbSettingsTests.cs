using CaseCadastro.Infra.Configurations;

namespace CaseCadastro.Test.Unit.Infra.Configurations
{
    public class MongoDbSettingsTests
    {
        [Fact]
        public void Deve_Criar_MongoDbSettings_Com_Dados_Validos()
        {
            var settings = new MongoDbSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                Database = "TestDb"
            };

            Assert.Equal("mongodb://localhost:27017", settings.ConnectionString);
            Assert.Equal("TestDb", settings.Database);
        }
    }
}
