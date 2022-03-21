

namespace LRUcashe.LinkedList.Interfaces
{
    interface IStack<T>
    {
        public T PeekBack(); // リストの末尾にある要素を返します。
        public T Pop(); // リストの末尾の要素を削除し、削除した要素を返します
        public void Push(T element); // リストの末尾に要素を追加します。
        public bool IsEmpty();
    }
}
