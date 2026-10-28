namespace Lab5_Window
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.addProductsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.existingProductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sellProductsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.categoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addProductsToolStripMenuItem,
            this.sellProductsToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.categoriesToolStripMenuItem,
            this.salesHistoryToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(996, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // addProductsToolStripMenuItem
            // 
            this.addProductsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProductToolStripMenuItem,
            this.existingProductToolStripMenuItem});
            this.addProductsToolStripMenuItem.Name = "addProductsToolStripMenuItem";
            this.addProductsToolStripMenuItem.Size = new System.Drawing.Size(112, 26);
            this.addProductsToolStripMenuItem.Text = "Add Products";
            // 
            // newProductToolStripMenuItem
            // 
            this.newProductToolStripMenuItem.Name = "newProductToolStripMenuItem";
            this.newProductToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.newProductToolStripMenuItem.Text = "New Product";
            this.newProductToolStripMenuItem.Click += new System.EventHandler(this.newProductToolStripMenuItem_Click_1);
            // 
            // existingProductToolStripMenuItem
            // 
            this.existingProductToolStripMenuItem.Name = "existingProductToolStripMenuItem";
            this.existingProductToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.existingProductToolStripMenuItem.Text = "Existing Product";
            // 
            // sellProductsToolStripMenuItem
            // 
            this.sellProductsToolStripMenuItem.Name = "sellProductsToolStripMenuItem";
            this.sellProductsToolStripMenuItem.Size = new System.Drawing.Size(108, 26);
            this.sellProductsToolStripMenuItem.Text = "Sell Products";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(67, 26);
            this.searchToolStripMenuItem.Text = "Search";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(26, 112);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(937, 253);
            this.dataGridView1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(340, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(399, 42);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(444, 22);
            this.txtSearch.TabIndex = 3;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(873, 46);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(106, 40);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // categoriesToolStripMenuItem
            // 
            this.categoriesToolStripMenuItem.Name = "categoriesToolStripMenuItem";
            this.categoriesToolStripMenuItem.Size = new System.Drawing.Size(94, 26);
            this.categoriesToolStripMenuItem.Text = "Categories";
            // 
            // salesHistoryToolStripMenuItem
            // 
            this.salesHistoryToolStripMenuItem.Name = "salesHistoryToolStripMenuItem";
            this.salesHistoryToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.salesHistoryToolStripMenuItem.Text = "Sales History";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 435);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Store";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addProductsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sellProductsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ToolStripMenuItem newProductToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem existingProductToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem categoriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salesHistoryToolStripMenuItem;
    }
}

