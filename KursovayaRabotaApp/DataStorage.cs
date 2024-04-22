using System.IO;

namespace KursovayaRabotaApp;

public class DataStorage
{
    // Метод для сохранения данных издательства в файл
    public static void SaveToFile(PublishingHouse publishingHouse, string fileName)
    {
        // Создание потока записи в файл
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            // Перебор всех авторов в издательстве
            foreach (Author author in publishingHouse.GetAuthors())
            {
                // Запись информации об авторе и его книгах в файл
                writer.WriteLine(author.GetSurname());
                writer.WriteLine(author.GetMobileNumber());
                foreach (Book book in author.GetBooks())
                {
                    writer.WriteLine(book.GetTitle());
                    writer.WriteLine(book.GetCirculation());
                }
                writer.WriteLine("---"); // Разделитель для авторов
            }
        }
    }

    // Метод для загрузки данных издательства из файла
    public static PublishingHouse LoadFromFile(string fileName)
    {
        PublishingHouse publishingHouse = new PublishingHouse();
        // Создание потока чтения из файла
        using (StreamReader reader = new StreamReader(fileName))
        {
            string surname;
            // Чтение данных до конца файла
            while ((surname = reader.ReadLine()) != null)
            {
                string mobileNumber = reader.ReadLine();
                Author author = new Author(surname, mobileNumber);
                string bookTitle;
                // Чтение данных книги до разделителя "---"
                while ((bookTitle = reader.ReadLine()) != "---")
                {
                    int circulation = int.Parse(reader.ReadLine());
                    Book book = new Book(bookTitle, circulation);
                    author.AddBook(book);
                }
                publishingHouse.AddAuthor(author);
            }
        }
        return publishingHouse;
    }
}
