
using MediatR;
using System.ComponentModel.DataAnnotations;
using Teste.AM53.Domain.Domain;
using Teste.AM53.Domain.Interfaces.Repository;

namespace Teste.AM53.Application.Commands.Produto
{
    public class CreateProdutoCommand:IRequest<Response<Domain.Entities.Produto>>
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sku { get; set; }
        [Required]
        public decimal Preco { get; set; }

        public class CreateProdutoHandler : IRequestHandler<CreateProdutoCommand, Response<Domain.Entities.Produto>>
        {
            private readonly IProdutoRespository _produtoRespository;
            private readonly IMediator _mediator;

            public CreateProdutoHandler(
                IProdutoRespository produtoRespository, 
                IMediator mediator
            )
            {
                _produtoRespository = produtoRespository;
                _mediator = mediator;
            }

            public async Task<Response<Domain.Entities.Produto>> Handle(CreateProdutoCommand command,
                CancellationToken cancellationToken)
            {
                try
                {
                    var produto = new Domain.Entities.Produto()
                    {
                        Nome = command.Nome,
                        Sku = command.Sku,
                        Preco = command.Preco,
                        CreatedAt = DateTime.Now
                        
                    };

                    await _produtoRespository.CreateAsync(produto);

                    return new Response<Domain.Entities.Produto>()
                    {
                        Sucesso = true,
                        Data = produto,
                        Mensagem = "Produto cadastrado com Sucesso!"
                    };
                }
                catch (Exception ex)
                {

                    return new Response<Domain.Entities.Produto>()
                    {
                        Sucesso = false,
                        Data = null,
                        Mensagem = $"Erro ao cadastrar o produto [Error] ==> {ex.Message}"
                    };
                }
            }
        }

    }
}
