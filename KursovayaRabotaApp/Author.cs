using System;
using System.Linq;

namespace KursovayaRabotaApp; 

public class Author
{
    // Приватные поля для хранения фамилии автора, номера его мобильного телефона и списка его книг
    private string surname;
    private string mobileNumber;
    private CustomLinkedList<Book> books;

    // Конструктор класса, который принимает фамилию автора и номер его мобильного телефона
    public Author(string surname, string mobileNumber)
    {
        // Проверка входных данных на корректность
        if (string.IsNullOrEmpty(surname) || char.IsLower(surname[0]))
        {
            throw new ArgumentException("Фамилия автора должна начинаться с большой буквы.");
        }
        if (string.IsNullOrEmpty(mobileNumber) || !mobileNumber.StartsWith("+"))
        {
            throw new ArgumentException("Номер мобильного телефона должен начинаться с '+'.");
        }
        // Инициализация полей класса
        this.surname = surname;
        this.mobileNumber = mobileNumber;
        this.books = new CustomLinkedList<Book>();
    }
    
    // Методы для получения значений приватных полей класса
    public string GetSurname() { return surname; }
    public string GetMobileNumber() { return mobileNumber; }
    public CustomLinkedList<Book> GetBooks() { return books; }
    
    // Метод для добавления книги в список книг автора
    public void AddBook(Book book, Book existingBook = null, bool after = true)
    {
        if (existingBook == null)
        {
            books.Add(book);
        }
        else if (after)
        {
            books.AddAfter(existingBook, book);
        }
        else
        {
            books.AddBefore(existingBook, book);
        }
    }
    
    // Метод для удаления книги из списка книг автора
    public void RemoveBook(string bookTitle)
    {
        // Поиск книги в списке по названию
        Book bookToRemove = books.FirstOrDefault(b => b.GetTitle() == bookTitle);
        if (bookToRemove != null)
        {
            // Если книга найдена, то она удаляется из списка
            books.Remove(bookToRemove);
        }
        else
        {
            // Если книга не найдена, то выводится сообщение об ошибке
            Console.WriteLine("Книга с таким названием не найдена.");
        }
    }
    
    // Метод для расчета общего тиража всех книг автора
    public int CalculateTotalCirculationForAuthor()
    {
        int total = 0;
        foreach (Book book in books)
        {
            total += book.GetCirculation();
        }
        return total;
    }
    
    // Метод для получения количества книг автора
    public int GetBookCount()
    {
        return books.Count();
    }
}

