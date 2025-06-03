namespace InteriorCatalog
{
    partial class MainForm
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
            catalogComboBox = new ComboBox();
            showCatalogButton = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // catalogComboBox
            // 
            catalogComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            catalogComboBox.FormattingEnabled = true;
            catalogComboBox.Location = new Point(13, 73);
            catalogComboBox.Margin = new Padding(4, 5, 4, 5);
            catalogComboBox.Name = "catalogComboBox";
            catalogComboBox.Size = new Size(397, 28);
            catalogComboBox.TabIndex = 0;
            catalogComboBox.SelectedIndexChanged += catalogComboBox_SelectedIndexChanged;
            // 
            // showCatalogButton
            // 
            showCatalogButton.Enabled = false;
            showCatalogButton.Location = new Point(13, 122);
            showCatalogButton.Margin = new Padding(4, 5, 4, 5);
            showCatalogButton.Name = "showCatalogButton";
            showCatalogButton.Size = new Size(400, 35);
            showCatalogButton.TabIndex = 1;
            showCatalogButton.Text = "Показать каталог";
            showCatalogButton.UseVisualStyleBackColor = true;
            showCatalogButton.Click += showCatalogButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 48);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(191, 20);
            label1.TabIndex = 2;
            label1.Text = "Выберите каталог мебели";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(432, 171);
            Controls.Add(label1);
            Controls.Add(showCatalogButton);
            Controls.Add(catalogComboBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Мебельный каталог";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox catalogComboBox;
        private System.Windows.Forms.Button showCatalogButton;
        private System.Windows.Forms.Label label1;
    }
}