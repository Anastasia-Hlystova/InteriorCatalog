using System.Linq;
using Model.Core;
using Model.Data;
using System;
using System.Windows.Forms;

namespace InteriorCatalog
{
    public partial class CatalogForm : Form
    {
        private FurnitureCatalog _catalog;
        private DataGridViewImageColumn _imageColumn;
        private bool _sortAscending = true;

        public CatalogForm(FurnitureCatalog catalog)
        {
            InitializeComponent(); //Вызывает автоматически сгенерированный метод, который инициализирует все компоненты формы (кнопки, таблицы, панели и т.д.), определенные в дизайнере форм.
            this.BackColor = Color.LightPink;
            _catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));
            Text = $"{_catalog.Name} - Каталог";
            InitializeFilterComboBox();
            InitializeSortButtons();
            InitializeDataGridView();
            this.MinimumSize = new Size(800, 600);
        }
        private void InitializeFilterComboBox()
        {
            var filterPanel = new FlowLayoutPanel
            {
                Name = "filterPanel",
                Dock = DockStyle.Top, //означает, что панель "приклеится" к верхнему краю формы
                Height = 40,
                BackColor = Color.LightPink,
                Padding = new Padding(5),
                Margin = new Padding(0, 0, 0, 5) //Создает промежуток между этой панелью и следующим элементом управления
            };

            var filterLabel = new Label
            {
                Text = "Фильтр по типу:",
                AutoSize = true,
                Margin = new Padding(5),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft //Выравнивает текст по центру по вертикали и слева по горизонтали
            };

            var typeComboBox = new ComboBox
            {
                Name = "typeFilterComboBox",
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White
            };

            // Добавляем варианты фильтрации
            typeComboBox.Items.Add("Все типы");
            typeComboBox.Items.AddRange(new object[] {
                nameof(Bed)
            });
            typeComboBox.Items.AddRange(new object[] {
                nameof(Chair)
            });
            typeComboBox.Items.AddRange(new object[] {
                nameof(Table)
            });
            typeComboBox.Items.AddRange(new object[] {
                nameof(Sofa)
            });
            typeComboBox.Items.AddRange(new object[] {
                nameof(Armchair)
            });
            typeComboBox.Items.AddRange(new object[] {
                nameof(Stool)
            });
            typeComboBox.SelectedIndex = 0; //// Устанавливает первый элемент ("Все типы") по умолчанию

            typeComboBox.SelectedIndexChanged += (sender, e) => 
            {
                ApplyFilterAndSort();
            };

            filterPanel.Controls.Add(filterLabel);
            filterPanel.Controls.Add(typeComboBox);

            this.Controls.Add(filterPanel);
        }
        private void ApplyFilterAndSort(string sortBy = null, bool? ascending = null)
        {
            var filterPanel = this.Controls.OfType<FlowLayoutPanel>()
        .FirstOrDefault(p => p.Controls.OfType<ComboBox>().Any());

            var typeComboBox = filterPanel?.Controls.OfType<ComboBox>().FirstOrDefault();
            if (typeComboBox == null) return;

            IEnumerable<Furniture> filteredItems = _catalog.Items;

            // Фильтрация по типу
            if (typeComboBox.SelectedIndex > 0)
            {
                string selectedType = typeComboBox.SelectedItem.ToString();
                filteredItems = selectedType switch
                {
                    "Chair" => filteredItems.Where(i => i is Chair), // Все стулья (Stool + Armchair)
                    "Stool" => filteredItems.Where(i => i is Stool), // Только табуреты
                    "Armchair" => filteredItems.Where(i => i is Armchair), // Только кресла
                    _ => filteredItems.Where(i => i.GetType().Name.Equals(selectedType, StringComparison.OrdinalIgnoreCase)) //Срабатывает для всех типов, кроме указанных выше
                };
            }

            // Сортировка
            if (!string.IsNullOrEmpty(sortBy))
            {
                filteredItems = SortItems(filteredItems, sortBy, ascending ?? _sortAscending);
            }

            // Обновление DataGridView
            furnitureDataGridView.DataSource = filteredItems
                .Select(i => new FurnitureDisplayItem(i))
                .ToList();
        }
        private IEnumerable<Furniture> SortItems(IEnumerable<Furniture> items, string sortBy, bool ascending)
        {
            switch (sortBy.ToLower())
            {
                case "article":
                    return ascending ?
                        items.OrderBy(i => i.ArticleNumber) :
                        items.OrderByDescending(i => i.ArticleNumber);
                case "name":
                    return ascending ?
                        items.OrderBy(i => i.Brand).ThenBy(i => i.Model) :
                        items.OrderByDescending(i => i.Brand).ThenByDescending(i => i.Model);
                case "price":
                    return ascending ?
                        items.OrderBy(i => i.Price) :
                        items.OrderByDescending(i => i.Price);
                default:
                    return items;
            }
        }
        private void InitializeSortButtons()
        {
            var sortPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.LightPink,
                Padding = new Padding(5),
                Margin = new Padding(0, 0, 0, 10)
            };

            // 2. Создаем кнопки с явным указанием Tag
            var btnSortArticle = new Button
            {
                Text = "Артикул ▲▼",
                Width = 120,
                Height = 30,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Margin = new Padding(5),
                Tag = "article", // Важно: задаем Tag для идентификации
                BackColor = Color.White
            };

            var btnSortName = new Button
            {
                Text = "Название ▲▼",
                Width = 120,
                Height = 30,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Margin = new Padding(5),
                Tag = "name",
                BackColor = Color.White
            };

            var btnSortPrice = new Button
            {
                Text = "Цена ▲▼",
                Width = 100,
                Height = 30,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Margin = new Padding(5),
                Tag = "price",
                BackColor = Color.White
            };
            var btnGroup = new Button
            {
                Text = "Сгруппировать",
                Width = 120,
                Height = 30,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Margin = new Padding(5),
                Tag = "group",
                BackColor = Color.White
            };
            // 3. Добавляем обработчики
            btnSortArticle.Click += SortButton_Click;
            btnSortName.Click += SortButton_Click;
            btnSortPrice.Click += SortButton_Click;
            btnGroup.Click += GroupButton_Click;

            // 4. Добавляем кнопки на панель
            sortPanel.Controls.AddRange(new Control[] { btnSortArticle, btnSortName, btnSortPrice, btnGroup });

            // 5. Добавляем панель в форму ПЕРЕД DataGridView
            this.Controls.Add(sortPanel);
            
        }
        private void SortButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is string sortBy)
            {
                var sortPanel = this.Controls[1] as FlowLayoutPanel;
                foreach (var control in sortPanel.Controls.OfType<Button>())
                {
                    if (control != button)
                    {
                        control.Text = control.Text.Replace("▲", "").Replace("▼", "").Replace("✓", "");
                    }
                }

                _sortAscending = !_sortAscending;
                ApplyFilterAndSort(sortBy, _sortAscending);

                // Обновляем текст кнопки с указанием направления
                button.Text = button.Text.Contains("▲") ?
                    button.Text.Replace("▲", "▼") :
                    button.Text.Replace("▼", "▲");
            }
        }
        private void GroupButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                // Сбрасываем стрелки на других кнопках
                var sortPanel = this.Controls[1] as FlowLayoutPanel;
                foreach (var control in sortPanel.Controls.OfType<Button>())
                {
                    if (control != button)
                    {
                        control.Text = control.Text.Replace("▲", "").Replace("▼", "");
                    }
                }

                // Применяем фильтр по типу (если выбран)
                var typeComboBox = (this.Controls[0] as FlowLayoutPanel)?.Controls.OfType<ComboBox>().FirstOrDefault();
                IEnumerable<Furniture> filteredItems = _catalog.Items;

                if (typeComboBox != null && typeComboBox.SelectedIndex > 0)
                {
                    string selectedType = typeComboBox.SelectedItem.ToString();
                    filteredItems = filteredItems.Where(i => i.GetType().Name == selectedType);
                }

                // Применяем PrioritySort
                var sortedList = filteredItems
            .OrderBy(i => i.Brand, StringComparer.OrdinalIgnoreCase)
            .ThenBy(i => i.Model, StringComparer.OrdinalIgnoreCase)
            .ThenBy(i => i.Description, StringComparer.OrdinalIgnoreCase)
            .ThenBy(i => i.ArticleNumber, StringComparer.OrdinalIgnoreCase)
            .ToList();

                // Обновляем DataGridView
                furnitureDataGridView.DataSource = sortedList
                    .Select(i => new FurnitureDisplayItem(i))
                    .ToList();

                // Помечаем кнопку как активную
                button.Text = "Сгруппировано ✓";
            }
        }
        private void SortCatalog(string sortBy)
        {
            IEnumerable<Furniture> sortedItems = _catalog.Items;

            switch (sortBy)
            {
                case "article":
                    sortedItems = _sortAscending
                        ? sortedItems.OrderBy(i => i.ArticleNumber)
                        : sortedItems.OrderByDescending(i => i.ArticleNumber);
                    break;
                case "name":
                    sortedItems = _sortAscending
                        ? sortedItems.OrderBy(i => i.Brand).ThenBy(i => i.Model)
                        : sortedItems.OrderByDescending(i => i.Brand).ThenByDescending(i => i.Model);
                    break;
                case "price":
                    sortedItems = _sortAscending
                        ? sortedItems.OrderBy(i => i.Price)
                        : sortedItems.OrderByDescending(i => i.Price);
                    break;
            }

            furnitureDataGridView.DataSource = sortedItems
                .Select(i => new FurnitureDisplayItem(i))
                .ToList();
        }
        private void InitializeDataGridView()
        {
            furnitureDataGridView.BackgroundColor = Color.LightPink;
            furnitureDataGridView.DefaultCellStyle.BackColor = Color.LightPink;
            furnitureDataGridView.DefaultCellStyle.SelectionBackColor = Color.HotPink; // Цвет выделения

            // Настройка заголовков
            furnitureDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightPink;
            furnitureDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.LightPink;

            // Альтернативные строки (если включена AlternatingRows)
            furnitureDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.Pink; // Чуть темнее для четных строк

            // Границы и сетка
            furnitureDataGridView.GridColor = Color.FromArgb(255, 200, 150); // Тонкая розовая сетка
            furnitureDataGridView.BorderStyle = BorderStyle.None; // Убираем стандартную рамку

            // Настройка DataGridView
            furnitureDataGridView.AutoGenerateColumns = false;
            furnitureDataGridView.AllowUserToAddRows = false;
            furnitureDataGridView.AllowUserToDeleteRows = false;
            furnitureDataGridView.ReadOnly = true;
            furnitureDataGridView.RowHeadersVisible = false;

            // Добавление колонок
            furnitureDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Артикул",
                DataPropertyName = "ArticleNumber",
                Width = 100
            });

            furnitureDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Бренд",
                DataPropertyName = "Brand",
                Width = 120
            });

            furnitureDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Модель",
                DataPropertyName = "Model",
                Width = 150
            });

            furnitureDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Тип",
                DataPropertyName = "TypeName",
                Width = 100
            });

            furnitureDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Цена",
                DataPropertyName = "Price",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            furnitureDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Описание",
                DataPropertyName = "Description",
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            var imageColumn = new DataGridViewImageColumn
            {
                HeaderText = "Изображение",
                Name = "Image",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    NullValue = Properties.Resources.ikonka // Ваша иконка из ресурсов
                }
            };
            furnitureDataGridView.Columns.Add(imageColumn);
            // Обновляем FurnitureDisplayItem
            furnitureDataGridView.DataSource = _catalog.Items.Select(i => new FurnitureDisplayItem(i)).ToList();
            // Привязка данных

            furnitureDataGridView.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0 && furnitureDataGridView.Columns[e.ColumnIndex].Name == "Image")
                {
                    var item = (FurnitureDisplayItem)furnitureDataGridView.Rows[e.RowIndex].DataBoundItem;
                    ShowImage(item.SourceFurniture);
                }
            };
        }
        private void CatalogForm_Resize(object sender, EventArgs e)
        {
            if (furnitureDataGridView != null && this.Controls.Count > 0)
            {
                var sortPanel = this.Controls[0] as FlowLayoutPanel;
                if (sortPanel != null)
                {
                    furnitureDataGridView.Height = this.ClientSize.Height - sortPanel.Height;
                }
            }
        }
        private void ShowImage(Furniture furniture)
        {
            var imageForm = new Form
            {
                Text = $"{furniture.Brand} {furniture.Model}",
                Size = new Size(500, 500),
                StartPosition = FormStartPosition.CenterParent
            };

            var pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = new FurnitureDisplayItem(furniture).Image
            };

            imageForm.Controls.Add(pictureBox);
            imageForm.ShowDialog();
        }
        private void furnitureDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedItem = (FurnitureDisplayItem)furnitureDataGridView.Rows[e.RowIndex].DataBoundItem;
                var detailForm = new FurnitureDetailForm(selectedItem.SourceFurniture);
                detailForm.ShowDialog();
            }
        }

        private void CatalogForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

    // Класс для отображения мебели в DataGridView
    public class FurnitureDisplayItem
    {
        public Furniture SourceFurniture { get; }
        public string ArticleNumber => SourceFurniture.ArticleNumber;
        public string Brand => SourceFurniture.Brand;
        public string Model => SourceFurniture.Model;
        public string TypeName => SourceFurniture.GetType().Name;
        public decimal Price => SourceFurniture.Price;
        public string Description => SourceFurniture.Description;
        public Image Image
        {
            get
            {
                try
                {
                    if (SourceFurniture is Stool)
                    {
                        switch (SourceFurniture.ArticleNumber)
                        {
                            case "CH_001": return Properties.Resources.CH_001;
                            case "CH_002": return Properties.Resources.CH_002;
                            case "CH_003": return Properties.Resources.CH_003;
                            default: return Properties.Resources.DefaultImage;
                        }
                    }
                    else if (SourceFurniture is Armchair)
                    {
                        switch (SourceFurniture.ArticleNumber)
                        {
                            case "CH_004": return Properties.Resources.CH_004;
                            case "CH_005": return Properties.Resources.CH_005;
                            default: return Properties.Resources.DefaultImage;
                        }
                    }
                    switch (SourceFurniture.ArticleNumber)
                    {
                        
                        case "SF_001":
                            return Properties.Resources.SF_001;
                        case "SF_002":
                            return Properties.Resources.SF_002;
                        case "SF_003":
                            return Properties.Resources.SF_003;
                        case "SF_004":
                            return Properties.Resources.SF_004;
                        case "SF_005":
                            return Properties.Resources.SF_005;
                        case "TB_001":
                            return Properties.Resources.TB_001;
                        case "TB_002":
                            return Properties.Resources.TB_002;
                        case "TB_003":
                            return Properties.Resources.TB_003;
                        case "TB_004":
                            return Properties.Resources.TB_004;
                        case "BD_001":
                            return Properties.Resources.BD_001;
                        case "BD_002":
                            return Properties.Resources.BD_002;
                        case "BD_003":
                            return Properties.Resources.BD_003;
                        case "BD_004":
                            return Properties.Resources.BD_004;
                        case "BD_005":
                            return Properties.Resources.BD_005;
                        default:
                            return Properties.Resources.DefaultImage;
                    }
                }
                catch
                {
                    return Properties.Resources.DefaultImage;
                }
            }
        }
        public FurnitureDisplayItem(Furniture furniture)
        {
            SourceFurniture = furniture ?? throw new ArgumentNullException(nameof(furniture));
        }

    }
}
