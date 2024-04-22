using System;
using System.Collections;
using System.Collections.Generic;

namespace KursovayaRabotaApp;
public class CustomLinkedList<T> : IEnumerable<T>
{
    // Головной узел списка
    private Node<T> head;
    // Количество элементов в списке
    private int count;

    // Свойство для получения количества элементов в списке
    public int Count { get { return count; } }

    // Метод для добавления элемента в список
    public void Add(T data)
    {
        Node<T> node = new Node<T> { Data = data };
        if (head == null) head = node;
        else
        {
            Node<T> current = head;
            while (current.Next != null) current = current.Next;
            current.Next = node;
        }
        count++;
    }
    // Метод для добавления элемента после определенного элемента в списке
    public void AddAfter(T item, T newItem)
    {
        Node<T> current = head;
        while (current != null)
        {
            if (current.Data.Equals(item))
            {
                Node<T> newNode = new Node<T> { Data = newItem, Next = current.Next };
                current.Next = newNode;
                count++;
                return;
            }
            current = current.Next;
        }
        throw new InvalidOperationException("Элемент не найден в списке");
    }

    // Метод для добавления элемента перед определенным элементом в списке
    public void AddBefore(T item, T newItem)
    {
        if (head == null) throw new InvalidOperationException("Список пуст");
        
        if (head.Data.Equals(item))
        {
            Node<T> newNode = new Node<T> { Data = newItem, Next = head };
            head = newNode;
            count++;
            return;
        }
        Node<T> current = head;
        while (current.Next != null)
        {
            if (current.Next.Data.Equals(item))
            {
                Node<T> newNode = new Node<T> { Data = newItem, Next = current.Next };
                current.Next = newNode;
                count++;
                return;
            }
            current = current.Next;
        }
        throw new InvalidOperationException("Элемент не найден в списке");
    }
    
    // Метод для удаления элемента из списка
    public void Remove(T data)
    {
        if (head == null)  return;
        
        if (head.Data.Equals(data))
        {
            head = head.Next;
            return;
        }
        Node<T> current = head;
        while (current.Next != null)
        {
            if (current.Next.Data.Equals(data))
            {
                current.Next = current.Next.Next;
                return;
            }
            current = current.Next;
        }
    }
    
    // Метод для получения итератора списка
    public IEnumerator<T> GetEnumerator()
    {
        Node<T> current = head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    // Реализация интерфейса IEnumerable
    IEnumerator IEnumerable.GetEnumerator() {return GetEnumerator();}
}


