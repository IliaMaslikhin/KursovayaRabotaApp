using System;
using System.Collections.Generic;
using System.Linq;

namespace KursovayaRabotaApp;


public class PublishingHouse
{
    // Приватное поле для хранения списка авторов издательства
    private ShiftQueue<Author> authors;

    // Конструктор класса, который инициализирует список авторов
    public PublishingHouse()
    {
        this.authors = new ShiftQueue<Author>(100);
    }
    
    // Метод для получения списка авторов издательства
    public ShiftQueue<Author> GetAuthors() { return authors; }
    
    // Метод для добавления автора в издательство
    public void AddAuthor(Author author)
    {
        // Проверка на наличие автора в издательстве перед добавлением
        foreach (Author a in authors)
        { 
            if (a.GetSurname().Equals(author.GetSurname()) && a.GetMobileNumber().Equals(author.GetMobileNumber())) throw new InvalidOperationException("Этот автор уже существует в списке авторов издательства.");
        }

        // Добавление автора в издательство
        authors.Enqueue(author);
    }
    
    public Author DequeueAuthor()
    {
        if (authors.Count() == 0) throw new InvalidOperationException("Нет авторов для удаления.");
        return authors.Dequeue();
    }
    
    // Метод для расчета общего тиража всех книг в издательстве
    public int CalculateTotalCirculationForPublishingHouse()
    {
        int total = 0;
        foreach (Author author in authors)
        {
            total += author.CalculateTotalCirculationForAuthor();
        }
        return total;
    }
    
    // Метод для поиска авторов по фамилии
    public List<Author> FindAuthors(string surname)
    {
        List<Author> foundAuthors = new List<Author>();
        foreach (Author author in authors)
        {
            if (author.GetSurname().Equals(surname))
            {
                foundAuthors.Add(author);
            }
        }
        return foundAuthors;
    }
    
    // Метод для получения количества авторов в издательстве
    public int GetAuthorCount()
    {
        return authors.Count();
    }
}


