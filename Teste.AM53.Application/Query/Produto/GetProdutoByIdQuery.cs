using MediatR;
using Teste.AM53.Domain.Domain;
using Teste.AM53.Domain.Interfaces.Repository;

namespace Teste.AM53.Application.Query.Produto
{
    public class GetProdutoByIdQuery : IRequest<Response<Domain.Entities.Produto>>
    {
        public int Id { get; set; }
        public class GetProdutoByIdQueryHandler : IRequestHandler<GetProdutoByIdQuery, Response<Domain.Entities.Produto>>
        {
            private readonly IProdutoRespository _produtoRespository;
            private readonly IMediator _mediator;

            public GetProdutoByIdQueryHandler(
                IProdutoRespository produtoRespository,
                IMediator mediator
            )
            {
                _produtoRespository = produtoRespository;
                _mediator = mediator;
            }

            public async Task<Response<Domain.Entities.Produto>> Handle(
                GetProdutoByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                try
                {


                   var produto = await _produtoRespository.GetByIdAsync(query.Id);

                    if(produto is null)
                    {
                        return new Response<Domain.Entities.Produto>()
                        {
                            Sucesso = false,
                            Data = null,
                            Mensagem = "Produto não encontrado"
                        };
                    }

                    return new Response<Domain.Entities.Produto>()
                    {
                        Sucesso = true,
                        Data = produto
                    };
                }
                catch (Exception ex)
                {

                    return new Response<Domain.Entities.Produto>()
                    {
                        Sucesso = false,
                        Data = null,
                        Mensagem = $"Erro ao buscar  o produto [Error] ==> {ex.Message}"
                    };
                }
            }
        }
    }
}
