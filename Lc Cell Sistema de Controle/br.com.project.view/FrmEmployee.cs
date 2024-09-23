using Lc_Cell_Sistema_de_Controle.br.com.project.dao;
using Lc_Cell_Sistema_de_Controle.br.com.project.model;
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
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

            try
            {
                // method of storing data in the model
                Employee employee = new Employee();

                employee.Name = txtNameClient.Text;
                employee.Rg = txtRg.Text;
                employee.Cpf = txtCpf.Text;
                employee.Email = txtEmailClient.Text;
                employee.Password = txtPassword.Text;
                employee.Position = txtPosition.Text; 
                employee.AccessLevel = txtAccessLevel.Text;
                employee.Telephone = txtTelephoneClient.Text;
                employee.Phone = txtPhoneClient.Text;
                employee.Cep = txtCep.Text;
                employee.Address = txtAddress.Text;
                employee.Number = int.Parse(txtNumberHome.Text);
                employee.Complement = txtComplement.Text;
                employee.Neighborhood = txtNeighborhood.Text;
                employee.City = txtCity.Text;
                employee.State = txtUf.Text;

                //invoking the register method
                EmployeeDAO dao = new EmployeeDAO();
                dao.RegisterEmployee(employee);

                // trocar de pagina 
                TabEmployee.SelectedTab = tabPage2;

                // to list table customer
                EmployeeTable.DataSource = dao.ListEmployee();

                // Programmed method for clearing operating Inputs
                new Helpers().LimparTela(this);
                txtUf.SelectedIndex = -1;
                txtPosition.SelectedIndex = -1;
                txtAccessLevel.SelectedIndex = -1;
            }
            catch (Exception)
            {
                MessageBox.Show("Todos os campos devem ser preenchidos");
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

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            EmployeeTable.DefaultCellStyle.ForeColor = Color.Black;
            EmployeeDAO dao = new EmployeeDAO();
            EmployeeTable.DataSource = dao.ListEmployee();
        }

        private void EmployeeTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodeClient.Text = EmployeeTable.CurrentRow.Cells[0].Value.ToString();
            txtNameClient.Text = EmployeeTable.CurrentRow.Cells[1].Value.ToString();
            txtRg.Text = EmployeeTable.CurrentRow.Cells[2].Value.ToString();
            txtCpf.Text = EmployeeTable.CurrentRow.Cells[3].Value.ToString();
            txtEmailClient.Text = EmployeeTable.CurrentRow.Cells[4].Value.ToString();
            txtPassword.Text = EmployeeTable.CurrentRow.Cells[5].Value.ToString();
            txtPosition.Text = EmployeeTable.CurrentRow.Cells[6].Value.ToString();
            txtAccessLevel.Text = EmployeeTable.CurrentRow.Cells[7].Value.ToString();
            txtTelephoneClient.Text = EmployeeTable.CurrentRow.Cells[8].Value.ToString();
            txtPhoneClient.Text = EmployeeTable.CurrentRow.Cells[9].Value.ToString();
            txtCep.Text = EmployeeTable.CurrentRow.Cells[10].Value.ToString();
            txtAddress.Text = EmployeeTable.CurrentRow.Cells[11].Value.ToString();
            txtNumberHome.Text = EmployeeTable.CurrentRow.Cells[12].Value.ToString();
            txtComplement.Text = EmployeeTable.CurrentRow.Cells[13].Value.ToString();
            txtNeighborhood.Text = EmployeeTable.CurrentRow.Cells[14].Value.ToString();
            txtCity.Text = EmployeeTable.CurrentRow.Cells[15].Value.ToString();
            txtUf.Text = EmployeeTable.CurrentRow.Cells[16].Value.ToString();

            //Update the phone number on the consultation page with the values from the input fields
            TabEmployee.SelectedTab = tabPage1;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                // method of storing data in the model

                Employee employee = new Employee();

                employee.Code = int.Parse(txtCodeClient.Text);
                employee.Name = txtNameClient.Text;
                employee.Rg = txtRg.Text;
                employee.Cpf = txtCpf.Text;
                employee.Email = txtEmailClient.Text;
                employee.Password = txtPassword.Text; 
                employee.Position = txtPosition.Text;
                employee.AccessLevel = txtAccessLevel.Text;
                employee.Telephone = txtTelephoneClient.Text;
                employee.Phone = txtPhoneClient.Text;
                employee.Cep = txtCep.Text;
                employee.Address = txtAddress.Text;
                employee.Number = int.Parse(txtNumberHome.Text);
                employee.Complement = txtComplement.Text;
                employee.Neighborhood = txtNeighborhood.Text;
                employee.City = txtCity.Text;
                employee.State = txtUf.Text;

                //invoking the register method
                EmployeeDAO dao = new EmployeeDAO();
                dao.EditEmployee(employee);

                // change page
                TabEmployee.SelectedTab = tabPage2;

                // to list table customer
                EmployeeTable.DataSource = dao.ListEmployee();

                /// cleaning the inputs
                new Helpers().LimparTela(this);
                txtUf.SelectedIndex = -1;
                txtPosition.SelectedIndex = -1;
                txtAccessLevel.SelectedIndex = -1;

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
            txtPosition.SelectedIndex = -1;
            txtAccessLevel.SelectedIndex = -1;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // deletar cliente 

                Employee obj = new Employee();

                // pegar o codigo 
                obj.Code = int.Parse(txtCodeClient.Text);

                EmployeeDAO dao = new EmployeeDAO();

                dao.DeleteEmployee(obj);

                TabEmployee.SelectedTab = tabPage2;

                // atualizar os dados do banco de dados 
                EmployeeTable.DataSource = dao.ListEmployee();

                /// cleaning the inputs
                new Helpers().LimparTela(this);
                txtUf.SelectedIndex = -1;
                txtPosition.SelectedIndex = -1;
                txtAccessLevel.SelectedIndex = -1;
            }
            catch (Exception)
            {
                MessageBox.Show("Todos os campos devem ser preenchidos");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string name = txtSearch.Text;

            EmployeeDAO dao = new EmployeeDAO();

            EmployeeTable.DataSource = dao.SearchEmployeeByName(name);

            if (EmployeeTable.Rows.Count == 0)
            {
                EmployeeTable.DataSource = dao.ListEmployee();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            string name = "%" + txtSearch.Text + "%";

            EmployeeDAO dao = new EmployeeDAO();

            EmployeeTable.DataSource = dao.ListEmployeeByName(name);
        }
    }
}
