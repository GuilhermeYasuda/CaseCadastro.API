using CaseCadastro.API.Configurations.Profiles;
using CaseCadastro.Application.Interfaces.Services;
using CaseCadastro.Application.Services;
using CaseCadastro.Domain.Interfaces.ExternalServices;
using CaseCadastro.Domain.Interfaces.Repositories;
using CaseCadastro.Infra.Config;
using CaseCadastro.Infra.DbContext;
using CaseCadastro.Infra.ExternalServices;
using CaseCadastro.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SupportNonNullableReferenceTypes();
});

builder.Services.AddScoped<IEnderecoService, EnderecoService>();
builder.Services.AddScoped<IPessoaFisicaService, PessoaFisicaService>();
builder.Services.AddScoped<IPessoaJuridicaService, PessoaJuridicaService>();

builder.Services.AddHttpClient<IViaCepExternalService, ViaCepExternalService>(x =>
    x.BaseAddress = new Uri(builder.Configuration.GetSection("AppSettings:ViaCepUrl").Value)
);

builder.Services.AddAutoMapper(
    typeof(AutoMappingHttpResponseToEntityProfile)
);

var dbProvider = builder.Configuration["DatabaseProvider"];

if (dbProvider == "Sql")
{
    builder.Services.AddScoped<IPessoaFisicaRepository, PessoaFisicaRepository>();
    builder.Services.AddScoped<IPessoaJuridicaRepository, PessoaJuridicaRepository>();

    var folder = Environment.SpecialFolder.LocalApplicationData;
    var path = Environment.GetFolderPath(folder);
    string DbPath = System.IO.Path.Join(path, "case.db");

    builder.Services.AddDbContext<CaseDbContext>(options =>
        options.UseSqlite($"Data Source={DbPath}")
    );
}
else if (dbProvider == "Mongo")
{
    builder.Services.Configure<MongoDbSettings>(
        builder.Configuration.GetSection("MongoDbSettings"));

    builder.Services.AddScoped<IPessoaFisicaRepository, PessoaFisicaMongoRepository>();
    builder.Services.AddScoped<IPessoaJuridicaRepository, PessoaJuridicaMongoRepository>();
}

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