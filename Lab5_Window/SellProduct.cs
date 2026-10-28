using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5_Window
{
    public partial class SellProduct : Form
    {
        private int productId;
        private int amount;

        public SellProduct(Product p)
        {
            InitializeComponent();
            btnSell.Click -= btnSell_Click;
            btnSell.Click += btnSell_Click;

            productId = p.Id;
            lblInfo.Text = p.Name + " (available: " + p.Quantity + ")";
            numSell.Minimum = 1;
            numSell.Maximum = p.Quantity;
            numSell.Value = 1;
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            amount = (int)numSell.Value;
            btnSell.Enabled = false;
            Task.Run(new Action(SaveToDatabase));
        }

        private void SaveToDatabase()
        {
            using (StoreDbContext db = new StoreDbContext())
            {
                Product p = db.Products.Find(productId);
                if (p != null)
                {
                    // the category name is saved in the history too
                    string catName = "";
                    Category cat = db.Categories.Find(p.CategoryId);
                    if (cat != null)
                        catName = cat.Name;

                    SaleHistory h = new SaleHistory();
                    h.ProductId = p.Id;
                    h.ProductName = p.Name;
                    h.CategoryName = catName;
                    h.Quantity = amount;
                    h.SoldAt = DateTime.Now;
                    db.SaleHistories.Add(h);

                    p.Quantity = p.Quantity - amount;
                    if (p.Quantity <= 0)
                        db.Products.Remove(p);   // quantity 0: delete the product

                    db.SaveChanges();            // saves the sale and the new quantity together
                }
            }
            Invoke(new MethodInvoker(Close));
        }
    }
}