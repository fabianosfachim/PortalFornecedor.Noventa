

namespace PortalFornecedor.Noventa.Domain.Model
{
    public class StatusCotacao
    {
       public enum Status
        {
            Pendente = 1,
            Enviada = 2,
            Aprovada = 3,
            NaoAprovada = 4
        }
    }
}
