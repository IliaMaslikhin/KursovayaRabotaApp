namespace KursovayaRabotaApp
{
    public class Node<T>
    {
        // Свойство для хранения данных узла
        public T Data { get; set; }
        // Свойство для ссылки на следующий узел в списке
        public Node<T> Next { get; set; }
    }
}


