namespace PortalFornecedor.Noventa.Domain.Entities
{
    public class Recuperar_Dados_Acesso : EntityBase
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime DataValidadeAcesso { get; set; }
    }
}
