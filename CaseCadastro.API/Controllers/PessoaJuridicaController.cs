using CaseCadastro.Application.Interfaces.Services;
using CaseCadastro.Domain.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CaseCadastro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaJuridicaController : ControllerBase
    {
        private readonly IPessoaJuridicaService _service;

        public PessoaJuridicaController(IPessoaJuridicaService service)
        {
            _service = service;
        }

        [HttpGet("{cnpj}")]
        public async Task<IActionResult> Get(string cnpj) =>
            Ok(await _service.ObterPorCnpjAsync(cnpj));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PessoaJuridica pessoa) =>
            Ok(await _service.CriarAsync(pessoa));

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PessoaJuridica pessoa) =>
            Ok(await _service.AtualizarAsync(pessoa));

        [HttpDelete("{cnpj}")]
        public async Task<IActionResult> Delete(string cnpj)
        {
            await _service.RemoverAsync(cnpj);
            return NoContent();
        }
    }
}
