using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teste.AM53.Application.Commands.Produto;
using Teste.AM53.Application.Query.Produto;
using Teste.AM53.Domain.Domain;
using Teste.AM53.Domain.Entities;

namespace Teste.AM53.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private IMediator _mediator;

        public ProdutoController(IMediator mediator)
        {
            _mediator = mediator; 
        }

        [HttpPost]
        [Authorize(Roles = "Escrita")]
        public async Task<IActionResult> Create(CreateProdutoCommand createProdutoCommand)
        {
            if(createProdutoCommand is null)
            {
                return this.BadRequest(new Response<Produto>() { 
                 Sucesso = false,
                 Data = null,
                 Mensagem = "Objeto Produto nulo"
                });
            }

            var resultado = await _mediator.Send(createProdutoCommand);
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Editar")]
        public async Task<IActionResult> Update(int id, UpdateProdutoCommand updateProdutoCommand)
        {
            if (updateProdutoCommand is null)
            {
                return this.BadRequest(new Response<Produto>()
                {
                    Sucesso = false,
                    Data = null,
                    Mensagem = "Objeto Produto nulo"
                });
            }

            updateProdutoCommand.Id = id;
            var resultado = await _mediator.Send(updateProdutoCommand);
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Delete")]
        public async Task<IActionResult> Delete(int id)
        {
           
            var resultado = await _mediator.Send(new RemoveProdutoCommand() { Id = id });
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Leitura")]
        public async Task<IActionResult> GetById(int id)
        {

            var resultado = await _mediator.Send(new GetProdutoByIdQuery() { Id = id });
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpGet]
        [Authorize(Roles = "Leitura")]
        public async Task<IActionResult> Get()
        {

            var resultado = await _mediator.Send(new GetAllProdutosQuery());
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

    }
}
