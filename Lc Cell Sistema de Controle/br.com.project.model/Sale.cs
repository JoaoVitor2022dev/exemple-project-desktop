using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lc_Cell_Sistema_de_Controle.br.com.project.model
{
    internal class Sale
    {
        public int Id { get; set; }
        public int client_id { get; set; }
        public DateTime Date_sales { get; set; }
        public decimal Total_sales { get; set; }
        public string observation { get; set; }
    }
}
