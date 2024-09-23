using Lc_Cell_Sistema_de_Controle.br.com.project.model;
using Lc_Cell_Sistema_de_Controle.br.com.projet.connection;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lc_Cell_Sistema_de_Controle.br.com.project.dao
{
    internal class ProductDAO
    {
        private MySqlConnection conexao;
        public ProductDAO()
        {
            this.conexao = new ConnectionFactory().GetConnection();
        }
        #region Register Product 
        public void RegisterProduct(Product obj)
        {
            try
            {
                string sql = "INSERT INTO tb_produtos (descricao, preco, qtd_estoque, for_id ) " +
                             "VALUES (@descricao, @preco, @qtd , @for_id  );";

                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@descricao", obj.Description);
                executacmd.Parameters.AddWithValue("@preco", obj.Price);
                executacmd.Parameters.AddWithValue("@qtd", obj.StockQuantity);
                executacmd.Parameters.AddWithValue("@for_id", obj.for_id);

                conexao.Open();
                executacmd.ExecuteNonQuery();

                MessageBox.Show("Produto cadastrado com sucesso!");
                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Aconteceu um erro " + erro);
            }
        }
        #endregion

        #region list products
        public DataTable listProducts()
        {
            try
            {
                // 1 - passo é criar um datatable com sql 
                DataTable tabelaProduto = new DataTable();
                string sql = @"SELECT 
                            tb_produtos.id AS ""Código"", 
                            tb_produtos.descricao AS ""Descrição"",
                            tb_produtos.qtd_estoque AS ""Qtd Estoque"", 
                            tb_produtos.preco AS ""Preço"",
                            tb_fornecedores.nome AS ""Fornecedor""
                            FROM 
                            tb_produtos
                            JOIN 
                            tb_fornecedores ON tb_produtos.for_id = tb_fornecedores.id;";

                // 2 - organizar o comando sql no executar 
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                conexao.Open();
                executacmd.ExecuteNonQuery();

                // 3 - passo - criar MysqDataApter para preencher os dados no datatable
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(executacmd);
                dataAdapter.Fill(tabelaProduto);
                conexao.Close();

                return tabelaProduto;
            }
            catch (Exception error)
            {

                MessageBox.Show("Error ao executar o comando sql: " + error);
                return null;
            }
        }
        #endregion

        #region Edit Product
        public void EdictProduct(Product obj)
        {
            try
            {
                string sql = "UPDATE tb_produtos SET descricao = @descricao, preco = @preco, qtd_estoque = @qtd, for_id = @for_id WHERE id = @id";

                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@descricao", obj.Description);
                executacmd.Parameters.AddWithValue("@preco", obj.Price);
                executacmd.Parameters.AddWithValue("@qtd", obj.StockQuantity);
                executacmd.Parameters.AddWithValue("@for_id", obj.for_id);
                executacmd.Parameters.AddWithValue("@id", obj.Id);

                conexao.Open();
                executacmd.ExecuteNonQuery();

                MessageBox.Show("Produto Alterado com sucesso!");
                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Aconteceu um erro " + erro);
            }
        }
        #endregion

        #region Delete Product 
        public void DeleteProduct(Product obj)
        {
            try
            {
                string sql = "DELETE FROM tb_produtos WHERE id = @id";

                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@id", obj.Id);

                conexao.Open();
                executacmd.ExecuteNonQuery();

                MessageBox.Show("Produto Deletado com sucesso!");
                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Aconteceu um erro " + erro);
            }
        }
        #endregion

        #region Search Products By Name
        public DataTable SearchProductsByName(string name)
        {
            try
            {
                // 1 - passo é criar um datatable com sql 
                DataTable tabelaProduto = new DataTable();
                string sql = @"SELECT 
                            tb_produtos.id AS ""Código"", 
                            tb_produtos.descricao AS ""Descrição"",
                            tb_produtos.qtd_estoque AS ""Qtd Estoque"", 
                            tb_produtos.preco AS ""Preço"",
                            tb_fornecedores.nome AS ""Fornecedor""
                            FROM 
                            tb_produtos
                            JOIN 
                            tb_fornecedores ON tb_produtos.for_id = tb_fornecedores.id  
                            WHERE tb_produtos.descricao = @nome;";

                // 2 - organizar o comando sql no executar 
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@nome", name);
                conexao.Open();
                executacmd.ExecuteNonQuery();

                // 3 - passo - criar MysqDataApter para preencher os dados no datatable
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(executacmd);
                dataAdapter.Fill(tabelaProduto);
                conexao.Close();

                return tabelaProduto;
            }
            catch (Exception error)
            {

                MessageBox.Show("Error ao executar o comando sql: " + error);
                return null;
            }
        }
        #endregion

        #region List Products By Name
        public DataTable ListProductsByName(string name)
        {
            try
            {
                // 1 - Create a DataTable to hold the product data
                DataTable tabelaProduto = new DataTable();

                // SQL query to select products by name
                string sql = @"SELECT 
                          tb_produtos.id AS 'Código', 
                          tb_produtos.descricao AS 'Descrição',
                          tb_produtos.qtd_estoque AS 'Qtd Estoque', 
                          tb_produtos.preco AS 'Preço',
                          tb_fornecedores.nome AS 'Fornecedor'
                       FROM 
                          tb_produtos
                       JOIN 
                          tb_fornecedores ON tb_produtos.for_id = tb_fornecedores.id  
                       WHERE 
                          tb_produtos.descricao LIKE @nome;";

                // 2 - Organize the SQL command
                using (MySqlCommand executacmd = new MySqlCommand(sql, conexao))
                {
                    executacmd.Parameters.AddWithValue("@nome", "%" + name + "%");

                    // 3 - Create MySqlDataAdapter to fill the DataTable
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(executacmd))
                    {
                        // Open the connection
                        conexao.Open();

                        // Fill the DataTable with the data from the query
                        dataAdapter.Fill(tabelaProduto);

                        // Close the connection
                        conexao.Close();
                    }
                }

                return tabelaProduto;
            }
            catch (Exception error)
            {
                MessageBox.Show("Erro ao executar o comando SQL: " + error.Message);
                return null;
            }
        }

        #endregion

        #region returns product by id
        public Product ReturnsProductById(int id)
        {
            try
            {
                // 1 - comando sql 
                Product produto = new Product();

                string sql = @"SELECT * FROM tb_produtos WHERE id = @id";

                MySqlCommand executedcmd = new MySqlCommand(sql, conexao);
                executedcmd.Parameters.AddWithValue("@id", id);

                conexao.Open();

                MySqlDataReader reader = executedcmd.ExecuteReader();

                if (reader.Read())
                {
                    produto.Id = reader.GetInt32("id");
                    produto.Description = reader.GetString("descricao");
                    produto.Price = reader.GetDecimal("preco");

                    conexao.Close();

                    return produto;
                }
                else
                {
                    MessageBox.Show("Nenhum produto encontrado com esse codigo");
                    conexao.Close();
                    return null;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Aconteceu o erro: " + err);
                return null;
            }
        }
        #endregion

        #region Method of returning the current stock of products
        public int ReturningCurrentStockProducts(int idproduto)
        {
            try
            {
                string sql = @"SELECT qtd_estoque FROM tb_produtos WHERE id = @id";
                int qtd_estoque = 0;

                // organizar e arrumar o comando sql 
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@id", idproduto);

                conexao.Open();

                MySqlDataReader reader = executacmd.ExecuteReader();

                if (reader.Read())
                {
                    qtd_estoque = reader.GetInt32("qtd_estoque");
                    conexao.Close();
                }

                return qtd_estoque;
            }
            catch (Exception err)
            {
                MessageBox.Show($"Aconteceu um erro: {err}");
                return 0;
            }
        }
        #endregion

        #region Method to lower stock
        public void LowerStock(int idproduto, int qtdestoque)
        {
            try
            {
                // 1 - primeiro paso criar o comando sql 
                string sql = "UPDATE tb_produtos SET qtd_estoque = @qtd WHERE id = @id";

                // organizar e arrumar o comando sql 
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@qtd", qtdestoque);
                executacmd.Parameters.AddWithValue("@id", idproduto);

                // abrir a conexao e executar o comando 
                conexao.Open();
                executacmd.ExecuteNonQuery();

                conexao.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show($"Aconteceu um erro: {err}");
                conexao.Close();
            }
        }
        #endregion
    }
}
