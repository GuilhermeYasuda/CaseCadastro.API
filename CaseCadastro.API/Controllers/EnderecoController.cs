using CaseCadastro.Application.Interfaces.Services;
using CaseCadastro.Domain.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CaseCadastro.API.Controllers
{
    /// <summary>
    /// Controlador responsável por consultas de endereço.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _service;

        /// <summary>
        /// Construtor do controlador de solicitações de consulta de endereço.
        /// </summary>
        /// <param name="service">Serviço de endereço.</param>
        public EnderecoController(IEnderecoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Consulta um endereço a partir do CEP fornecido.
        /// </summary>
        /// <param name="cep">CEP do endereço a ser consultado.</param>
        /// <returns>Endereço do CEP fornecido.</returns>
        [HttpGet("{cep}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Endereco))]
        public async Task<IActionResult> Get(string cep) =>
            Ok(await _service.ConsultarCepAsync(cep));

        /// <summary>
        /// Lista todos os endereços que correspondem aos parâmetros de UF, cidade e logradouro fornecidos.
        /// </summary>
        /// <param name="uf">UF do endereço a ser consultado.</param>
        /// <param name="cidade">Cidade do endereço a ser consultado.</param>
        /// <param name="logradouro">Logradouro do endereço a ser consultado.</param>
        /// <returns>Todos os endereços que correspondem aos parâmetros.</returns>
        [HttpGet("{uf}/{cidade}/{logradouro}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICollection<Endereco>))]
        public async Task<IActionResult> Get(string uf, string cidade, string logradouro) =>
            Ok(await _service.ConsultarEnderecoAsync(uf, cidade, logradouro));
    }
}
