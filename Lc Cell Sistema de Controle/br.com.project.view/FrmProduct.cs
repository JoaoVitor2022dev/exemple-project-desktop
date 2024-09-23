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
    public partial class FrmProduct : Form
    {
        public FrmProduct()
        {
            InitializeComponent();
        }
        private void FrmProduct_Load(object sender, EventArgs e)
        {
            SupplierDAO supplier = new SupplierDAO();

            cbSupplier.DataSource = supplier.ListSupplier();
            cbSupplier.DisplayMember = "nome";
            cbSupplier.ValueMember = "id";

            ProductDAO dao = new ProductDAO();

            ProductTable.DataSource = dao.listProducts();
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Product product = new Product();

                product.Description = txtDescription.Text;
                product.Price = decimal.Parse(txtPrice.Text);
                product.StockQuantity = int.Parse(txtStockQuantity.Text);
                product.for_id = int.Parse(cbSupplier.SelectedValue.ToString());

                ProductDAO dao = new ProductDAO();

                dao.RegisterProduct(product);

                new Helpers().LimparTela(this);

                ProductTable.DataSource = dao.listProducts();

                new Helpers().LimparTela(this);
                cbSupplier.SelectedIndex = -1;

                TabPorduct.SelectedTab = tabPage2;
            }
            catch (Exception)
            {
                MessageBox.Show("Todos os campos tem que ser preenchidos!"); 
            }
        }
        private void BtnNew_Click(object sender, EventArgs e)
        {
            new Helpers().LimparTela(this);
            cbSupplier.SelectedIndex = -1;
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Product product = new Product();

                product.Description = txtDescription.Text;
                product.Price = decimal.Parse(txtPrice.Text);
                product.StockQuantity = int.Parse(txtStockQuantity.Text);
                product.for_id = int.Parse(cbSupplier.SelectedValue.ToString());
                product.Id = int.Parse(txtCodeClient.Text);

                ProductDAO dao = new ProductDAO();

                dao.EdictProduct(product);

                new Helpers().LimparTela(this);

                ProductTable.DataSource = dao.listProducts();

                TabPorduct.SelectedTab = tabPage2;
            }
            catch (Exception)
            {
                MessageBox.Show("Verifique se todos os campos estão preenchidos.");
            }

        }
        private void ProductTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Pegando os dados de um intem selecionado 
            txtCodeClient.Text = ProductTable.CurrentRow.Cells[0].Value.ToString();
            txtDescription.Text = ProductTable.CurrentRow.Cells[1].Value.ToString();
            txtStockQuantity.Text = ProductTable.CurrentRow.Cells[2].Value.ToString();
            txtPrice.Text = ProductTable.CurrentRow.Cells[3].Value.ToString();
            cbSupplier.Text = ProductTable.CurrentRow.Cells[4].Value.ToString();

            // alterar para guia de dados pessoais 
            TabPorduct.SelectedTab = tabPage1;
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Product product = new Product();

            product.Id = int.Parse(txtCodeClient.Text);

            ProductDAO dao = new ProductDAO();

            dao.DeleteProduct(product);

            new Helpers().LimparTela(this);

            ProductTable.DataSource = dao.listProducts();

            TabPorduct.SelectedTab = tabPage2;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string nome = txtSearch.Text;

            ProductDAO dao = new ProductDAO();

            ProductTable.DataSource = dao.SearchProductsByName(nome);

            if (ProductTable.Rows.Count == 0)
            {
                MessageBox.Show("Nenhum Fornecedor encontrado.");
                ProductTable.DataSource = dao.listProducts();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string nome = "%" + txtSearch.Text + "%";

            ProductDAO dao = new ProductDAO();

            ProductTable.DataSource = dao.ListProductsByName(nome);
        }
    }
}
