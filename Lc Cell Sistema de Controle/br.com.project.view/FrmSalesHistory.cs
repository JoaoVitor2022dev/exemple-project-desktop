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
    public partial class FrmSalesHistory : Form
    {
        public FrmSalesHistory()
        {
            InitializeComponent();
        }
        private void FrmSalesHistory_Load(object sender, EventArgs e)
        {
            SaleDAO dao = new SaleDAO();

            SaleTable.DefaultCellStyle.ForeColor = Color.Black;
            SaleTable.DataSource = dao.ListSales();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime DateStart, DateEnd;

            DateStart = Convert.ToDateTime(txtDateStart.Value.ToString("yyyy-MM-dd"));
            DateEnd = Convert.ToDateTime(txtDateEnd.Value.ToString("yyyy-MM-dd"));

            SaleDAO dao = new SaleDAO();

            SaleTable.DataSource = dao.ListSalesPerPeriods(DateStart, DateEnd);
        }
        private void SaleTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // CRIAR UMA INSTACIA PARA O TELA

            // passar o id do venda 
            int id_venda = int.Parse(SaleTable.CurrentRow.Cells[0].Value.ToString());

            FrmDetailSale Tela = new FrmDetailSale(id_venda);

            DateTime DataSale = Convert.ToDateTime(SaleTable.CurrentRow.Cells[1].Value.ToString());

            Tela.textDate.Text = DataSale.ToString("dd/MM/yyyy");
            Tela.txtClient.Text = SaleTable.CurrentRow.Cells[2].Value.ToString();
            Tela.textTotalSale.Text = SaleTable.CurrentRow.Cells[3].Value.ToString();
            Tela.txtObservation.Text = SaleTable.CurrentRow.Cells[4].Value.ToString();

            Tela.ShowDialog();
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void SaleTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
