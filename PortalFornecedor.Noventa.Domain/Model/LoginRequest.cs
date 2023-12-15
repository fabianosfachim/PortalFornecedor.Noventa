using PortalFornecedor.Noventa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
