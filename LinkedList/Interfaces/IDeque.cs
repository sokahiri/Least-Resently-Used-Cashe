

namespace LRUcashe.LinkedList.Interfaces
{
    interface IDeque<T> : IQueue<T>, IStack<T>
    {
        public void Add(T element); //リストの先頭に要素を追加します。
        public new bool IsEmpty();
    }
}
