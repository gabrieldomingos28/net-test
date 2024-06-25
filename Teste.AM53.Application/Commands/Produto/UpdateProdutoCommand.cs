using MediatR;
using System.ComponentModel.DataAnnotations;
using Teste.AM53.Domain.Domain;
using Teste.AM53.Domain.Interfaces.Repository;

namespace Teste.AM53.Application.Commands.Produto
{
    public class UpdateProdutoCommand:IRequest<Response<Domain.Entities.Produto>>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sku { get; set; }
        [Required]
        public decimal Preco { get; set; }

        public class UpdateProdutoCommandHandler : IRequestHandler<UpdateProdutoCommand, Response<Domain.Entities.Produto>>
        {
            private readonly IProdutoRespository _produtoRespository;
            private readonly IMediator _mediator;

            public UpdateProdutoCommandHandler(
                IProdutoRespository produtoRespository,
                IMediator mediator
            )
            {
                _produtoRespository = produtoRespository;
                _mediator = mediator;
            }

            public async Task<Response<Domain.Entities.Produto>> Handle(
                UpdateProdutoCommand command,
                CancellationToken cancellationToken
            )
            {
                try
                {

                    var produto = await _produtoRespository.GetByIdAsync(command.Id);

                    if (produto is null)
                    {
                        return new Response<Domain.Entities.Produto>()
                        {
                            Sucesso = false,
                            Data = null,
                            Mensagem = "Produto não encontrado, ID Incorreto!"
                        };

                    }

                    produto.Nome = command.Nome;
                    produto.Sku  = command.Sku;
                    produto.Preco = command.Preco;

                    await _produtoRespository.UpdateAsync(produto);

                    return new Response<Domain.Entities.Produto>()
                    {
                        Sucesso = true,
                        Data = produto,
                        Mensagem = "Produto atualizado com Sucesso!"
                    };
                }
                catch (Exception ex)
                {

                    return new Response<Domain.Entities.Produto>()
                    {
                        Sucesso = false,
                        Data = null,
                        Mensagem = $"Erro ao atualizar o produto [Error] ==> {ex.Message}"
                    };
                }
            }
        }

    }
}
