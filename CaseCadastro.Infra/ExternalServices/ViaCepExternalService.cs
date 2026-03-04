using AutoMapper;
using CaseCadastro.Domain.Domain;
using CaseCadastro.Domain.Interfaces.ExternalServices;
using CaseCadastro.Infra.ExternalServices.Responses;
using Newtonsoft.Json;

namespace CaseCadastro.Infra.ExternalServices
{
    public class ViaCepExternalService : IViaCepExternalService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public ViaCepExternalService(HttpClient httpClient,
            IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<Endereco> ConsultarCepAsync(string cep)
        {
            var response = await _httpClient.GetAsync($"ws/{cep}/json/");
            response.EnsureSuccessStatusCode();

            var resultJson = await response.Content.ReadAsStringAsync();
            var viaCep = JsonConvert.DeserializeObject<ViaCepResponse>(resultJson);

            return _mapper.Map<Endereco>(viaCep);
        }

        public async Task<IEnumerable<Endereco>> ConsultarEnderecoAsync(string uf, string cidade, string logradouro)
        {
            var response = await _httpClient.GetAsync($"ws/{uf}/{cidade}/{logradouro}/json/");
            response.EnsureSuccessStatusCode();

            var resultJson = await response.Content.ReadAsStringAsync();
            var viaCep = JsonConvert.DeserializeObject<List<ViaCepResponse>>(resultJson);

            return _mapper.Map<List<Endereco>>(viaCep);
        }
    }
}
