using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Teste.AM53.Application.Commands.Produto;
using Teste.AM53.Domain.Domain;
using Teste.AM53.Domain.Interfaces.Repository;

namespace Teste.AM53.Application.Commands.Usuario
{
    public class LoginCommand : IRequest<LoginResponse>
    {

        public string Email { get; set; }
        public string Senha { get; set; }
        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
        {
            private readonly IUsuarioRepository _usuarioRepository;
            private readonly IMediator _mediator;
            private readonly IConfiguration _configuration;

            public LoginCommandHandler(
                IUsuarioRepository usuarioRepository,
                IMediator mediator,
                IConfiguration configuration
            )
            {
                _usuarioRepository = usuarioRepository;
                _mediator = mediator;
                _configuration = configuration;
            }

            public async Task<LoginResponse> Handle(
                LoginCommand command,
                CancellationToken cancellationToken
            )
            {
                try
                {


                    var usuario =  await _usuarioRepository.GetUsuarioAsync(command.Email, command.Senha);
                    if (usuario is null) {
                        return new LoginResponse()
                        {
                            Mensagem = "Login ou Senha incorretos !",
                            Sucesso = false
                        };
                    }

                    var permissoes = await _usuarioRepository.GetPermissoesAsync(usuario.Id);

                    var authClaims = new List<Claim>
                     {
                           new Claim(ClaimTypes.Email, usuario.Email),
                           new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                   };

                    foreach (var permissao in permissoes)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, permissao.Permissao.Descricao));
                    }

                    string token = GenerateToken(authClaims);

                    return new LoginResponse()
                    {
                        Sucesso = true,
                        Token = token,
                        Permissoes = permissoes.Select(_ => _.Permissao.Descricao).ToList(),
                    };

                }
                catch (Exception ex)
                {

                    return new LoginResponse()
                    {
                        Mensagem = $"Erro ao validar o login [Erro]=>{ex.Message}",
                        Sucesso = false
                    };
                }
            }

            private string GenerateToken(IEnumerable<Claim> claims)
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _configuration["JWT:ValidIssuer"],
                    Audience = _configuration["JWT:ValidAudience"],
                    Expires = DateTime.UtcNow.AddHours(3),
                    SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                    Subject = new ClaimsIdentity(claims)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }

    }
}
