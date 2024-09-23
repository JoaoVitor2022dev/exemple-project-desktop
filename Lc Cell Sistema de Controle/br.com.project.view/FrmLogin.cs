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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // botoa entrar
            string email = txtEmail.Text;
            string senha = txtPassword.Text;

            EmployeeDAO dao = new EmployeeDAO();

            if (dao.LoginMethod(email, senha) == true)
            {
                this.Hide();
            }
        }
    }
}
