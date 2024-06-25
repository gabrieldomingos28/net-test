using Azure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teste.AM53.Application.Commands.Produto;
using Teste.AM53.Application.Commands.Usuario;
using Teste.AM53.Domain.Entities;

namespace Teste.AM53.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            if (loginCommand is null)
            {
                return this.BadRequest();
            }

            var resultado = await _mediator.Send(loginCommand);
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

    }
}
