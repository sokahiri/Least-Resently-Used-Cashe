using System;

namespace LRUcashe.LinkedList
{
    class LinkedList<T> : Interfaces.IDeque<T>
    {
        private Node<T> _head;
        private Node<T> _tail;

        public int Size { get; private set; } = 0;

        public Node<T> Head
        {
            set
            {
                this._head = value;
            }
            get { return this._head; }
        }

        public Node<T> Tail
        {
            set
            {
                this._tail = value;
            }
            get { return this._tail; }
        }

        public LinkedList() { }
        public LinkedList(T element) 
        {
            Push(element);
        }
        public LinkedList(T[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                Push(array[i]);
            }
        }

        public bool IsEmpty()
        {
            return Size == 0;
        }

        public T PeekFront()
        {
            return IsEmpty() ? default : Head.Data;
        }

        public T PeekBack()
        {
            return IsEmpty() ? default : Tail.Data;
        }

        public T Poll()
        {
            if (IsEmpty()) 
            { 
                throw new ArgumentException("Poll:空のリスト先頭を削除しようとしています"); 
            }
            Node<T> removeNode = Head;
            Head = Head.Next;
            Size--;
            if (IsEmpty()) { Tail = null; }
            else { Head.Prev = null; }
            return removeNode.Data;
        }

        public T Pop()
        {
            if (IsEmpty()) 
            { 
                throw new ArgumentException("Pop:空のリスト末尾を削除しようとしています"); 
            }
            Node<T> removeNode = Tail;
            Tail = Tail.Prev;
            Size--;
            if (IsEmpty()) { Head = null; }
            else { Tail.Next = null; }
            return removeNode.Data;
        }

        public void Add(T element)
        {
            var newNode = new Node<T>(element);
            if (IsEmpty())
            {
                Head = newNode;
                Tail = newNode;
                Size++;
                return;
            }
            var nextNode = Head;
            nextNode.Prev = newNode;
            newNode.Next = nextNode;
            Head = newNode;
            Size++;
        }

        public void Push(T element)
        {
            var newNode = new Node<T>(element);
            if(IsEmpty())
            {
                Size++;
                Tail = newNode;
                Head = newNode;
                return;
            }
            var prevNode = Tail;
            prevNode.Next = newNode;
            newNode.Prev = prevNode;
            Tail = newNode;
            Size++;
        }

        public T[] CreateArray()
        {
            var array = new T[Size];
            Node<T> iterator = Head;
            int i = 0;
            while (iterator != null)
            {
                array[i] = iterator.Data;
                iterator = iterator.Next;
                i++;
            }
            return array;
        }

        //O(1)の時間計算量だが、危険性ありのメソッド
        //引数のnodeには、リストに含まれていることが保障されていないノードを渡してはいけない。
        public void DangerRemoveNode(Node<T> node)
        {
            //listのサイズが1なら、削除するだけ
            if (Size == 1)
            {
                Pop();
                return;
            }
            //nodeの隣がnull(=:Head or Tail)なら、隣にHead,Tailを移す
            if (node.Next == null) { Tail = node.Prev; }
            if (node.Prev == null) { Head = node.Next; }
            //ノードを削除する
            node.Remove();
            Size--;
        }

        public void RemoveNode(Node<T> node)
        {
            //nodeがLinkedList内に存在しないノードなら何もしない(O(Size)の時間計算量)
            if (!IsIncludedNode(node)) return;
            DangerRemoveNode(node);
        }

        public bool IsIncludedNode(Node<T> node)
        {
            Node<T> iterator = node;
            while (iterator != null)
            {
                if (ReferenceEquals(iterator, Tail)) return true;
                iterator = iterator.Next;
            }
            return false;
        }
    }
}
