using System;

namespace LRUcashe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var case1 = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            LinkedList.Test<int>.Run(case1, new int[] { 1, 2, 3, 4, 5, 6, 7 }, LinkedList.Test<int>.QueueTest, LinkedList.Test<int>.IsSameArray);
            LinkedList.Test<int>.Run(case1, new int[] { 7, 6, 5, 4, 3, 2, 1 }, LinkedList.Test<int>.StackTest, LinkedList.Test<int>.IsSameArray);
            LinkedList.Test<int>.Run(case1, new int[] { 1, 7, 2, 6, 3, 5, 4 }, LinkedList.Test<int>.DequeTest, LinkedList.Test<int>.IsSameArray);
            LinkedList.Test<int>.Run(case1, new int[] { 1, 1, 1, 1, 1, 1, 1 }, LinkedList.Test<int>.QueueTest, LinkedList.Test<int>.IsSameArray);
            
            var cashe = new LRUcashe<int, string>(5);
            var case2 = new DataSet<int, string>[] 
            {
                new DataSet<int, string>(1, "one"),
                new DataSet<int, string>(2, "two"),
                new DataSet<int, string>(3, "three"),
                new DataSet<int, string>(4,"four")
            };

            Test<int, string>.Run(
                "シンプルなputテスト",
                case2,
                new DataSet<int, string>[]
                {
                    new DataSet<int, string>(4,"four"),
                    new DataSet<int, string>(3, "three"),
                    new DataSet<int, string>(2, "two"),
                    new DataSet<int, string>(1, "one"),
                },
                (DataSet<int, string>[] case2) =>
                    {
                        var cashe = new LRUcashe<int, string>(5);
                        cashe.Put(case2);
                        return cashe.CreateArray();
                    }
                );
            Test<int, string>.Run(
                "Capacityを超えてputしたとき",
                case2,
                new DataSet<int, string>[]
                {
                    new DataSet<int, string>(4,"four"),
                    new DataSet<int, string>(3, "three"),
                },
                (DataSet<int, string>[] case2) =>
                {
                    var cashe = new LRUcashe<int, string>(2);
                    cashe.Put(case2);
                    return cashe.CreateArray();
                }
                );
            Test<int, string>.Run(
                "シンプルなgetテスト",
                case2,
                new DataSet<int, string>[]
                {
                    new DataSet<int, string>(3, "three"),
                    new DataSet<int, string>(2, "two"),
                    new DataSet<int, string>(4,"four"),
                    new DataSet<int, string>(1, "one"),
                },
                (DataSet<int, string>[] case2) =>
                {
                    var cashe = new LRUcashe<int, string>(5);
                    cashe.Put(case2);
                    Console.WriteLine("\ntwo="+cashe.Get(2));
                    Console.WriteLine("three=" + cashe.Get(3));
                    return cashe.CreateArray();
                }
            );
            Test<int, string>.Run(
                "値の書き替えテスト",
                case2,
                new DataSet<int, string>[]
                {
                    new DataSet<int, string>(1, "いち"),
                    new DataSet<int, string>(4,"four"),
                    new DataSet<int, string>(3, "three"),
                    new DataSet<int, string>(2, "two"),
                },
                (DataSet<int, string>[] case2) =>
                {
                    var cashe = new LRUcashe<int, string>(5);
                    cashe.Put(case2);
                    cashe.Put(1, "いち");
                    return cashe.CreateArray();
                }
            );
            Test<int, string>.Run(
                "値の書き替えテスト*2",
                case2,
                new DataSet<int, string>[]
                {
                    new DataSet<int, string>(3, "三"),
                    new DataSet<int, string>(1, "いち"),
                    new DataSet<int, string>(4,"four"),
                    new DataSet<int, string>(2, "two"),
                },
                (DataSet<int, string>[] case2) =>
                {
                    var cashe = new LRUcashe<int, string>(5);
                    cashe.Put(case2);
                    cashe.Put(1, "いち");
                    cashe.Put(3, "三");
                    return cashe.CreateArray();
                }
            );
            Test<int, string>.Run(
                "存在しないValueの所得を試みた場合",
                case2,
                new DataSet<int, string>[]
                {
                    new DataSet<int, string>(3, "three"),
                    new DataSet<int, string>(1, "one"),
                    new DataSet<int, string>(4,"four"),
                    new DataSet<int, string>(2, "two"),
                },
                (DataSet<int, string>[] case2) =>
                {
                    var cashe = new LRUcashe<int, string>(5);
                    cashe.Put(case2);
                    Console.WriteLine("\none=" + cashe.Get(1));
                    Console.WriteLine("null=" + cashe.Get(999));
                    Console.WriteLine("three=" + cashe.Get(3));
                    return cashe.CreateArray();
                }
            );
            var case3 = new DataSet<int, string>[]
            {
                new DataSet<int, string>(1, "one"),
            };
            Test<int, string>.Run(
                "境界値テスト：要素数が1の時get,setを試す",
                case3,
                new DataSet<int, string>[]
                {
                    new DataSet<int, string>(1, "いち"),
                },
                (DataSet<int, string>[] data) =>
                {
                    var cashe = new LRUcashe<int, string>(3);
                    cashe.Put(data);
                    cashe.Put(1, "いち");
                    Console.WriteLine("\nいち=" + cashe.Get(1));
                    return cashe.CreateArray();
                }
            );
            Test<int, string>.Run(
                "後からputで値を追加していく",
                case3,
                new DataSet<int, string>[]
                {
                    new DataSet<int, string>(4,"four"),
                    new DataSet<int, string>(3, "three"),
                    new DataSet<int, string>(2, "two"),
                },
                (DataSet<int, string>[] testCase) =>
                {
                    var cashe = new LRUcashe<int, string>(3);
                    cashe.Put(testCase);
                    cashe.Put(2, "two");
                    cashe.Put(3, "three");
                    cashe.Put(4, "four");
                    return cashe.CreateArray();
                }
                );

        }
    }
}
