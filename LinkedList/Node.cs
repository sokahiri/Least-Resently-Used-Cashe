

namespace LRUcashe.LinkedList
{
    class Node<T>
    {
        private T _data;
        private Node<T> _next;
        private Node<T> _prev;

        public T Data
        {
            set
            {
                _data = value;
            }
            get { return _data; }
        }

        public Node<T> Next
        {
            set
            {
                _next = value;
            }
            get { return _next; }
        }

        public Node<T> Prev
        {
            set
            {
                _prev = value;
            }
            get { return _prev; }
        }

        public Node() { _data = default; }
        public Node(T element) { _data = element; }

        public T Remove()
        {
            Node<T> next = Next;
            Node<T> prev = Prev;
            if (next != null) { 
                next.Prev = prev;
                Next = null;
            }
            if (prev != null) 
            {
                prev.Next = next;
                Prev = null;
            }
            return Data;
        }
    }
}
