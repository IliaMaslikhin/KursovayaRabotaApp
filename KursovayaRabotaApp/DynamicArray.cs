using System;

namespace Kursovayarabota;


// Класс DynamicArray представляет собой динамический массив, который может автоматически увеличиваться по мере добавления элементов.
public class DynamicArray<T>
{
    // Массив для хранения элементов
    private T[] items;
    // Количество элементов в массиве
    private int count;

    // Конструктор принимает начальную емкость массива
    public DynamicArray(int initialCapacity)
    {
        items = new T[initialCapacity];
    }

    // Метод Add добавляет элемент в массив. Если массив заполнен, он увеличивает его размер в два раза.
    public void Add(T item)
    {
        if (count == items.Length)
        {
            // Увеличиваем размер массива в два раза
            T[] newItems = new T[items.Length * 2];
            Array.Copy(items, newItems, count);
            items = newItems;
        }
        items[count] = item;
        count++;
    }

    // Метод Get возвращает элемент по указанному индексу
    public T Get(int index)
    {
        if (index < 0 || index >= count)
        {
            throw new IndexOutOfRangeException();
        }
        return items[index];
    }

    // Метод RemoveAt удаляет элемент по указанному индексу, сдвигая все последующие элементы влево
    public void RemoveAt(int index)
    {
        if (index < 0 || index >= count)
        {
            throw new IndexOutOfRangeException();
        }
        for (int i = index; i < count - 1; i++)
        {
            items[i] = items[i + 1];
        }
        count--;
    }

    // Свойство Count возвращает количество элементов в массиве
    public int Count
    {
        get { return count; }
    }
}

