﻿using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Data.Repositories.Entities
{
    public class MotivoCotacaoRepository : EntityBaseRepository<Cotacao_Motivo>, IMotivoCotacaoRepository
    {
        private readonly ApplicationContext _db;
        public MotivoCotacaoRepository(ApplicationContext context) : base(context)
        {
            _db = context;
        }
    }
}
