﻿using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.Application.Services.Interfaces
{
    public interface IFornecedorServices
    {
        /// <summary>
        /// Realizar o cadastro dos dados do fornecedor no portal
        /// </summary>
        /// <param name="fornecedorRequest">Objeto para cadastro dos dados do fornecedor no portal</param>
        /// <returns>Retornar se os dados do fornecedor foram cadastrados ou não no portal</returns>
        Task<Response<FornecedorResponse>> AdicionarFornecedorAsync(FornecedorRequest fornecedorRequest, string url);

        /// <summary>
        /// Listar o cadastro de um fornecedor no portal pelo cnpj
        /// </summary>
        /// <param name="id">Número de identificação do fornecedor</param>
        /// <returns></returns>
        Task<Response<FornecedorResponse>> ListarDadosFornecedorAsync(string CnpjCpf);

        /// <summary>
        /// Listar o cadastro de um fornecedor no portal pelo número de identificação do fornecedor
        /// </summary>
        /// <param name="id">Número de identificação do fornecedor</param>
        /// <returns></returns>
        Task<Response<FornecedorResponse>> ListarDadosFornecedorAsync(int id);


    }
}
