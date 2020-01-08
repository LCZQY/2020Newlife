using System;
using System.Collections.Generic;
using System.Text;

namespace Example
{
    public class CommonMethod
    {

        /// <summary>
        /// 打印个int值方法
        /// 
        /// 因为方法声明的时候，写死了参数类型
        /// 已婚的男人 Eleven San
        /// </summary>
        /// <param name="iParameter"></param>
        public static void ShowInt(int iParameter)
        {
            Console.WriteLine("This is {0},parameter={1},type={2}",typeof(CommonMethod).Name, iParameter.GetType().Name, iParameter);
        }

        /// <summary>
        /// 打印个string值方法
        /// </summary>
        /// <param name="sParameter"></param>
        public static void ShowString(string sParameter)
        {
            Console.WriteLine("This is {0},parameter={1},type={2}",
                typeof(CommonMethod).Name, sParameter.GetType().Name, sParameter);
        }

        /// <summary>
        /// 打印个DateTime值方法
        /// </summary>
        /// <param name="oParameter"></param>
        public static void ShowDateTime(DateTime dtParameter)
        {
            Console.WriteLine("This is {0},parameter={1},type={2}",
                typeof(CommonMethod).Name, dtParameter.GetType().Name, dtParameter);
        }


        /// <summary>
        /// 申明一个object的对象变量参数，也可以接受不同的变量
        /// </summary>
        /// <param name="oParameter"></param>
        public static void ShowObject(object oParameter)
        {
            Console.WriteLine("This is {0},parameter={1},type={2}",
               typeof(CommonMethod), oParameter.GetType().Name, oParameter);
        }


       

    }


    /// <summary>
    /// 泛型
    /// </summary>
    public class GenericMethod
    {
        /// <summary>
        /// 泛型方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tParameter"></param>
        public static void Show<T>(T tParameter)
        {
            Console.WriteLine("This is {0},parameter={1},type={2}",
                typeof(GenericMethod), tParameter.GetType().Name, tParameter.ToString());
        }
    }
}
