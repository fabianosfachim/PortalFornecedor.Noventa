namespace PortalFornecedor.Noventa.Domain.Entities
{
    public class Login : EntityBase
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nome { get; set; }
        public string NomeUsuarioCadastro { get; set; }
        public DateTime DataCadastro { get; set; }
        public string NomeUsuarioAlteracao { get; set; }
        public DateTime DataAlteracaoCadastro { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataUltimaSessaoAtivaUsuario { get; set; }

    }
}
