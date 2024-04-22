using System.Windows;
using KursovayaRabotaApp;

namespace KursovayaRabotaApp;

public partial class AddAuthorWindow : Window
{
    // Эти два свойства представляют фамилию автора и его мобильный номер.
    // Они объявлены как private set, что означает, что они могут быть установлены только внутри этого класса.
    public string AuthorSurname { get; private set; }
    public string AuthorMobileNumber { get; private set; }

    public AddAuthorWindow()
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

        // Добавление обработчика событий для фильтрации ввода в текстовое поле номера телефона
        mobileNumberTextBox.PreviewTextInput += (s, e) =>
        {
            // Разрешить только цифры и знак плюса
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != "+")
            {
                e.Handled = true;
            }
        };

        // Это обработчик события для кнопки "Добавить".
        // Когда пользователь нажимает на кнопку "Добавить", фамилия автора и мобильный номер устанавливаются в соответствующие значения из текстовых полей.
        // Затем DialogResult устанавливается в true, что означает, что диалоговое окно было закрыто с положительным результатом.
        addButton.Click += (s, e) =>
        {
            AuthorSurname = surnameTextBox.Text;
            AuthorMobileNumber = mobileNumberTextBox.Text;
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

