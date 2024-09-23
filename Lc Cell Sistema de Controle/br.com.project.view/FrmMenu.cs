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
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }
        private void FrmMenu_Load(object sender, EventArgs e)
        {
            // pegando a data atual 
            txtDateCurrent.Text = DateTime.Now.ToShortDateString();
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            //prgramando dentro do timer
            // pegando as horas 
            txtCurrentTime.Text = DateTime.Now.ToLongTimeString();
        }
        private void MenuCustomerRegister_Click(object sender, EventArgs e)
        {
            // Gerar a tela de Cadastrar cliente 
            FrmClients Tela = new FrmClients();
            Tela.ShowDialog();
        }
        private void MenuClientConsultation_Click(object sender, EventArgs e)
        {
            // Gerar a tela de Cadastrar cliente 
            FrmClients Tela = new FrmClients();
            Tela.TabClient.SelectedTab = Tela.tabPage2;

            Tela.ShowDialog();
        }
        private void MenuRegisterSupplier_Click(object sender, EventArgs e)
        {
            // Gerar a tela de Cadastrar cliente 
            FrmSupplier Tela = new FrmSupplier();
            Tela.ShowDialog();
        }

        private void MenuSupplierConsultation_Click(object sender, EventArgs e)
        {
            // Gerar a tela de Cadastrar Funcionario
            FrmSupplier Tela = new FrmSupplier();
            Tela.TabSupplier.SelectedTab = Tela.tabPage2;
            Tela.ShowDialog();
        }
        private void MenuRegisterEmployee_Click(object sender, EventArgs e)
        {
            // Gerar a tela de Cadastrar cliente 
            FrmEmployee Tela = new FrmEmployee();
            Tela.ShowDialog();
        }
        private void MenuEmployeeConsultation_Click(object sender, EventArgs e)
        {
            // Gerar a tela de Cadastrar Funcionario
            FrmEmployee Tela = new FrmEmployee();
            Tela.TabEmployee.SelectedTab = Tela.tabPage2;
            Tela.ShowDialog();
        }
        private void MenuRegisterProduct_Click(object sender, EventArgs e)
        {
            // Gerar a tela de Cadastrar cliente 
            FrmProduct Tela = new FrmProduct();
            Tela.ShowDialog();
        }

        private void MenuProductConsultation_Click(object sender, EventArgs e)
        {
            // Gerar a tela de Cadastrar Funcionario
            FrmProduct Tela = new FrmProduct();
            Tela.TabPorduct.SelectedTab = Tela.tabPage2;
            Tela.ShowDialog();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void registrarVendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Gerar a tela de Cadastrar cliente 
            FrmSales Tela = new FrmSales();
            Tela.ShowDialog();
        }
        private void MenuHistorySale_Click(object sender, EventArgs e)
        {
            // Gerar Fila de Cadastrar Fornecedor
            FrmSalesHistory Tela = new FrmSalesHistory();
            Tela.ShowDialog();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Você deseja sair?", "ATENÇÃO!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == DialogResult.Yes)
            {
                // Apertou para sair 
                Application.Exit();
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
           
        }

  
    }
}
