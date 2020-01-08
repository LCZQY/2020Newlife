using System;
using System.Collections.Generic;
using System.Text;

namespace Example02
{
    /// <summary>
    /// 手写Auomapper
    /// </summary>
    public class AutoMapper
    {

        /// <summary>
        /// 自动赋值对象
        /// </summary>
        /// <typeparam name="TDestination">dto对象</typeparam>
        /// <typeparam name="T1">赋值对象</typeparam>
        /// <param name="source">源数据</param>
        /// <returns></returns>
        public static dynamic CreateDto<TDestination, T1>(T1 source)
        {
            Type destination = typeof(TDestination);
            Type objects = typeof(T1);
            var instance = Activator.CreateInstance(destination);
            foreach (var item in destination.GetProperties())
            {
            
                object value = objects.GetProperty(item.Name).GetValue(source);
                item.SetValue(instance, value);


                foreach (var filed in destination.GetFields()) //遍历对象所有的字段
                {
                    object value1 = objects.GetField(filed.Name).GetValue(source);
                    filed.SetValue(instance, value1);
                }
            }
            return instance;
        }


    }
}
