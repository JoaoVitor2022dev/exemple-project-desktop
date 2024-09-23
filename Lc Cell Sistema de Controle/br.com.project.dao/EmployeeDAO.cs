using Lc_Cell_Sistema_de_Controle.br.com.projet.connection;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System;
using Lc_Cell_Sistema_de_Controle.br.com.project.model;
using System.Data;
using Lc_Cell_Sistema_de_Controle.br.com.project.view;

namespace Lc_Cell_Sistema_de_Controle.br.com.project.dao
{
    internal class EmployeeDAO
    {
        private MySqlConnection conexao;
        public EmployeeDAO()
        {
            this.conexao = new ConnectionFactory().GetConnection();
        }
        #region Register Employee
        public void RegisterEmployee(Employee obj)
        {
            try
            {
                // Set COMMAND SQL -insert into
                string sql = "INSERT INTO tb_funcionarios (nome, rg, cpf, email, senha, cargo, nivel_acesso, telefone, celular, cep, endereco, numero, complemento, bairro, cidade, estado) " +
                                 "VALUES (@nome, @rg, @cpf, @email, @senha, @cargo, @nivel, @telefone, @celular, @cep, @endereco, @numero, @complemento, @bairro, @cidade, @estado);\r\n";
                // Organize the sql command   
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@nome", obj.Name);
                executacmd.Parameters.AddWithValue("@rg", obj.Rg);
                executacmd.Parameters.AddWithValue("@cpf", obj.Cpf);
                executacmd.Parameters.AddWithValue("@email", obj.Email);
                executacmd.Parameters.AddWithValue("@senha", obj.Password);
                executacmd.Parameters.AddWithValue("@cargo", obj.Position);
                executacmd.Parameters.AddWithValue("@nivel", obj.AccessLevel);
                executacmd.Parameters.AddWithValue("@telefone", obj.Telephone);
                executacmd.Parameters.AddWithValue("@celular", obj.Phone);
                executacmd.Parameters.AddWithValue("@cep", obj.Cep);
                executacmd.Parameters.AddWithValue("@endereco", obj.Address);
                executacmd.Parameters.AddWithValue("@numero", obj.Number);
                executacmd.Parameters.AddWithValue("@complemento", obj.Complement);
                executacmd.Parameters.AddWithValue("@bairro", obj.Neighborhood);
                executacmd.Parameters.AddWithValue("@cidade", obj.City);
                executacmd.Parameters.AddWithValue("@estado", obj.State);

                // Execute the sql command
                conexao.Open();
                executacmd.ExecuteNonQuery();

                MessageBox.Show("Cliente cadastrado com sucesso!");
                conexao.Close();
            }
            catch (Exception erro)
            {

                MessageBox.Show("Ocorreu um error: " + erro);
            }
        }
        #endregion

        #region list employee
        public DataTable ListEmployee()
        {
            try
            {
                // 1 - passo é criar um datatable com sql 
                DataTable customerTable = new DataTable();
                string sql = "SELECT * FROM tb_funcionarios;";

                // 2 - organizar o comando sql no executar 
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                conexao.Open();
                executacmd.ExecuteNonQuery();

                // 3 - passo - criar MysqDataApter para preencher os dados no datatable
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(executacmd);
                dataAdapter.Fill(customerTable);

                return customerTable;
            }
            catch (Exception error)
            {

                MessageBox.Show("Error ao executar o comando sql: " + error);
                return null;
            }
        }
        #endregion

        #region Edit Employee
        public void EditEmployee(Employee obj)
        {
            try
            {
                string sql = @"UPDATE tb_funcionarios 
                             SET nome=@nome, rg=@rg, cpf=@cpf, email=@email, senha=@senha, 
                             cargo=@cargo, nivel_acesso=@nivel, telefone=@telefone, 
                             celular=@celular, cep=@cep, endereco=@endereco, numero=@numero, 
                             complemento=@complemento, bairro=@bairro, cidade=@cidade, estado=@estado 
                             WHERE id=@id";


                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@nome", obj.Name);
                executacmd.Parameters.AddWithValue("@rg", obj.Rg);
                executacmd.Parameters.AddWithValue("@cpf", obj.Cpf);
                executacmd.Parameters.AddWithValue("@email", obj.Email);
                executacmd.Parameters.AddWithValue("@senha", obj.Password);
                executacmd.Parameters.AddWithValue("@cargo", obj.Position);
                executacmd.Parameters.AddWithValue("@nivel", obj.AccessLevel);
                executacmd.Parameters.AddWithValue("@telefone", obj.Telephone);
                executacmd.Parameters.AddWithValue("@celular", obj.Phone);
                executacmd.Parameters.AddWithValue("@cep", obj.Cep);
                executacmd.Parameters.AddWithValue("@endereco", obj.Address);
                executacmd.Parameters.AddWithValue("@numero", obj.Number);
                executacmd.Parameters.AddWithValue("@complemento", obj.Complement);
                executacmd.Parameters.AddWithValue("@bairro", obj.Neighborhood);
                executacmd.Parameters.AddWithValue("@cidade", obj.City);
                executacmd.Parameters.AddWithValue("@estado", obj.State);
                executacmd.Parameters.AddWithValue("@id", obj.Code);

                conexao.Open();
                executacmd.ExecuteNonQuery();
                MessageBox.Show("Cliente Alterado com sucesso!");
                conexao.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show($"Ocorreu um error: {error}");
            }
        }
        #endregion

        #region Delete Employee
        public void DeleteEmployee(Employee obj)
        {
            try
            {
                string sql = "DELETE FROM tb_funcionarios WHERE id = @id";

                MySqlCommand executacmd = new MySqlCommand(sql, conexao);

                executacmd.Parameters.AddWithValue("@id", obj.Code);

                conexao.Open();
                executacmd.ExecuteNonQuery();

                MessageBox.Show("Cliente Deletado com sucesso com sucesso!");
                conexao.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show($"Ocorreu um error: {error}");
            }
        }
        #endregion

        #region Search Employee By Name
        public DataTable SearchEmployeeByName(string name)
        {
            try
            {
                // 1 - passo é criar um datatable com sql 
                DataTable tabelaCliente = new DataTable();
                string sql = "SELECT * FROM tb_funcionarios WHERE nome = @nome;";

                // 2 - organizar o comando sql no executar 
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@nome", name);


                conexao.Open();
                executacmd.ExecuteNonQuery();

                // 3 - passo - criar MysqDataApter para preencher os dados no datatable
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(executacmd);
                dataAdapter.Fill(tabelaCliente);
                conexao.Close();

                return tabelaCliente;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error ao executar o comando sql: " + error);
                return null;
            }
        }
        #endregion

        #region List Employee By Name
        public DataTable ListEmployeeByName(string name)
        {
            try
            {
                // 1 - passo é criar um datatable com sql 
                DataTable tabelaCliente = new DataTable();
                string sql = "SELECT * FROM tb_funcionarios WHERE nome LIKE @nome;";

                // 2 - organizar o comando sql no executar 
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@nome", name);


                conexao.Open();
                executacmd.ExecuteNonQuery();

                // 3 - passo - criar MysqDataApter para preencher os dados no datatable
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(executacmd);
                dataAdapter.Fill(tabelaCliente);
                conexao.Close();

                return tabelaCliente;
            }
            catch (Exception error)
            {

                MessageBox.Show("Error ao executar o comando sql: " + error);
                return null;
            }
        }
        #endregion

        #region Login method
        public bool LoginMethod(string email, string senha)
        {
            try
            {
                string sql = "SELECT * FROM tb_funcionarios WHERE email = @email AND senha = @senha";

                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@email", email);
                executacmd.Parameters.AddWithValue("@senha", senha);

                conexao.Open();

                MySqlDataReader reader = executacmd.ExecuteReader();

                if (reader.Read())
                {
                    string nivel = reader.GetString("nivel_acesso");
                    string nome = reader.GetString("nome");

                    MessageBox.Show($"Bem vindo {nivel}: {nome}");

                    FrmMenu TelaMenu = new FrmMenu();

                     TelaMenu.txtMenuUser.Text = nome;

                     if (nivel.Equals("Administrador"))
                     {
                         TelaMenu.Show();
                     }
                     else if (nivel.Equals("Usuário"))
                     {
                         // personalizar o que o vendedorvai ter acesso 
                         TelaMenu.MenuEmployee.Visible = false;
                         TelaMenu.Show();
                     }  
                    return true;
                }
                else
                {
                    MessageBox.Show("E-mail ou senha incorreto!");
                    return false;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show($"Ocorreu um erro: {err}");
                return false;
            }
        }
        #endregion
    }
}
