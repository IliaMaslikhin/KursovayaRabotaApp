using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace KursovayaRabotaApp;

public partial class RemoveBookWindow : Window
{
    // Это свойство представляет список книг, которые нужно удалить.
    // Оно объявлено как private set, что означает, что оно может быть установлено только внутри этого класса.
    public List<string> BooksToRemove { get; private set; }

    public RemoveBookWindow(List<string> bookTitles)
    {
        // Этот метод инициализирует компоненты окна.
        InitializeComponent();

        // Инициализация списка книг для удаления
        BooksToRemove = new List<string>();

        // Заполнение CheckBox'ов названиями книг
        foreach (var title in bookTitles)
        {
            // Создание нового CheckBox'а для каждой книги
            CheckBox checkBox = new CheckBox
            {
                Content = title
            };
            // Добавление CheckBox'а на панель
            booksPanel.Children.Add(checkBox);
        }

        // Это обработчик события для кнопки "Удалить".
        // Когда пользователь нажимает на кнопку "Удалить", все выбранные книги добавляются в список BooksToRemove.
        // Затем DialogResult устанавливается в true, что означает, что диалоговое окно было закрыто с положительным результатом.
        removeButton.Click += (s, e) =>
        {
            foreach (CheckBox checkBox in booksPanel.Children)
            {
                if (checkBox.IsChecked == true)
                {
                    BooksToRemove.Add(checkBox.Content.ToString());
                }
            }
            DialogResult = true;
        };

        // Это обработчик события для кнопки "Отмена".
        // Когда пользователь нажимает на кнопку "Отмена", DialogResult устанавливается в false, что означает, что диалоговое окно было закрыто с отрицательным результатом.
        cancelButton.Click += (s, e) =>
        {
            DialogResult = false;
        };
    }
}