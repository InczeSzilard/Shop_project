using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5_Window
{
    public partial class CategoriesForm : Form
    {
        private List<Category> loaded = new List<Category>();
        private string newName;
        private int deleteId;
        private string message = "";   // empty = the operation worked

        public CategoriesForm()
        {
            InitializeComponent();

            Load -= CategoriesForm_Load;
            Load += CategoriesForm_Load;

            btnAddCategory.Click -= btnAddCategory_Click;
            btnAddCategory.Click += btnAddCategory_Click;

            btnDeleteCategory.Click -= btnDeleteCategory_Click;
            btnDeleteCategory.Click += btnDeleteCategory_Click;
        }

        private void CategoriesForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }

        // ---------- show the list ----------

        private void LoadCategories()
        {
            Task.Run(new Action(ReadCategories));            // thread pool
        }

        private void ReadCategories()
        {
            StoreDbContext.CreateTables();

            using (StoreDbContext db = new StoreDbContext())
            {
                loaded = (from c in db.Categories
                          orderby c.Name
                          select c).ToList();
            }

            Invoke(new MethodInvoker(ShowCategories));       // GUI thread
        }

        private void ShowCategories()
        {
            dgvCategories.DataSource = loaded;
        }

        // ---------- add ----------

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            newName = txtCategory.Text.Trim();

            if (newName == "")
            {
                MessageBox.Show("Type a category name first.");
                return;
            }

            btnAddCategory.Enabled = false;
            Task.Run(new Action(AddCategoryToDatabase));
        }

        private void AddCategoryToDatabase()
        {
            message = "";

            using (StoreDbContext db = new StoreDbContext())
            {
                bool exists = (from c in db.Categories
                               where c.Name == newName
                               select c).Any();

                if (exists)
                {
                    message = "This category already exists.";
                }
                else
                {
                    Category c = new Category();
                    c.Name = newName;
                    db.Categories.Add(c);
                    db.SaveChanges();
                }
            }

            Invoke(new MethodInvoker(AfterChange));
        }

        // ---------- delete ----------

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (dgvCategories.CurrentRow == null)
            {
                MessageBox.Show("Select a category first.");
                return;
            }

            Category c = (Category)dgvCategories.CurrentRow.DataBoundItem;

            DialogResult answer = MessageBox.Show(
                "Delete the category \"" + c.Name + "\"?",
                "Delete category",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (answer != DialogResult.Yes)
                return;

            deleteId = c.Id;
            btnDeleteCategory.Enabled = false;
            Task.Run(new Action(DeleteCategoryFromDatabase));
        }

        private void DeleteCategoryFromDatabase()
        {
            message = "";

            using (StoreDbContext db = new StoreDbContext())
            {
                // a category that still has products can't be deleted
                bool inUse = (from p in db.Products
                              where p.CategoryId == deleteId
                              select p).Any();

                if (inUse)
                {
                    message = "This category has products. Move or sell them first.";
                }
                else
                {
                    Category c = db.Categories.Find(deleteId);
                    if (c != null)
                    {
                        db.Categories.Remove(c);
                        db.SaveChanges();
                    }
                }
            }

            Invoke(new MethodInvoker(AfterChange));
        }

        // ---------- runs on the GUI thread after add / delete ----------

        private void AfterChange()
        {
            btnAddCategory.Enabled = true;
            btnDeleteCategory.Enabled = true;

            if (message != "")
                MessageBox.Show(message);
            else
                txtCategory.Clear();

            LoadCategories();
        }
    }
}