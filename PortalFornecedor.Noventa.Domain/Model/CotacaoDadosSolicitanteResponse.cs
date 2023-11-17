using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Domain.Model
{
    public class CotacaoDadosSolicitanteResponse
    {
        public Cotacao_Dados_Solicitante solicitante { get; set; }
        public bool Executado { get; set; }
        public string MensagemRetorno { get; set; }
    }
}

