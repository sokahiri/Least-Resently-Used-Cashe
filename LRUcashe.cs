
using System.Collections.Generic;
using LRUcashe.LinkedList;

namespace LRUcashe
{
    class LRUcashe<Tkey, Tvalue>
    {
        private Dictionary<Tkey, Node<DataSet<Tkey, Tvalue>>> dict;
        private LinkedList.LinkedList<DataSet<Tkey, Tvalue>> linkedList;
        private int capacity;

        public LRUcashe(int capacity)
        {
            this.capacity = capacity;
            dict = new Dictionary<Tkey, Node<DataSet<Tkey, Tvalue>>>(capacity * 3);
            linkedList = new LinkedList.LinkedList<DataSet<Tkey, Tvalue>>();
        }

        public Tvalue Get(Tkey key)
        {
            if (!dict.ContainsKey(key)) return default;
            Node<DataSet<Tkey, Tvalue>> targetNode = dict[key];
            MoveToHead(targetNode);
            return targetNode.Data.Value;
        }

        public void Put(Tkey key, Tvalue value)
        {
            //keyに対応する値が格納されているなら
            if (dict.ContainsKey(key))
            {
                //Nodeの値を書き替え
                Node<DataSet<Tkey, Tvalue>> targetNode = dict[key];
                targetNode.Data.Value = value;
                //連結リストの先頭へ
                MoveToHead(targetNode);
                //dictを更新
                dict[key] = linkedList.Head;
                return;
            }
            //格納されていないなら
            //新規ノードを先頭に
            linkedList.Add(new DataSet<Tkey, Tvalue>(key, value));
            //dictを更新
            dict[key] = linkedList.Head;
            //linkedListのサイズがcapacityを超えたら
            if (linkedList.Size > capacity)
            {
                //末尾ノードを削除
                Tkey deleteKey = linkedList.Pop().Key;
                //対象ノードの辞書を削除
                dict.Remove(deleteKey);
            }

        }

        public void Put(DataSet<Tkey,Tvalue>[] dataSets)
        {
            for(int i = 0; i < dataSets.Length; i++)
            {
                Put(dataSets[i].Key, dataSets[i].Value);
            }
        }

        private void MoveToHead(Node<DataSet<Tkey, Tvalue>> node)
        {
            linkedList.DangerRemoveNode(node);
            linkedList.Add(node.Data);
        }

        public DataSet<Tkey, Tvalue>[] CreateArray()
        {
            return linkedList.CreateArray();
        }
    }

    class DataSet<Tkey, Tvalue>
    {
        public Tkey Key { get; set; }
        public Tvalue Value { get; set; }

        public DataSet(Tkey key, Tvalue value)
        {
            Key = key;
            Value = value;
        }
    }
}
