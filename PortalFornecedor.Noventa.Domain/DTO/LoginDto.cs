using System.ComponentModel.DataAnnotations;

namespace PortalFornecedor.Noventa.Domain.DTO
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
