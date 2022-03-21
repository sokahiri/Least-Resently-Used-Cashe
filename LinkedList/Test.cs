using System;
using System.Collections.Generic;
using LRUcashe.LinkedList.Interfaces;

namespace LRUcashe.LinkedList
{
    delegate T[] Function<T>(T[] array);
    delegate bool IsEqual<T>(T[] input, T[] expectedValue);

    static class Test<T>
    {
        static public void Run(T[] testCase, T[] expectedValue, Function<T> function, IsEqual<T> isEqual)
        {
            Console.WriteLine("------start------");
            Console.WriteLine("test target:" + function.Method.Name);
            Console.WriteLine("testCase:" + String.Join(",", testCase));
            Console.WriteLine("expected:" + String.Join(",", expectedValue));
            Console.Write("test result: ");
            T[] output = function(testCase);
            if (isEqual(output, expectedValue))
            {
                Console.WriteLine("passed");
            }
            else
            {
                Console.WriteLine("failed");
                Console.WriteLine("output:" + String.Join(",", output));
            }
            Console.WriteLine("------end------");
        }

        static public bool IsSameArray(T[] input, T[] expectedValue)
        {
            if (input.Length != expectedValue.Length) { return false; }
            for (int i = 0; i < input.Length; i++)
            {
                if (!input[i].Equals(expectedValue[i])) { return false; }
            }
            return true;
        }

        static public T[] QueueTest(T[] array)
        {
            var linkedList = new LinkedList<T>(array);
            return CreateArrayByQueue(linkedList);
        }

        static public T[] StackTest(T[] array)
        {
            var linkedList = new LinkedList<T>(array);
            return CreateArrayByStack(linkedList);
        }

        static public T[] DequeTest(T[] array)
        {
            var linkedList = new LinkedList<T>(array);
            return CreateArrayByDeque(linkedList);
        }

        static public T[] CreateArrayByQueue(IQueue<T> queue)
        {
            var list = new List<T>();
            while (!queue.IsEmpty())
            {
                list.Add(queue.Poll());
            }
            var array = new T[list.Count];
            for(int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }
            return array;
        }
        
        static public T[] CreateArrayByStack(IStack<T> stack)
        {
            var list = new List<T>();
            while (!stack.IsEmpty())
            {
                list.Add(stack.Pop());
            }
            var array = new T[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }
            return array;
        }

        static public T[] CreateArrayByDeque(IDeque<T> deque)
        {
            var list = new List<T>();
            while (!deque.IsEmpty())
            {
                list.Add(deque.Poll());
                if (deque.IsEmpty()) break;
                list.Add(deque.Pop());
            }
            var array = new T[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }
            return array;
        }
    }
}
