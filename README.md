# InteriorCatalog
 ***Чтобы приложение заработало, нужно самим подключить к рабочей области(решению) файл Logic.csproj***
 
**1.Главное окно (MainForm)**

***Выбор формата сохранения***

Выпадающий список с вариантами "JSON" и "XML".

*	Позволяет выбрать формат для сохранения данных (по умолчанию — JSON).

*	При изменении формата все каталоги автоматически конвертируются в выбранный формат.

*	После конвертации появляется сообщение о смене формата сохранения.
  

***Выбор каталога***

Выпадающий список с названиями каталогов ("Коллекция мебели для работы", "Коллекция мебели для отдыха", "Выбор нашего магазина").

*	Список заполняется автоматически при запуске программы.

*	При выборе каталога кнопка "Показать каталог" становится активной.

*	Если каталогов нет - список пуст, а кнопка "Показать каталог" неактивна.
  

***Кнопка "Показать каталог"***

*	Открывает окно со списком мебели выбранного каталога (CatalogForm).

**2. Окно со списком мебели из выбранного каталога (CatalogForm)**


***Кнопки сортировки***

* "Артикул ▲▼" — сортировка по артикулу (При нажатии на кнопку стрелка меняется: возрастание "▲▲" /убывание "▼▼").

*	"Название ▲▼" — сортировка по бренду и модели (При нажатии на кнопку стрелка меняется: возрастание "▲▲" /убывание "▼▼").

*	"Цена ▲▼" — сортировка по цене (При нажатии на кнопку стрелка меняется: возрастание "▲▲" /убывание "▼▼").

*	"Сгруппировать" — группировка сперва по бренду, потом по модели, потом по названию, потом по артикулу в алфавитном порядке. При нажатии, для обозначения того, что объекты уже сгруппированы, текст кнопки сменяется на "Сгруппировано ✓".


***Фильтр по типу мебели***

Выпадающий список с вариантами: "Все типы" (по умолчанию), "Chair", "Table", "Sofa", "Bed", "Stool", "Armchair". При выборе типа отображаются только соответствующие товары. 

 При выборе фильтра по типу "Chair" отображаются как обычные стулья/табуреты ("Stool"), так и кресла ("Armchair"). При выборе конкретного вида "Stool" или "Armchair" отображаются только соответствующие объекты.
 

***Таблица мебели***

*	Отображает список мебели с колонками: Артикул, Бренд, Модель, Тип, Цена, Описание, Изображение.

*	Двойной клик по строке открывает детальную информацию о мебели (FurnitureDetailForm).

*	Клик по специальной иконке ( ) в колонке "Изображение" открывает изображение предмета мебели в увеличенном виде.


**3. Окно с детальной информацией о товаре (FurnitureDetailForm)**
   
***Информация о мебели***

*	Отображается следующая базовая информация для каждого предмета мебели: артикул, бренд, модель, тип, цена, описание.

*	Дополнительная информация отображается в зависимости от типа мебели (например, для кровати - размер, наличие ящиков)


***Кнопка "Закрыть"***

Закрывает форму и возвращает к каталогу.

Обратная связь с пользователем в проекте:

При конвертации файлов показывается информационное сообщение ("Все каталоги успешно конвертированы в N формат. Конвертация завершена"). В случае ошибок выводятся соответствующие сообщения (например, "Ошибка при конвертации каталогов")








