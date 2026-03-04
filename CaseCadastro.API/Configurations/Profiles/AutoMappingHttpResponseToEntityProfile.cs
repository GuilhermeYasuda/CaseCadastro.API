using AutoMapper;
using CaseCadastro.Domain.Domain;
using CaseCadastro.Infra.ExternalServices.Responses;

namespace CaseCadastro.API.Configurations.Profiles
{
    public class AutoMappingHttpResponseToEntityProfile : Profile
    {
        public AutoMappingHttpResponseToEntityProfile()
        {
            CreateMap<ViaCepResponse, Endereco>();
        }
    }
}
