namespace InteriorCatalog
{
    partial class CatalogForm : Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            furnitureDataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)furnitureDataGridView).BeginInit();
            SuspendLayout();
            // 
            // furnitureDataGridView
            // 
            furnitureDataGridView.AllowUserToAddRows = false;
            furnitureDataGridView.AllowUserToDeleteRows = false;
            furnitureDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            furnitureDataGridView.Dock = DockStyle.Fill;
            furnitureDataGridView.Location = new Point(0, 0);
            furnitureDataGridView.Margin = new Padding(4, 5, 4, 5);
            furnitureDataGridView.Name = "furnitureDataGridView";
            furnitureDataGridView.ReadOnly = true;
            furnitureDataGridView.RowHeadersWidth = 51;
            furnitureDataGridView.Size = new Size(1045, 709);
            furnitureDataGridView.TabIndex = 0;
            furnitureDataGridView.CellDoubleClick += furnitureDataGridView_CellDoubleClick;
            // 
            // CatalogForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1045, 709);
            Controls.Add(furnitureDataGridView);
            Margin = new Padding(4, 5, 4, 5);
            Name = "CatalogForm";
            StartPosition = FormStartPosition.CenterParent;
            Load += CatalogForm_Load;
            ((System.ComponentModel.ISupportInitialize)furnitureDataGridView).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView furnitureDataGridView;
    }
}