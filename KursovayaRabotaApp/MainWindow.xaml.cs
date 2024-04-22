using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using KursovayaRabotaApp;

namespace KursovayaRabotaApp
{
    public partial class MainWindow : Window
    {
        PublishingHouse publishingHouse = new PublishingHouse();

        public MainWindow()
        {
            InitializeComponent();

            // Инициализация издательства
            this.publishingHouse = new PublishingHouse();

            // Создание ObservableCollection из ShiftQueue
            ObservableCollection<Author> authors = new ObservableCollection<Author>(publishingHouse.GetAuthors());

            // Привязка ObservableCollection к DataGrid
            authorsDataGrid.ItemsSource = authors;

            // Добавление столбцов в authorsDataGrid
            authorsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Фамилия", Binding = new Binding { Converter = new FunctionValueConverter<Author, string>(a => a.GetSurname()) } });
            authorsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Номер телефона", Binding = new Binding { Converter = new FunctionValueConverter<Author, string>(a => a.GetMobileNumber()) } });

            // Добавление столбцов в booksDataGrid
            booksDataGrid.Columns.Add(new DataGridTextColumn { Header = "Название книги", Binding = new Binding { Converter = new FunctionValueConverter<Book, string>(b => b.GetTitle()) } });
            booksDataGrid.Columns.Add(new DataGridTextColumn { Header = "Тираж", Binding = new Binding { Converter = new FunctionValueConverter<Book, string>(b => b.GetCirculation().ToString()) } });


            // Привязка данных к authorsDataGrid
            authorsDataGrid.ItemsSource = new ObservableCollection<Author>(publishingHouse.GetAuthors());
            authorsDataGrid.IsReadOnly = true;
            booksDataGrid.IsReadOnly = true;

            // Обработчик события выбора автора в основной таблице
            authorsDataGrid.SelectionChanged += (s, e) =>
            {
                // Получение выбранного автора
                Author selectedAuthor = (Author)authorsDataGrid.SelectedItem;
    
                // Проверка, что автор был выбран
                if (selectedAuthor != null)
                {
                    // Отображение книг выбранного автора во вспомогательной таблице
                    booksDataGrid.ItemsSource = selectedAuthor.GetBooks();
                    // Сделать таблицу книг видимой
                    booksDataGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    // Если автор не выбран, очистить таблицу книг и сделать ее невидимой
                    booksDataGrid.ItemsSource = null;
                    booksDataGrid.Visibility = Visibility.Hidden;
                }
            };


            this.Loaded += (s, e) =>
            {
                Button addAuthorButton = (Button)FindName("addAuthorButton");
                // Добавление кнопки для добавления автора
                addAuthorButton.Click += (s, e) =>
                {
                    AddAuthorWindow addAuthorWindow = new AddAuthorWindow();
                    if (addAuthorWindow.ShowDialog() == true)
                    {
                        string surname = addAuthorWindow.AuthorSurname;
                        string mobileNumber = addAuthorWindow.AuthorMobileNumber;

                        // Проверка, что поля ввода не пустые
                        if (string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(mobileNumber))
                        {
                            MessageBox.Show("Пожалуйста, заполните все поля.");
                            return;
                        }

                        // Проверка, что номер телефона начинается с '+'
                        if (!mobileNumber.StartsWith("+"))
                        {
                            MessageBox.Show("Номер мобильного телефона должен начинаться с '+'");
                            return;
                        }

                        // Создание нового автора и добавление его в издательство
                        try
                        {
                            Author newAuthor = new Author(surname, mobileNumber);
                            publishingHouse.AddAuthor(newAuthor);

                            // Обновление DataGrid
                            authorsDataGrid.ItemsSource = null;
                            authorsDataGrid.ItemsSource = new ObservableCollection<Author>(publishingHouse.GetAuthors());

                            // Сообщение об успешном добавлении автора
                            MessageBox.Show("Автор добавлен.");
                        }
                        catch (ArgumentException ex)
                        {
                            // Если введены некорректные данные, выводится сообщение об ошибке
                            MessageBox.Show(ex.Message);
                        }
                    }
                };


                Button removeAuthorButton = (Button)FindName("removeAuthorButton");
                removeAuthorButton.Click += (s, e) =>
                {
                    try
                    {
                        // Удаление первого автора из очереди и получение его данных
                        Author authorToRemove = publishingHouse.DequeueAuthor();

                        // Удаление всех книг автора
                        while (authorToRemove.GetBookCount() > 0)
                        {
                            authorToRemove.RemoveBook(authorToRemove.GetBooks().First().GetTitle());
                        }

                        // Обновление DataGrid для авторов
                        authorsDataGrid.ItemsSource = null;
                        authorsDataGrid.ItemsSource = new ObservableCollection<Author>(publishingHouse.GetAuthors());

                        // Если после удаления автора в очереди остались другие авторы, отобразить книги первого автора
                        if (publishingHouse.GetAuthors().Any())
                        {
                            Author firstAuthor = publishingHouse.GetAuthors().First();
                            authorsDataGrid.SelectedItem = firstAuthor;
                            booksDataGrid.ItemsSource = firstAuthor.GetBooks();
                        }
                        else
                        {
                            // Если авторов в очереди больше нет, очистить таблицу книг
                            booksDataGrid.ItemsSource = null;
                            booksDataGrid.Visibility = Visibility.Hidden;
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Обработка исключения, если очередь авторов пуста
                        MessageBox.Show("Очередь авторов пуста. Нет авторов для удаления.");
                    }
                };


                
                // Добавление кнопки для расчета общего тиража
                Button calculateTotalCirculationButton = (Button)FindName("calculateTotalCirculationButton");
                calculateTotalCirculationButton.Click += (s, e) =>
                {
                    // Расчет общего тиража для издательства
                    int totalCirculation = publishingHouse.CalculateTotalCirculationForPublishingHouse();
                    // Вывод результата расчета
                    MessageBox.Show($"Общий тираж для издательства: {totalCirculation}");
                };


                // Добавление кнопки для поиска авторов
                Button findAuthorsButton = (Button)FindName("findAuthorsButton");
                findAuthorsButton.Click += (s, e) =>
                {
                    FindAuthorWindow findAuthorWindow = new FindAuthorWindow();
                    if (findAuthorWindow.ShowDialog() == true)
                    {
                        // Получение фамилии автора из нового окна
                        string searchSurname = findAuthorWindow.AuthorSurname;

                        // Поиск авторов с введенной фамилией
                        List<Author> foundAuthors = publishingHouse.FindAuthors(searchSurname);
                        if (foundAuthors.Count > 0)
                        {
                            // Если найдены авторы с введенной фамилией, то выводится их количество
                            string message = $"Найдено авторов: {foundAuthors.Count}\n\n";

                            // Для каждого найденного автора добавляем его фамилию и список его книг в сообщение
                            foreach (Author author in foundAuthors)
                            {
                                message += $"Автор: {author.GetSurname()}\n";
                                message += "Список книг:\n";
                                foreach (Book book in author.GetBooks())
                                {
                                    message += $"- {book.GetTitle()}\n";
                                }
                                message += "\n";
                            }

                            MessageBox.Show(message);
                        }
                        else
                        {
                            // Если авторы с введенной фамилией не найдены, то выводится сообщение об ошибке
                            MessageBox.Show("Авторы с такой фамилией не найдены.");
                        }
                    }
                };

                
    Button addBookButton = (Button)FindName("addBookButton");
addBookButton.Click += (s, e) =>
{
    FindAuthorWindow findAuthorWindow = new FindAuthorWindow();
    if (findAuthorWindow.ShowDialog() == true)
    {
        // Получение данных из текстовых полей
        string surname = findAuthorWindow.AuthorSurname;

        // Поиск автора с введенной фамилией
        Author authorToAddBook = publishingHouse.GetAuthors()
            .FirstOrDefault(a => a.GetSurname() == surname);
        if (authorToAddBook != null)
        {
            AddBookWindow addBookWindow = new AddBookWindow();
            if (addBookWindow.ShowDialog() == true)
            {
                string bookTitle = addBookWindow.BookTitle;
                int bookCirculation = addBookWindow.BookCirculation;

                try
                {
                    // Создание новой книги
                    Book bookToAdd = new Book(bookTitle, bookCirculation);

                    SelectBookWindow selectBookWindow = new SelectBookWindow(authorToAddBook.GetBooks());
                    if (selectBookWindow.ShowDialog() == true)
                    {
                        // Создание нового объекта Book для добавления
                        Book newBook = new Book(bookToAdd.GetTitle(), bookToAdd.GetCirculation());

                        if (selectBookWindow.Action == "before")
                        {
                            // Добавить книгу до выбранной
                            authorToAddBook.AddBook(newBook, selectBookWindow.SelectedBook, false);
                        }
                        else if (selectBookWindow.Action == "after")
                        {
                            // Добавить книгу после выбранной
                            authorToAddBook.AddBook(newBook, selectBookWindow.SelectedBook, true);
                        }
                        else
                        {
                            // Добавить книгу в конец списка
                            authorToAddBook.AddBook(newBook);
                        }

                        // Обновление DataGrid
                        booksDataGrid.ItemsSource = null;
                        booksDataGrid.ItemsSource = new ObservableCollection<Book>(authorToAddBook.GetBooks());

                        // Сообщение об успешном добавлении книги
                        MessageBox.Show("Книга добавлена автору.");
                    }
                }
                catch (ArgumentException ex)
                {
                    // Если тираж меньше нуля, выводится сообщение об ошибке
                    MessageBox.Show(ex.Message);
                }
            }
        }
        else
        {
            // Если автор не найден, то выводится сообщение об ошибке
            MessageBox.Show("Автор с такой фамилией не найден.");
        }
    }
};

    
                // Добавление кнопки для удаления книги
                Button removeBookButton = (Button)FindName("removeBookButton");
                removeBookButton.Click += (s, e) =>
                {
                    FindAuthorWindow findAuthorWindow = new FindAuthorWindow();
                    if (findAuthorWindow.ShowDialog() == true)
                    {
                        // Получение данных из текстовых полей
                        string surname = findAuthorWindow.AuthorSurname;

                        // Поиск автора с введенной фамилией
                        Author authorToRemoveBook = publishingHouse.GetAuthors()
                            .FirstOrDefault(a => a.GetSurname() == surname);
                        if (authorToRemoveBook != null)
                        {
                            // Если автор найден, то открытие окна удаления книги
                            RemoveBookWindow removeBookWindow = new RemoveBookWindow(authorToRemoveBook.GetBooks().Select(b => b.GetTitle()).ToList());
                            if (removeBookWindow.ShowDialog() == true)
                            {
                                foreach (string bookTitle in removeBookWindow.BooksToRemove)
                                {
                                    // Удаление каждой выбранной книги из списка книг автора
                                    authorToRemoveBook.RemoveBook(bookTitle);
                                }

                                // Обновление DataGrid
                                booksDataGrid.ItemsSource = null;
                                booksDataGrid.ItemsSource = new ObservableCollection<Book>(authorToRemoveBook.GetBooks());

                                // Сообщение об успешном удалении книг
                                MessageBox.Show("Книги удалены у автора.");
                            }
                        }
                        else
                        {
                            // Если автор не найден, то выводится сообщение об ошибке
                            MessageBox.Show("Автор с такой фамилией не найден.");
                        }
                    }
                };

                
                // Добавление кнопки для сохранения в файл
                Button saveToFileButton = (Button)FindName("saveToFileButton");
                saveToFileButton.Click += (s, e) =>
                {
                    // Создание и открытие диалогового окна для сохранения файла
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    Nullable<bool> result = dlg.ShowDialog();

                    // Если пользователь выбрал файл, сохраняем данные
                    if (result == true)
                    {
                        // Получение имени файла
                        string saveFileName = dlg.FileName;

                        // Вызов метода SaveToFile() класса DataStorage для сохранения данных издательства в файл
                        DataStorage.SaveToFile(publishingHouse, saveFileName);

                        // Сообщение об успешном сохранении данных
                        MessageBox.Show("Данные сохранены в файл.");
                    }
                };


                // Добавление кнопки для загрузки из файла
                Button loadFromFileButton = (Button)FindName("loadFromFileButton");
                loadFromFileButton.Click += (s, e) =>
                {
                    // Создание и открытие диалогового окна для загрузки файла
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                    Nullable<bool> result = dlg.ShowDialog();

                    // Если пользователь выбрал файл, загружаем данные
                    if (result == true)
                    {
                        // Получение имени файла
                        string loadFileName = dlg.FileName;

                        // Вызов метода LoadFromFile() класса DataStorage для загрузки данных из файла в издательство
                        publishingHouse = DataStorage.LoadFromFile(loadFileName);

                        // Сообщение об успешной загрузке данных
                        MessageBox.Show("Данные загружены из файла.");

                        // Обновление DataGrid
                        authorsDataGrid.ItemsSource = null;
                        authorsDataGrid.ItemsSource = publishingHouse.GetAuthors();
                    }
                };
            };
        }
    }
}

