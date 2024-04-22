using System.Windows;

namespace KursovayaRabotaApp
{

    public partial class AddBookWindow : Window
    {
        // Это свойства представляют название книги и тираж.
        // Они объявлены как private set, что означает, что они могут быть установлены только внутри этого класса.
        public string BookTitle { get; private set; }
        public int BookCirculation { get; private set; }

        public AddBookWindow()
        {
            // Этот метод инициализирует компоненты окна.
            InitializeComponent();

            // Добавление обработчика событий для фильтрации ввода в текстовое поле названия книги
            titleTextBox.PreviewTextInput += (s, e) =>
            {
                // Разрешить только буквы и пробелы
                if (!char.IsLetter(e.Text, e.Text.Length - 1) && !char.IsWhiteSpace(e.Text, e.Text.Length - 1))
                {
                    e.Handled = true;
                }
            };

            // Добавление обработчика событий для фильтрации ввода в текстовое поле тиража
            circulationTextBox.PreviewTextInput += (s, e) =>
            {
                // Разрешить только цифры
                if (!char.IsDigit(e.Text, e.Text.Length - 1))
                {
                    e.Handled = true;
                }
            };

            addButton.Click += (s, e) =>
            {
                // Когда пользователь нажимает на кнопку "Добавить", название книги и тираж устанавливаются в соответствующие значения из текстовых полей.
                // Если тираж не является числом, выводится сообщение об ошибке.
                // Если все в порядке, DialogResult устанавливается в true, что означает, что диалоговое окно было закрыто с положительным результатом.
                BookTitle = titleTextBox.Text;
                if (int.TryParse(circulationTextBox.Text, out int circulation))
                {
                    BookCirculation = circulation;
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Тираж должен быть числом.");
                }
            };

            // Это обработчик события для кнопки "Отмена".
            // Когда пользователь нажимает на кнопку "Отмена", DialogResult устанавливается в false, что означает, что диалоговое окно было закрыто с отрицательным результатом.
            cancelButton.Click += (s, e) => { DialogResult = false; };
        }
    }
}
