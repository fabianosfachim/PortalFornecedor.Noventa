namespace PortalFornecedor.Noventa.Domain.Model
{
    public class LoginRequest
    {
        public int? Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Nome { get; set; }
        public string? CnpjCpf { get; set; }
    }
}
