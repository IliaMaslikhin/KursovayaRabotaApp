using System.Collections.Generic;
using System.Windows;

namespace KursovayaRabotaApp;

public partial class SelectBookWindow : Window
{
    public Book SelectedBook { get; private set; }
    public string Action { get; private set; }

    public SelectBookWindow(IEnumerable<Book> books)
    {
        InitializeComponent();

        // Заполнение ListBox книгами
        foreach (var book in books)
        {
            listBox.Items.Add(book);
        }

        // Обработчики событий для кнопок
        addButtonBefore.Click += (s, e) =>
        {
            SelectedBook = (Book)listBox.SelectedItem;
            Action = "before";
            DialogResult = true;
        };

        addButtonAfter.Click += (s, e) =>
        {
            SelectedBook = (Book)listBox.SelectedItem;
            Action = "after";
            DialogResult = true;
        };

        addButtonEnd.Click += (s, e) =>
        {
            Action = "end";
            DialogResult = true;
        };
    }
}
