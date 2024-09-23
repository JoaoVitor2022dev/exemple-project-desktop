using Lc_Cell_Sistema_de_Controle.br.com.project.dao;
using Lc_Cell_Sistema_de_Controle.br.com.project.model;using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lc_Cell_Sistema_de_Controle.br.com.project.view
{
    public partial class FrmPayments : Form
    {
        Client client = new Client();
        DataTable shoppingCart = new DataTable();
        DateTime currentDate;
        public FrmPayments(Client cliente, DataTable shoppingCart, DateTime currentDate)
        {
            InitializeComponent();
            this.client = cliente;
            this.shoppingCart = shoppingCart;
            this.currentDate = currentDate;
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            // botão de finalizar 

            decimal sale_money, sale_cart, sale_change, totalPaymented, total;

            int stock_qty, purchased_qty, updated_stock;

            ProductDAO produtoADao = new ProductDAO();

            sale_money = decimal.Parse(txtMoney.Text);
            sale_cart = decimal.Parse(txtCard.Text);
            total = decimal.Parse(txtTotal.Text);

            // calcular o total pago 

            totalPaymented = sale_money + sale_cart; 

            if (totalPaymented < total)
            {
                MessageBox.Show("O total pago é menor que o valor total da venda");
            }
            else
            {
                sale_change = totalPaymented - total;

                Sale sale = new Sale();

                sale.client_id = client.Code;
                sale.Date_sales = currentDate;
                sale.Total_sales = total;
                sale.observation = txtObservation.Text;

                SaleDAO vdao = new SaleDAO();

                texchangeMoney.Text = sale_change.ToString();

                vdao.RegisterSales(sale);

                // cadastrar item de venda 
                foreach (DataRow linha in shoppingCart.Rows)
                {
                    ItemSale Item = new ItemSale();

                    Item.Sales_Id = vdao.returnsLastIdSale();
                    Item.Product_id = int.Parse(linha["Codigo"].ToString());
                    Item.Qtd = int.Parse(linha["Qtd"].ToString());
                    Item.Subtotal = decimal.Parse(linha["Subtotal"].ToString());

                    // baixa de estoque 
                    stock_qty = produtoADao.ReturningCurrentStockProducts(Item.Product_id);
                    purchased_qty = Item.Qtd;
                    updated_stock = stock_qty - purchased_qty;

                    produtoADao.LowerStock(Item.Product_id, updated_stock);

                    ItemSaleDAO itemDAO = new ItemSaleDAO();

                    itemDAO.RegisteringSalesItem(Item);

                    MessageBox.Show("Venda registrada com sucesso.");

                    this.Dispose();

                    // esse aqui é para intaciar  
                    new FrmSales().ShowDialog();
                }
            }
        }

        private void FrmPayments_Load(object sender, EventArgs e)
        {
            txtMoney.Text = "0,00";
            txtCard.Text = "0,00";
            texchangeMoney.Text = "0,00";
        }
    }
}
