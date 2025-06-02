using Model;
using Model.Core;
using Model.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
namespace InteriorCatalog
{
    public partial class MainForm : Form
    {
        private List<FurnitureCatalog> _catalogs = new List<FurnitureCatalog>();
        private string _dataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FurnitureCatalogApp");
        private string _currentFormat = "json";
        public MainForm()
        {
            InitializeComponent();
            InitializeFormatComboBox();
            LoadCatalogs();
            InitializeCatalogComboBox();
        }
        private void InitializeFormatComboBox()
        {
            var formatPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = SystemColors.Control,
                Padding = new Padding(5),
                Margin = new Padding(0, 0, 0, 10)
            };

            var formatLabel = new Label
            {
                Text = "Формат сохранения:",
                AutoSize = true,
                Margin = new Padding(5),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            var formatComboBox = new ComboBox
            {
                Width = 120,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            formatComboBox.Items.AddRange(new object[] { "JSON", "XML" });
            formatComboBox.SelectedIndex = 0; // JSON по умолчанию

            formatComboBox.SelectedIndexChanged += (sender, e) =>
            {
                string newFormat = formatComboBox.SelectedItem.ToString().ToLower();
                if (newFormat != _currentFormat)
                {
                    _currentFormat = newFormat;
                    ConvertAllCatalogsToNewFormat();
                }
            };

            formatPanel.Controls.Add(formatLabel);
            formatPanel.Controls.Add(formatComboBox);

            this.Controls.Add(formatPanel);
        }
        private void ConvertAllCatalogsToNewFormat()
        {
            try
            {
                foreach (var catalog in _catalogs)
                {
                    string catalogName = $"{catalog.Name}_{catalog.Season}_{catalog.Year}";
                    string oldPath = Path.Combine(_dataDirectory, $"{catalogName}.{(_currentFormat == "json" ? "xml" : "json")}");
                    string newPath = Path.Combine(_dataDirectory, $"{catalogName}.{_currentFormat}");

                    // Если существует файл в старом формате - конвертируем
                    if (File.Exists(oldPath))
                    {
                        if (_currentFormat == "json")
                        {
                            catalog.SaveToJson(newPath);
                        }
                        else
                        {
                            catalog.SaveToXml(newPath);
                        }
                    }
                    // Если файл только в новом формате - просто обновляем
                    else if (File.Exists(newPath))
                    {
                        if (_currentFormat == "json")
                        {
                            catalog.SaveToJson(newPath);
                        }
                        else
                        {
                            catalog.SaveToXml(newPath);
                        }
                    }
                }

                MessageBox.Show($"Все каталоги успешно конвертированы в {_currentFormat.ToUpper()} формат",
                    "Конвертация завершена", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при конвертации каталогов: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadCatalogs()
        {
            try
            {
                // Проверяем, есть ли данные при первом запуске
                if (!Directory.Exists(_dataDirectory))
                {
                    FurnitureDataGenerator.SaveInitialData(_dataDirectory);
                }

                // Загружаем каталоги
                var catalogFiles = Directory.GetFiles(_dataDirectory, $"catalog_*.{_currentFormat}");
                foreach (var file in catalogFiles)
                {
                    try
                    {
                        FurnitureCatalog catalog = _currentFormat == "json"
                            ? FurnitureCatalog.LoadFromJson(file)
                            : FurnitureCatalog.LoadFromXml(file);
                        _catalogs.Add(catalog);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки каталога из файла {file}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeCatalogComboBox()
        {
            catalogComboBox.Items.Clear();
            foreach (var catalog in _catalogs)
            {
                catalogComboBox.Items.Add($"{catalog.Name}");
            }

            if (catalogComboBox.Items.Count > 0)
            {
                catalogComboBox.SelectedIndex = 0;
            }
        }

        private void showCatalogButton_Click(object sender, EventArgs e)
        {
            if (catalogComboBox.SelectedIndex >= 0 && catalogComboBox.SelectedIndex < _catalogs.Count)
            {
                var selectedCatalog = _catalogs[catalogComboBox.SelectedIndex];
                var catalogForm = new CatalogForm(selectedCatalog);
                catalogForm.ShowDialog();
            }
        }

        private void catalogComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            showCatalogButton.Enabled = catalogComboBox.SelectedIndex >= 0;
        }
    }
}