using System.Windows;

namespace KursovayaRabotaApp;

public partial class FindAuthorWindow : Window
{
    // Это свойство представляет фамилию автора.
    // Оно объявлено как private set, что означает, что оно может быть установлено только внутри этого класса.
    public string AuthorSurname { get; private set; }

    public FindAuthorWindow()
    {
        // Этот метод инициализирует компоненты окна.
        InitializeComponent();

        // Добавление обработчика событий для фильтрации ввода в текстовое поле фамилии
        surnameTextBox.PreviewTextInput += (s, e) =>
        {
            // Разрешить только буквы
            if (!char.IsLetter(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        };

        // Это обработчик события для кнопки "Найти".
        // Когда пользователь нажимает на кнопку "Найти", фамилия автора устанавливается в соответствующее значение из текстового поля.
        // Затем DialogResult устанавливается в true, что означает, что диалоговое окно было закрыто с положительным результатом.
        findButton.Click += (s, e) =>
        {
            AuthorSurname = surnameTextBox.Text;
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

