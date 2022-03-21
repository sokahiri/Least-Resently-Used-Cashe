using System;

namespace LRUcashe
{
    delegate DataSet<Tkey, Tvalue>[] Function<Tkey, Tvalue>(DataSet<Tkey, Tvalue>[] dataSets);

    static class Test<Tkey,Tvalue>
    {
        static public void Run(string testName,DataSet<Tkey,Tvalue>[] testCase, DataSet<Tkey,Tvalue>[] expectedValue, Function<Tkey,Tvalue> function)
        {
            Console.WriteLine("------start------");
            Console.WriteLine("test target:" + testName);
            Console.Write("\ntest result: ");
            DataSet<Tkey,Tvalue>[] output = function(testCase);
            if (IsEqual(output, expectedValue))
            {
                Console.WriteLine("passed");
            }
            else
            {
                Console.WriteLine("failed");
                Console.WriteLine("\ntestCase:");
                for (int i = 0; i < testCase.Length; i++)
                {
                    Console.WriteLine("Key:" + testCase[i].Key + ", Value:" + testCase[i].Value);
                }
                Console.WriteLine("\nexpected:");
                for (int i = 0; i < expectedValue.Length; i++)
                {
                    Console.WriteLine("Key:" + expectedValue[i].Key + ", Value:" + expectedValue[i].Value);
                }
                Console.WriteLine("\noutput:");
                for(int i = 0; i < output.Length; i++)
                {
                    Console.WriteLine("key:" + output[i].Key + ", value:" + output[i].Value);
                }
            }
            Console.WriteLine("------end------");
        }

        static public bool IsEqual(DataSet<Tkey,Tvalue>[] output, DataSet<Tkey,Tvalue>[] expectedValue)
        {
            if (output.Length != expectedValue.Length) return false;
            for(int i = 0; i < output.Length; i++)
            {
                bool isEqual = output[i].Value.Equals(expectedValue[i].Value) && output[i].Key.Equals(expectedValue[i].Key);
                if (!isEqual) return false;
            }
            return true;
        }
    }
}
