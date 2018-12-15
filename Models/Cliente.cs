using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace EazySplitFunctions.Models
{
    public class Cliente
    {
        public int ID_Cliente { get; set; }
        public string Nome { get; set; }
        public string Token { get; set; }
    }
}
