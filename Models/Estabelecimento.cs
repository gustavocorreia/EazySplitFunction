using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySplitFunctions.Models
{
    public class Estabelecimento : TableEntity
    {
        public int ID_Estabelecimento { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
    }
}
