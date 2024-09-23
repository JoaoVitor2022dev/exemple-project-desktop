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
    internal class SaleDAO
    {
        private MySqlConnection conexao;
        public SaleDAO()
        {
            this.conexao = new ConnectionFactory().GetConnection();
        }

        #region Method of recording a sale
        public void RegisterSales(Sale obj)
        {
            try
            {
                string sql = @"INSERT INTO tb_vendas (cliente_id, data_venda, total_venda, observacoes)
                              VALUES (@cliente_id,@data_venda,@total_venda,@obs)";

                // 2 -  Organizar o comando sql     
                MySqlCommand executacmd = new MySqlCommand(sql, conexao);
                executacmd.Parameters.AddWithValue("@cliente_id", obj.client_id);
                executacmd.Parameters.AddWithValue("@data_venda", obj.Date_sales);
                executacmd.Parameters.AddWithValue("@total_venda", obj.Total_sales);
                executacmd.Parameters.AddWithValue("@obs", obj.observation);

                // 3 - executar o comando sql 
                conexao.Open();
                executacmd.ExecuteNonQuery();

                conexao.Close();

            }
            catch (Exception err)
            {
                MessageBox.Show("Aconteceu um erro: " + err);
            }
        }
        #endregion

        #region Method that returns the final sale ID
        public int returnsLastIdSale()
        {
            try
            {
                int idVenda = 0;

                string sql = @"SELECT MAX(id) id FROM tb_vendas";

                MySqlCommand executecmdsql = new MySqlCommand(sql, conexao);

                conexao.Open();

                MySqlDataReader reader = executecmdsql.ExecuteReader();

                if (reader.Read())
                {
                    idVenda = reader.GetInt32(0);
                }
                conexao.Close();

                return idVenda;

            }
            catch (Exception err)
            {
                MessageBox.Show("Ocorreu um erro: " + err);
                return 0;
            }
        }
        #endregion

        #region Method of Listing Sales
        public DataTable ListSales()
        {
            try
            {
                //  criar um data table
                DataTable HistoryTable = new DataTable();


                // criar um comando SQL 
                string sql = @" SELECT V.id as 'Código',
                                V.data_venda as 'Data da venda',
                                C.nome as 'Cliente',
                                V.total_venda as 'Total',
                                V.observacoes as 'Obs' 
                                FROM tb_vendas AS V JOIN tb_clientes AS C ON (V.cliente_id = C.id)
                                ";

                // execuatr o comando sql 
                MySqlCommand executecmdsql = new MySqlCommand(sql, conexao);

                conexao.Open();
                executecmdsql.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(executecmdsql);
                da.Fill(HistoryTable);

                return HistoryTable;

            }
            catch (Exception err)
            {
                MessageBox.Show($"Erro ao executar o comando SQL: {err}");
                return null;
            }
        }
        #endregion

        #region List Sales By Period Method
        public DataTable ListSalesPerPeriods(DateTime datainicio, DateTime datafim)
        {
            try
            {
                //  criar um data table
                DataTable TabelaHistorico = new DataTable();


                // criar um comando SQL 
                string sql = @" SELECT V.id as 'Código',
                                V.data_venda as 'Data da venda',
                                C.nome as 'Cliente',
                                V.total_venda as 'Total',
                                V.observacoes as 'Obs' 
                                FROM tb_vendas AS V JOIN tb_clientes AS C ON (V.cliente_id = C.id)

                                WHERE V.data_venda BETWEEN @datainicio AND @datafim;";

                // execuatr o comando sql 
                MySqlCommand executecmdsql = new MySqlCommand(sql, conexao);
                executecmdsql.Parameters.AddWithValue("@datainicio", datainicio);
                executecmdsql.Parameters.AddWithValue("@datafim", datafim);

                conexao.Open();
                executecmdsql.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(executecmdsql);
                da.Fill(TabelaHistorico);

                return TabelaHistorico;

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
