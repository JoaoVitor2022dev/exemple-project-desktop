using Lc_Cell_Sistema_de_Controle.br.com.project.model;
using Lc_Cell_Sistema_de_Controle.br.com.projet.connection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lc_Cell_Sistema_de_Controle.br.com.project.dao
{
    internal class SupplierDAO
    {
        private MySqlConnection conexao;
        public SupplierDAO()
        {
            this.conexao = new ConnectionFactory().GetConnection();
        }

        #region Register Supplier
        public void RegisterSupplier(Supplier obj)
        {
            try
            {
                // 1 - Definir o CMD sql - insert into
                string sql = @"INSERT INTO tb_fornecedores (nome, cnpj, email, telefone, celular, cep, endereco, numero, complemento, bairro, cidade, estado) 
                  VALUES (@nome, @cnpj, @email, @telefone, @celular, @cep, @endereco, @numero, @complemento , @bairro, @cidade, @estado);";

                // 2 -  Organizar o comando sql     
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@nome", obj.Name);
                executacmd.Parameters.AddWithValue("@cnpj", obj.Cnpj);
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

                // 3 - executar o comando sql 
                conexao.Open();
                executacmd.ExecuteNonQuery();

                MessageBox.Show("Fornecedor cadastrado com sucesso!");
                conexao.Close();
            }
            catch (Exception erro)
            {

                MessageBox.Show("Ocorreu um error: " + erro);
            }
        }
        #endregion

        #region list Supplier
        public DataTable ListSupplier()
        {
            try
            {
                // 1 - passo é criar um datatable com sql 
                DataTable customerTable = new DataTable();
                string sql = "SELECT * FROM tb_fornecedores;";

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

        #region 
        public void EditSupplier(Supplier obj)
        {
            try
            {
                string sql = @"UPDATE tb_fornecedores 
                             SET nome=@nome, cnpj=@cnpj, email=@email,
                             telefone=@telefone, 
                             celular=@celular, cep=@cep, endereco=@endereco, numero=@numero, 
                             complemento=@complemento, bairro=@bairro, cidade=@cidade, estado=@estado 
                             WHERE id=@id";


                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@nome", obj.Name);
                executacmd.Parameters.AddWithValue("@cnpj", obj.Cnpj);
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

                conexao.Open();
                executacmd.ExecuteNonQuery();
                MessageBox.Show("Fornecedor Alterado com Sucesso!");
                conexao.Close();

            }
            catch (Exception error)
            {
                MessageBox.Show($"Ocorreu um error: {error}");
            }
        }
        #endregion

        #region delet
        public void DeleteSupplier(Supplier obj)
        {
            try
            {
                // 1 - Definir o CMD sql - insert into
                string sql = @"delete from tb_fornecedores where id = @id";

                // 2 -  Organizar o comando sql     
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@id", obj.Code);

                // 3 - executar o comando sql 
                conexao.Open();
                executacmd.ExecuteNonQuery();

                MessageBox.Show("Fornecedor Deletado com Sucesso!");
                conexao.Close();
            }
            catch (Exception erro)
            {

                MessageBox.Show("Ocorreu um error: " + erro);
            }
        }
        #endregion
    }
}
