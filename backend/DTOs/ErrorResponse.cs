namespace backend_petshop.DTOs
{
    public class ErrorResponse
    {
        public string Mensagem { get; set; } = string.Empty;
        public List<string>? Detalhes { get; set; }
    }
}
