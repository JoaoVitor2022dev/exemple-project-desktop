using Lc_Cell_Sistema_de_Controle.br.com.projet.connection;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System;
using Lc_Cell_Sistema_de_Controle.br.com.project.model;
using System.Data;

namespace Lc_Cell_Sistema_de_Controle.br.com.project.dao
{
    public class ClientDAO
    {
        private MySqlConnection conexao;
        public ClientDAO()
        {
           this.conexao = new ConnectionFactory().GetConnection();
        }

        #region Register Customer
        public void RegisterCustomer(Client obj)
        {
            try
            {
                // Set COMMAND SQL -insert into
                string sql = @"INSERT INTO tb_clientes (nome, rg, cpf, email, telefone, celular, cep, endereco, numero, complemento, bairro, cidade, estado) 
                  VALUES (@nome, @rg, @cpf, @email, @telefone, @celular, @cep, @endereco, @numero, @complemento , @bairro, @cidade, @estado);";

                // Organize the sql command   
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@nome", obj.Name);
                executacmd.Parameters.AddWithValue("@rg", obj.Rg);
                executacmd.Parameters.AddWithValue("@cpf", obj.Cpf);
                executacmd.Parameters.AddWithValue("@email", obj.Email);
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

        #region list customers
        public DataTable ListCustomers()
        {
            try
            {
                // 1 - passo é criar um datatable com sql 
                DataTable customerTable = new DataTable();
                string sql = "SELECT * FROM tb_clientes;";

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

        #region Change customer 
        public void ChangeCustomer(Client obj)
        {
            try
            {
                // 1 - Definir o CMD sql - insert into
                string sql = @"UPDATE tb_clientes
                             SET nome=@nome, rg=@rg, cpf=@cpf, email=@email, telefone=@telefone, 
                             celular=@celular, cep=@cep, endereco=@endereco, numero=@numero, 
                             complemento=@complemento, bairro=@bairro, cidade=@cidade, estado=@estado 
                             WHERE id=@id";

                // 2 -  Organizar o comando sql     
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@nome", obj.Name);
                executacmd.Parameters.AddWithValue("@rg", obj.Rg);
                executacmd.Parameters.AddWithValue("@cpf", obj.Cpf);
                executacmd.Parameters.AddWithValue("@email", obj.Email);
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

                // 3 - executar o comando sql 
                conexao.Open();
                executacmd.ExecuteNonQuery();

                MessageBox.Show("Informações do cliente alteradas com Sucesso!");
                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Ocorreu um error: " + erro);
            }
        }
        #endregion

        #region deletecustomer
        public void DeleteCustomer(Client obj)
        {
            try
            {
                // 1 - Definir o CMD sql - insert into
                string sql = @"delete from tb_clientes where id = @id";

                // 2 -  Organizar o comando sql     
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@id", obj.Code);

                // 3 - executar o comando sql 
                conexao.Open();
                executacmd.ExecuteNonQuery();

                MessageBox.Show("Cliente Deletado com Sucesso!");
                conexao.Close();
            }
            catch (Exception erro)
            {

                MessageBox.Show("Ocorreu um error: " + erro);
            }
        }
        #endregion

        #region Search Customer By Name
        public DataTable SearchCustomerByName(string name)
        {
            try
            {
                // 1 - passo é criar um datatable com sql 
                DataTable tabelaCliente = new DataTable();
                string sql = "SELECT * FROM tb_clientes WHERE nome = @nome;";

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

        #region List Customer By Name
        public DataTable ListCustomerByName(string name)
        {
            try
            {
                // 1 - passo é criar um datatable com sql 
                DataTable tabelaCliente = new DataTable();
                string sql = "SELECT * FROM tb_clientes WHERE nome LIKE @nome;";

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

        #region Return Customers CPF
        public Client ReturnCustomersCPF(string cpf)
        {
            try
            {
                Client obj = new Client();

                // criar o comando sql 
                string sql = @"SELECT * FROM tb_clientes WHERE cpf = @cpf";

                MySqlCommand executedcmd = new MySqlCommand(sql, conexao);
                executedcmd.Parameters.AddWithValue("@cpf", cpf);

                conexao.Open();

                MySqlDataReader reader = executedcmd.ExecuteReader();

                if (reader.Read())
                {
                    obj.Code = reader.GetInt32("Id");
                    obj.Name = reader.GetString("nome");
                }
                else
                {
                    MessageBox.Show("Cliente não encontrado!");
                    conexao.Close();
                    return null;
                }
                conexao.Close();

                return obj;
            }
            catch (Exception error)
            {
                MessageBox.Show("Aconteceu um erro: " + error);
                conexao.Close();
                return null;
            }
        }
        #endregion
    }
}
