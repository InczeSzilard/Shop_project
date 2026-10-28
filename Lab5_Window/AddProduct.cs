using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5_Window
{
    public partial class AddProduct : Form
    {
        private string pName, pDescription;
        private DateTime pEntry, pExpiry;
        private int pQuantity;
        private int pCategoryId;
        private List<Category> categories = new List<Category>();

        public AddProduct()
        {
            InitializeComponent();

            Load -= AddProduct_Load;
            Load += AddProduct_Load;
        }

        // ---------- fill the category list ----------

        private void AddProduct_Load(object sender, EventArgs e)
        {
            Task.Run(new Action(ReadCategories));            // thread pool
        }

        private void ReadCategories()
        {
            StoreDbContext.CreateTables();

            using (StoreDbContext db = new StoreDbContext())
            {
                categories = (from c in db.Categories
                              orderby c.Name
                              select c).ToList();
            }

            Invoke(new MethodInvoker(ShowCategories));       // GUI thread
        }

        private void ShowCategories()
        {
            cmbCategory.DisplayMember = "Name";
            cmbCategory.ValueMember = "Id";
            cmbCategory.DataSource = categories;
        }

        // ---------- save ----------

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("The name can't be empty.");
                return;
            }

            if (cmbCategory.SelectedValue == null)
            {
                MessageBox.Show("Choose a category. If the list is empty, add one first (menu Categories).");
                return;
            }

            // read the controls on the UI thread
            pName = txtName.Text;
            pDescription = txtDescription.Text;
            pEntry = dtpEntry.Value;
            pExpiry = dtpExpiry.Value;
            pQuantity = (int)numQuantity.Value;
            pCategoryId = (int)cmbCategory.SelectedValue;

            btnSave.Enabled = false;                     // stops double clicks
            Task.Run(new Action(SaveToDatabase));        // thread pool
        }

        // Runs on the thread pool
        private void SaveToDatabase()
        {
            using (StoreDbContext db = new StoreDbContext())
            {
                Product p = new Product();
                p.Name = pName;
                p.Description = pDescription;
                p.EntryDate = pEntry;
                p.ExpiryDate = pExpiry;
                p.Quantity = pQuantity;
                p.CategoryId = pCategoryId;

                db.Products.Add(p);
                db.SaveChanges();
            }

            // notify the GUI thread
            Invoke(new MethodInvoker(Close));
        }
    }
}