using Lc_Cell_Sistema_de_Controle.br.com.project.model;
using Lc_Cell_Sistema_de_Controle.br.com.projet.connection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lc_Cell_Sistema_de_Controle.br.com.project.dao
{
    internal class ItemSaleDAO
    {
        private MySqlConnection conexao;
        public ItemSaleDAO()
        {
            this.conexao = new ConnectionFactory().GetConnection();
        }

        #region Method of registering sales item
        public void RegisteringSalesItem(ItemSale obj)
        {
            try
            {
                string sql = @"INSERT INTO tb_itensvendas (venda_id, produto_id, qtd,subtotal)
                               VALUES (@venda_id, @produto_id, @qtd, @subtotal)";

                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@venda_id", obj.Sales_Id);
                executacmd.Parameters.AddWithValue("@produto_id", obj.Product_id);
                executacmd.Parameters.AddWithValue("@qtd", obj.Qtd);
                executacmd.Parameters.AddWithValue("@subtotal", obj.Subtotal);

                // 3 - executar o comando sql 
                conexao.Open();
                executacmd.ExecuteNonQuery();

                conexao.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show($"Ocorreu um erro: {err}");
            }
        }
        #endregion

        #region Method to list all items for sale
        public DataTable ListAllItemsForSale(int id_venda)
        {
            try
            {
                //  criar um data table
                DataTable TabelaItens = new DataTable();


                // criar um comando SQL 
                string sql = @"SELECT i.id        AS 'Código',
                                      p.descricao AS 'Descrição', 
                                      i.qtd       AS 'Quantidade', 
                                      p.preco     AS 'Preço',
                                      i.subtotal  AS 'SubTotal'
                               FROM 
                                      tb_itensvendas AS i
                               JOIN 
                                      tb_produtos AS p
                                 ON 
                                      i.produto_id = p.id
                              WHERE 
                                      i.venda_id = @venda_id;";

                // execuatr o comando sql 
                MySqlCommand executecmdsql = new MySqlCommand(sql, conexao);
                executecmdsql.Parameters.AddWithValue("@venda_id", id_venda);


                conexao.Open();
                executecmdsql.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(executecmdsql);
                da.Fill(TabelaItens);

                return TabelaItens;

            }
            catch (Exception err)
            {
                MessageBox.Show($"Erro ao executar o comando SQL: {err}");
                return null;
            }
        }
        #endregion
    }
}
