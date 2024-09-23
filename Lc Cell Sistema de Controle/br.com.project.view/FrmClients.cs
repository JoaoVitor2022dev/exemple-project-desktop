using Lc_Cell_Sistema_de_Controle.br.com.project.dao;
using Lc_Cell_Sistema_de_Controle.br.com.project.model;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Lc_Cell_Sistema_de_Controle.br.com.project.view
{
    public partial class FrmClients : Form
    {
        public FrmClients()
        {
            InitializeComponent();
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // method of storing data in the model
                Client client = new Client();

                client.Name = txtNameClient.Text;
                client.Rg = txtRg.Text;
                client.Cpf = txtCpf.Text;
                client.Email = txtEmailClient.Text;
                client.Telephone = txtTelephoneClient.Text;
                client.Phone = txtPhoneClient.Text;
                client.Cep = txtCep.Text;
                client.Address = txtAddress.Text;
                client.Number = int.Parse(txtNumberHome.Text);
                client.Complement = txtComplement.Text;
                client.Neighborhood = txtNeighborhood.Text;
                client.City = txtCity.Text;
                client.State = txtUf.Text;

                //invoking the register method
                ClientDAO dao = new ClientDAO();
                dao.RegisterCustomer(client);

                // trocar de pagina 
                TabClient.SelectedTab = tabPage2;

                // listar novamente os dados salvo no cadastro
                CustomerTable.DataSource = dao.ListCustomers();

                // Programmed method for clearing operating Inputs
                new Helpers().LimparTela(this);
                txtUf.SelectedIndex = -1;
            }
            catch (Exception)
            {
                MessageBox.Show("Todos os campos devem ser preenchidos");
            }
        }
        private void BtnNew_Click(object sender, EventArgs e)
        {
            /// cleaning the inputs
            new Helpers().LimparTela(this);
            txtUf.SelectedIndex = -1;
        }
        private void FrmClients_Load(object sender, EventArgs e)
        {
            CustomerTable.DefaultCellStyle.ForeColor = Color.Black;
            ClientDAO dao = new ClientDAO();
            CustomerTable.DataSource = dao.ListCustomers();
        }
        private void TabelaClient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodeClient.Text = CustomerTable.CurrentRow.Cells[0].Value.ToString();
            txtNameClient.Text = CustomerTable.CurrentRow.Cells[1].Value.ToString();
            txtRg.Text = CustomerTable.CurrentRow.Cells[2].Value.ToString();
            txtCpf.Text = CustomerTable.CurrentRow.Cells[3].Value.ToString();
            txtEmailClient.Text = CustomerTable.CurrentRow.Cells[4].Value.ToString();
            txtTelephoneClient.Text = CustomerTable.CurrentRow.Cells[5].Value.ToString();
            txtPhoneClient.Text = CustomerTable.CurrentRow.Cells[6].Value.ToString();
            txtCep.Text = CustomerTable.CurrentRow.Cells[7].Value.ToString();
            txtAddress.Text = CustomerTable.CurrentRow.Cells[8].Value.ToString();
            txtNumberHome.Text = CustomerTable.CurrentRow.Cells[9].Value.ToString();
            txtComplement.Text = CustomerTable.CurrentRow.Cells[10].Value.ToString();
            txtNeighborhood.Text = CustomerTable.CurrentRow.Cells[11].Value.ToString();
            txtCity.Text = CustomerTable.CurrentRow.Cells[12].Value.ToString();
            txtUf.Text = CustomerTable.CurrentRow.Cells[13].Value.ToString();

            //Update the phone number on the consultation page with the values from the input fields

            TabClient.SelectedTab = tabPage1;
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                // method of storing data in the model

                Client client = new Client();

                client.Code = int.Parse(txtCodeClient.Text); 
                client.Name = txtNameClient.Text;
                client.Rg = txtRg.Text;
                client.Cpf = txtCpf.Text;
                client.Email = txtEmailClient.Text;
                client.Telephone = txtTelephoneClient.Text;
                client.Phone = txtPhoneClient.Text;
                client.Cep = txtCep.Text;
                client.Address = txtAddress.Text;
                client.Number = int.Parse(txtNumberHome.Text);
                client.Complement = txtComplement.Text;
                client.Neighborhood = txtNeighborhood.Text;
                client.City = txtCity.Text;
                client.State = txtUf.Text;

                //invoking the register method
                ClientDAO dao = new ClientDAO();
                dao.ChangeCustomer(client);

                // change page
                TabClient.SelectedTab = tabPage2;

                // to list table customer
                CustomerTable.DataSource = dao.ListCustomers();

                /// cleaning the inputs
                new Helpers().LimparTela(this);
                txtUf.SelectedIndex = -1;

            }
            catch (Exception)
            {
                MessageBox.Show("Todos os campos devem ser preenchidos");
            }
        }

        // search button in the API the customer address
        private void button1_Click(object sender, EventArgs e)
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // deletar cliente 

                Client obj = new Client();

                // pegar o codigo 
                obj.Code = int.Parse(txtCodeClient.Text);

                ClientDAO dao = new ClientDAO();

                dao.DeleteCustomer(obj);

                TabClient.SelectedTab = tabPage2;

                // atualizar os dados do banco de dados 
                CustomerTable.DataSource = dao.ListCustomers();

                new Helpers().LimparTela(this);
                txtUf.SelectedIndex = -1;
            }
            catch (Exception)
            {
                MessageBox.Show("Todos os campos devem ser preenchidos");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string name = txtSearch.Text;

            ClientDAO dao = new ClientDAO();

            CustomerTable.DataSource = dao.SearchCustomerByName(name);

            if (CustomerTable.Rows.Count == 0)
            {
                CustomerTable.DataSource = dao.ListCustomers();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            string name = "%" + txtSearch.Text + "%";

            ClientDAO dao = new ClientDAO();

            CustomerTable.DataSource = dao.ListCustomerByName(name);
        }


        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CustomerTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
