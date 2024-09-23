using Lc_Cell_Sistema_de_Controle.br.com.project.dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lc_Cell_Sistema_de_Controle.br.com.project.view
{
    public partial class FrmDetailSale : Form
    {
        int id_venda;
        public FrmDetailSale(int id_venda)
        {
            this.id_venda = id_venda;
            InitializeComponent();
        }
        private void SaleTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1 Passo  - abrir um outro formulario 

        }

        private void FrmDetailSale_Load(object sender, EventArgs e)
        {
            // Carrega tela de detalhes 
            ItemSaleDAO dao = new ItemSaleDAO();

            ItemSaleTable.DataSource = dao.ListAllItemsForSale(id_venda);
        }
    }
}
