using System;
using System.Collections;
using System.Collections.Generic;
using Kursovayarabota;

namespace KursovayaRabotaApp;

// Класс ShiftQueue представляет собой очередь со сдвигом, реализованную на основе динамического массива
public class ShiftQueue<T> : IEnumerable<T>
{
    // Используется  DynamicArray для хранения элементов очереди
    private DynamicArray<T> items;

    // Конструктор принимает начальную емкость очереди
    public ShiftQueue(int initialCapacity)
    {
        items = new DynamicArray<T>(initialCapacity);
    }

    // Метод Enqueue добавляет элемент в конец очереди
    public void Enqueue(T item)
    {
        items.Add(item);
    }

    // Метод Dequeue удаляет и возвращает первый элемент очереди, сдвигая все последующие элементы влево
    public T Dequeue()
    {
        if (items.Count == 0)
        {
            throw new InvalidOperationException("Очередь пуста");
        }
        T item = items.Get(0);
        items.RemoveAt(0);
        return item;
    }

    // Методы GetEnumerator возвращают итератор для обхода элементов очереди
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < items.Count; i++)
        {
            yield return items.Get(i);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}


