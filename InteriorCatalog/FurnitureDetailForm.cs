using Model.Core;
using Model.Data;
using System;
using System.Windows.Forms;
namespace InteriorCatalog
{
    public partial class FurnitureDetailForm : Form
    {
        public FurnitureDetailForm(Furniture furniture)
        {
            InitializeComponent();
            if (furniture == null)
                throw new ArgumentNullException(nameof(furniture));

            Text = $"{furniture.Brand} {furniture.Model} - Детали";
            InitializeControls(furniture);
        }

        private void InitializeControls(Furniture furniture)
        {
            articleNumberLabel.Text = furniture.ArticleNumber;
            brandLabel.Text = furniture.Brand;
            modelLabel.Text = furniture.Model;
            typeLabel.Text = furniture.GetType().Name;
            priceLabel.Text = furniture.Price.ToString("C2");
            descriptionTextBox.Text = furniture.Description;
            fullInfoTextBox.Text = furniture.GetFullInfo();

            // Дополнительная информация в зависимости от типа мебели
            if (furniture is Chair chair)
            {
                extraInfoLabel.Text = $"Материал: {chair.Material}\nПодлокотники: {(chair.HasArmrests ? "Да" : "Нет")}";
            }
            else if (furniture is Table table)
            {
                extraInfoLabel.Text = $"Форма: {table.Shape}\nКоличество ножек: {table.NumberOfLegs}"; 
            }
            else if (furniture is Sofa sofa)
            {
                extraInfoLabel.Text = $"Количество мест: {sofa.NumberOfSeats}\nМодульный: {(sofa.IsModular ? "Да" : "Нет")}";
            }
            else if (furniture is Bed bed)
            {
                extraInfoLabel.Text = $"Размер: {bed.Size}\nС ящиками: {(bed.HasStorage ? "Да" : "Нет")}";
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}