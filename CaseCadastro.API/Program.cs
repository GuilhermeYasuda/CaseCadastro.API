using CaseCadastro.API.Configurations.Profiles;
using CaseCadastro.Application.Interfaces.Services;
using CaseCadastro.Application.Services;
using CaseCadastro.Domain.Interfaces.ExternalServices;
using CaseCadastro.Infra.ExternalServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEnderecoService, EnderecoService>();

builder.Services.AddHttpClient<IViaCepExternalService, ViaCepExternalService>(x =>
    x.BaseAddress = new Uri(builder.Configuration.GetSection("AppSettings:ViaCepUrl").Value)
);

builder.Services.AddAutoMapper(
    typeof(AutoMappingHttpResponseToEntityProfile)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
