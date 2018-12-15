using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasySplitFunctions.Models;
using Microsoft.WindowsAzure.Storage.Table;

namespace EazySplitFunctions.Models
{
    public class Retorno
    {
        public Cliente Cliente { get; set; }
        public List<Estabelecimento> Estabelecimentos { get; set; }
    }
}
