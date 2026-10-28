using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5_Window
{
    public partial class SallesHistory : Form
    {
        private List<SaleHistory> loaded = new List<SaleHistory>();

        public SallesHistory()
        {
            InitializeComponent();

            Load -= SallesHistory_Load;
            Load += SallesHistory_Load;
        }

        private void SallesHistory_Load(object sender, EventArgs e)
        {
            Task.Run(new Action(ReadSales));                 // thread pool
        }

        private void ReadSales()
        {
            try
            {
                StoreDbContext.CreateTables();

                using (StoreDbContext db = new StoreDbContext())
                {
                    loaded = (from s in db.SaleHistories
                              orderby s.Id descending
                              select s).ToList();
                }

                Invoke(new MethodInvoker(ShowSales));        // GUI thread
            }
            catch (Exception ex)
            {
                Exception inner = ex;
                while (inner.InnerException != null)
                    inner = inner.InnerException;

                MessageBox.Show(inner.GetType().Name + ":\n" + inner.Message);
            }
        }

        private void ShowSales()
        {
            dgvSales.DataSource = loaded;
        }

        
    }
}