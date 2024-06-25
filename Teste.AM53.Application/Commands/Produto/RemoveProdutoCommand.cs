
using MediatR;
using Teste.AM53.Domain.Domain;
using Teste.AM53.Domain.Interfaces.Repository;

namespace Teste.AM53.Application.Commands.Produto
{
    public class RemoveProdutoCommand : IRequest<Response<Domain.Entities.Produto>>
    {
       
        public int Id { get; set; }
        public class RemoveProdutoCommandHandler : IRequestHandler<RemoveProdutoCommand, Response<Domain.Entities.Produto>>
        {
            private readonly IProdutoRespository _produtoRespository;
            private readonly IMediator _mediator;

            public RemoveProdutoCommandHandler(
                IProdutoRespository produtoRespository,
                IMediator mediator
            )
            {
                _produtoRespository = produtoRespository;
                _mediator = mediator;
            }

            public async Task<Response<Domain.Entities.Produto>> Handle(
                RemoveProdutoCommand command,
                CancellationToken cancellationToken
            )
            {
                try
                {
                

                    await _produtoRespository.DeleteAsync(command.Id);

                    return new Response<Domain.Entities.Produto>()
                    {
                        Sucesso = true,
                        Data = null,
                        Mensagem = "Produto removido com Sucesso!"
                    };
                }
                catch (Exception ex)
                {

                    return new Response<Domain.Entities.Produto>()
                    {
                        Sucesso = false,
                        Data = null,
                        Mensagem = $"Erro ao remover o produto [Error] ==> {ex.Message}"
                    };
                }
            }
        }

    }
}
