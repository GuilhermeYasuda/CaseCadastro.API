using CaseCadastro.Application.Interfaces.Services;
using CaseCadastro.Domain.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CaseCadastro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaFisicaController : ControllerBase
    {
        private readonly IPessoaFisicaService _service;

        public PessoaFisicaController(IPessoaFisicaService service)
        {
            _service = service;
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> Get(string cpf) =>
            Ok(await _service.ObterPorCpfAsync(cpf));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PessoaFisica pessoa) =>
            Ok(await _service.CriarAsync(pessoa));

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PessoaFisica pessoa) =>
            Ok(await _service.AtualizarAsync(pessoa));

        [HttpDelete("{cpf}")]
        public async Task<IActionResult> Delete(string cpf)
        {
            await _service.RemoverAsync(cpf);
            return NoContent();
        }
    }
}
