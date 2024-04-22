using System;
using System.Globalization;
using System.Windows.Data;

namespace KursovayaRabotaApp;

// Объявление класса FunctionValueConverter, который принимает два типа параметров: T и TResult
public class FunctionValueConverter<T, TResult> : IValueConverter
{
    // Закрытое поле для хранения функции преобразования
    private readonly Func<T, TResult> function;

    // Конструктор класса, который принимает функцию преобразования в качестве параметра
    public FunctionValueConverter(Func<T, TResult> function)
    {
        this.function = function; // Сохраняем функцию в поле класса
    }

    // Метод Convert преобразует значение типа T в значение типа TResult
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return function((T)value); // Вызываем функцию преобразования с входным значением
    }

    // Метод ConvertBack не реализован в этом классе
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException(); // Генерируем исключение, если метод вызывается
    }
}

