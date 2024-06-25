namespace Teste.AM53.Domain.Domain
{
    public class LoginResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public string Token { get; set; }
        public List<string> Permissoes { get; set; }
     
    }
}
