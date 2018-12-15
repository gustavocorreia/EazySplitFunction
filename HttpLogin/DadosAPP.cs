using System.Net;
using System.Net.Http;
using System.Data.SqlClient;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using EazySplitFunctions.Models;
using System.Data;
using System.Collections.Generic;
using EasySplitFunctions.Models;
using Newtonsoft.Json;
using System.Text;

namespace EasySplitFunctions.HttpLogin
{
    public static class DadosAPP
    {
        [FunctionName("DadosAPP")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "DadosAPP/token/{token}")]HttpRequestMessage req, string token, TraceWriter log)
        {
            try
            {
                log.Info("Iniciando");

                Cliente cliente = obterCliente(token);
                if (cliente != null)
                {
                    var listaEstabelecimentos = obterEstabelecimentos();
                    var retorno = new Retorno();
                    retorno.Cliente = cliente;
                    retorno.Estabelecimentos = listaEstabelecimentos;

                    var json = JsonConvert.SerializeObject(retorno);

                    var response = req.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(json, Encoding.UTF8, "application/json");

                    log.Info("Sucesso");

                    return response;
                }
            }
            catch (Exception ex)
            {

                log.Error(ex.Message, ex, ex.StackTrace);
                if(ex.InnerException != null)
                {
                    log.Error(ex.InnerException.Message, ex.InnerException, ex.InnerException.StackTrace);
                }
            }

            


            // Fetching the name from the path parameter in the request URL
            return req.CreateResponse(HttpStatusCode.NotFound, "Cliente não encontrado!");
        }

        private static Cliente obterCliente(string token)
        {
            Cliente cliente = null;
            string connectionString = Environment.GetEnvironmentVariable("BancoDeDados");

            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Cliente where Token Like @token";

                cmd.Parameters.Add("@token", SqlDbType.VarChar, 100).Value = token;

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if(dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cliente = new Cliente();
                        cliente.ID_Cliente = Convert.ToInt32(dr["ID_Cliente"]);
                        cliente.Nome = dr["Nome"].ToString();
                        cliente.Token = dr["Token"].ToString();
                    }
                }
            }

            return cliente;
        }

        private static List<Estabelecimento> obterEstabelecimentos()
        {
            var listaEstabelecimentos = new List<Estabelecimento>();
            string connectionString = Environment.GetEnvironmentVariable("BancoDeDados");

            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Estabelecimento";

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var estabelecimento = new Estabelecimento(); 
                        estabelecimento.ID_Estabelecimento = Convert.ToInt32(dr["ID_Estabelecimento"]);
                        estabelecimento.RazaoSocial = dr["RazaoSocial"].ToString();
                        estabelecimento.NomeFantasia = dr["NomeFantasia"].ToString();
                        estabelecimento.CNPJ = dr["CNPJ"].ToString();
                        estabelecimento.Descricao = dr["Descricao"].ToString();
                        estabelecimento.Imagem = dr["Imagem"].ToString();
                        listaEstabelecimentos.Add(estabelecimento);
                    }
                }
            }

            return listaEstabelecimentos;
        }
    }
}
