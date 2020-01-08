using System;

namespace Example
{
    /// <summary>
    /// 泛型
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int iValue = 123;
            string sValue = "456";
            DateTime dtValue = DateTime.Now;

            Console.WriteLine("***********CommonMethodc传统***************");
            CommonMethod.ShowInt(iValue);
            CommonMethod.ShowString(sValue);
            CommonMethod.ShowDateTime(dtValue);
            Console.WriteLine("***********Object一个对象变量***************");
            CommonMethod.ShowObject(iValue);
            CommonMethod.ShowObject(sValue);
            CommonMethod.ShowObject(dtValue);
            Console.WriteLine("***********Generic泛型参数***************");
            GenericMethod.Show<int>(iValue);
            GenericMethod.Show<string>(sValue);
            GenericMethod.Show<DateTime>(dtValue);


            Console.WriteLine("***********GenericCacheTest 测试泛型缓存***************");
            GenericCacheTest.Show();
            Console.ReadKey();


        }
    }
}
