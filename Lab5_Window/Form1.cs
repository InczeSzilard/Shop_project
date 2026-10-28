using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Lab5_Window
{
    public partial class Form1 : Form
    {
        private string currentFilter = "";
        private List<Product> loadedProducts = new List<Product>();

        public Form1()
        {
            InitializeComponent();
            btnRefresh.Click -= btnRefresh_Click;
            btnRefresh.Click += btnRefresh_Click;

            sellProductsToolStripMenuItem.Click -= sellProductsToolStripMenuItem_Click;
            sellProductsToolStripMenuItem.Click += sellProductsToolStripMenuItem_Click;

            searchToolStripMenuItem.Click -= searchToolStripMenuItem_Click;
            searchToolStripMenuItem.Click += searchToolStripMenuItem_Click;

            dataGridView1.CellDoubleClick -= dataGridView1_CellDoubleClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;

            txtSearch.TextChanged -= txtSearch_TextChanged;
            txtSearch.TextChanged += txtSearch_TextChanged;

            existingProductToolStripMenuItem.Click -= existingProductToolStripMenuItem_Click;
            existingProductToolStripMenuItem.Click += existingProductToolStripMenuItem_Click;

            categoriesToolStripMenuItem.Click -= categoriesToolStripMenuItem_Click;
            categoriesToolStripMenuItem.Click += categoriesToolStripMenuItem_Click;

            salesHistoryToolStripMenuItem.Click -= salesHistoryToolStripMenuItem_Click;
            salesHistoryToolStripMenuItem.Click += salesHistoryToolStripMenuItem_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts("");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts(txtSearch.Text);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProducts(txtSearch.Text);
        }

        

        // Starts the database work on a thread pool thread
        public void LoadProducts(string filter)
        {
            currentFilter = filter;
            Task.Run(new Action(ReadProductsFromDatabase));
        }

        // Runs on the thread pool, not the UI thread
        private void ReadProductsFromDatabase()
        {
            try
            {
                StoreDbContext.CreateTables();

                using (StoreDbContext db = new StoreDbContext())
                {
                    string f = currentFilter;
                    var res = from p in db.Products
                              where f == "" || p.Name.Contains(f)
                              orderby p.Id
                              select p;
                    loadedProducts = res.ToList();

                    // find the category name for every product
                    List<Category> cats = (from c in db.Categories select c).ToList();

                    foreach (Product prod in loadedProducts)
                    {
                        var names = from cat in cats
                                    where cat.Id == prod.CategoryId
                                    select cat.Name;
                        prod.CategoryName = names.FirstOrDefault() ?? "";
                    }
                }

                // notify the GUI thread
                Invoke(new MethodInvoker(ShowProducts));
            }
            catch (Exception ex)
            {
                Exception inner = ex;
                while (inner.InnerException != null)
                    inner = inner.InnerException;

                MessageBox.Show(inner.GetType().Name + ":\n" + inner.Message);
            }
        }


// Runs on the UI thread
private void ShowProducts()
        {
            dataGridView1.DataSource = loadedProducts;

            if (dataGridView1.Columns["CategoryId"] != null)
                dataGridView1.Columns["CategoryId"].Visible = false;
        }
        
        private void newProductToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AddProduct form = new AddProduct();
            form.ShowDialog();
            LoadProducts(txtSearch.Text);
        }


        private void sellProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Select a product in the list first.");
                return;
            }

            Product p = (Product)dataGridView1.CurrentRow.DataBoundItem;
            SellProduct form = new SellProduct(p);
            form.ShowDialog();
            LoadProducts(txtSearch.Text);
        }
        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Product p = (Product)dataGridView1.Rows[e.RowIndex].DataBoundItem;
            SellProduct form = new SellProduct(p);
            form.ShowDialog();
            LoadProducts(txtSearch.Text);
        }
        private void existingProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddQuantity form = new AddQuantity();
            form.ShowDialog();
            LoadProducts(txtSearch.Text);
        }
        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CategoriesForm form = new CategoriesForm();
            form.ShowDialog();
            LoadProducts(txtSearch.Text);
        }
        private void salesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SallesHistory form = new SallesHistory();
            form.ShowDialog();
        }
    }
}