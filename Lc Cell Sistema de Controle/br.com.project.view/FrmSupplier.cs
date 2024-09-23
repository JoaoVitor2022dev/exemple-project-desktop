using Lc_Cell_Sistema_de_Controle.br.com.project.dao;
using Lc_Cell_Sistema_de_Controle.br.com.project.model;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Lc_Cell_Sistema_de_Controle.br.com.project.view
{
    public partial class FrmSupplier : Form
    {
        public FrmSupplier()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Supplier supplier = new Supplier();

                supplier.Name = txtNameClient.Text;
                supplier.Cnpj = txtCnpj.Text;
                supplier.Email = txtEmailClient.Text;
                supplier.Telephone = txtTelephoneClient.Text;
                supplier.Phone = txtPhoneClient.Text;
                supplier.Cep = txtCep.Text;
                supplier.Address = txtAddress.Text;
                supplier.Number = int.Parse(txtNumberHome.Text);
                supplier.Complement = txtComplement.Text;
                supplier.Neighborhood = txtNeighborhood.Text;
                supplier.City = txtCity.Text;
                supplier.State = txtUf.Text;

                SupplierDAO dao = new SupplierDAO();

                dao.RegisterSupplier(supplier);

                SupplierTable.DataSource = dao.ListSupplier();

                TabSupplier.SelectedTab = tabPage2;

                new Helpers().LimparTela(this);
                txtUf.SelectedIndex = -1;
            }
            catch (Exception)
            {
                MessageBox.Show("Todos os campos tem que ser preenchidos.");
            }
        }

        private void btnCep_Click(object sender, EventArgs e)
        {
            try
            {
                string cep = txtCep.Text;

                string xml = "https://viacep.com.br/ws/" + cep + "/xml/";

                DataSet dados = new DataSet();

                dados.ReadXml(xml);

                txtAddress.Text = dados.Tables[0].Rows[0]["logradouro"].ToString();
                txtNeighborhood.Text = dados.Tables[0].Rows[0]["bairro"].ToString();
                txtCity.Text = dados.Tables[0].Rows[0]["localidade"].ToString();
                txtUf.Text = dados.Tables[0].Rows[0]["uf"].ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("Endereço não encontrado, por favor digite manualmente.");
            }
        }
        private void FrmSupplier_Load(object sender, EventArgs e)
        {
            SupplierTable.DefaultCellStyle.ForeColor = Color.Black;
            SupplierDAO dao = new SupplierDAO();
            SupplierTable.DataSource = dao.ListSupplier();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {

            try
            {
                // method of storing data in the model

                Supplier supplier = new Supplier();

                supplier.Code = int.Parse(txtCodeClient.Text);
                supplier.Name = txtNameClient.Text;
                supplier.Cnpj = txtCnpj.Text;
                supplier.Email = txtEmailClient.Text;
                supplier.Telephone = txtTelephoneClient.Text;
                supplier.Phone = txtPhoneClient.Text;
                supplier.Cep = txtCep.Text;
                supplier.Address = txtAddress.Text;
                supplier.Number = int.Parse(txtNumberHome.Text);
                supplier.Complement = txtComplement.Text;
                supplier.Neighborhood = txtNeighborhood.Text;
                supplier.City = txtCity.Text;
                supplier.State = txtUf.Text;

                //invoking the register method
                SupplierDAO dao = new SupplierDAO();
                dao.EditSupplier(supplier);

                // change page
                TabSupplier.SelectedTab = tabPage2;

                // to list table customer
                SupplierTable.DataSource = dao.ListSupplier();

                /// cleaning the inputs
                new Helpers().LimparTela(this);
                txtUf.SelectedIndex = -1;

            }
            catch (Exception)
            {
                MessageBox.Show("Todos os campos devem ser preenchidos");
            }
        }
        private void SupplierTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodeClient.Text = SupplierTable.CurrentRow.Cells[0].Value.ToString();
            txtNameClient.Text = SupplierTable.CurrentRow.Cells[1].Value.ToString();
            txtCnpj.Text = SupplierTable.CurrentRow.Cells[2].Value.ToString();
            txtEmailClient.Text = SupplierTable.CurrentRow.Cells[3].Value.ToString();
            txtTelephoneClient.Text = SupplierTable.CurrentRow.Cells[4].Value.ToString();
            txtPhoneClient.Text = SupplierTable.CurrentRow.Cells[5].Value.ToString();
            txtCep.Text = SupplierTable.CurrentRow.Cells[6].Value.ToString();
            txtAddress.Text = SupplierTable.CurrentRow.Cells[7].Value.ToString();
            txtNumberHome.Text = SupplierTable.CurrentRow.Cells[8].Value.ToString();
            txtComplement.Text = SupplierTable.CurrentRow.Cells[9].Value.ToString();
            txtNeighborhood.Text = SupplierTable.CurrentRow.Cells[10].Value.ToString();
            txtCity.Text = SupplierTable.CurrentRow.Cells[11].Value.ToString();
            txtUf.Text = SupplierTable.CurrentRow.Cells[12].Value.ToString();

            //Update the phone number on the consultation page with the values from the input fields

            TabSupplier.SelectedTab = tabPage1;
        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void BtnNew_Click(object sender, EventArgs e)
        {
            new Helpers().LimparTela(this);
            txtUf.SelectedIndex = -1;
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // deletar cliente 
                Supplier supplier = new Supplier();

                // pegar o codigo 
                supplier.Code = int.Parse(txtCodeClient.Text);

                SupplierDAO dao = new SupplierDAO();

                dao.DeleteSupplier(supplier);

                TabSupplier.SelectedTab = tabPage2;

                // atualizar os dados do banco de dados 
                SupplierTable.DataSource = dao.ListSupplier();

                new Helpers().LimparTela(this);
                txtUf.SelectedIndex = -1;
            }
            catch (Exception)
            {
                MessageBox.Show("Todos os campos devem ser preenchidos");
            }
        }
    }
}
