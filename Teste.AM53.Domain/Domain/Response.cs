
namespace Teste.AM53.Domain.Domain
{
    public class Response<T>
    {
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
        public T? Data {  get; set; } 
    }
}
