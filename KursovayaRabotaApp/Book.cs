using System;

namespace KursovayaRabotaApp;

public class Book
{
    // Приватные поля для хранения названия книги и ее тиража
    public string title;
    public int circulation;

    // Конструктор класса, который принимает название книги и ее тираж
    public Book(string title, int circulation)
    {
        // Проверка входных данных на корректность
        if (string.IsNullOrEmpty(title) || char.IsLower(title[0]))
        {
            throw new ArgumentException("Название книги должно начинаться с большой буквы.");
        }
        if (circulation < 0)
        {
            throw new ArgumentException("Тираж не может быть меньше нуля.");
        }
        // Инициализация полей класса
        this.title = title;
        this.circulation = circulation;
    }
    
    // Методы для получения и установки значений приватных полей класса
    public string GetTitle() { return title; }
    public void SetTitle(string title) { this.title = title; }

    public int GetCirculation() { return circulation; }
    public void SetCirculation(int circulation) { this.circulation = circulation; }
    public override string ToString() => $"{title}";
}


