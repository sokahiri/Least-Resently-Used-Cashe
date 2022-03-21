

namespace LRUcashe.LinkedList.Interfaces
{
    interface IQueue<T>
    {
        public T PeekFront(); // リストの先頭にある要素を返します。
        public T Poll(); // リストの先頭の要素を削除し、削除した要素を返します。 
        public void Push(T element); // リストの末尾に要素を追加します。
        public bool IsEmpty();
    }
}
