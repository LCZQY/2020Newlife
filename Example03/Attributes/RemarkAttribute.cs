using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Example03.Attributes
{

    public class RemarkAttribute : Attribute
    {
        private string _remark;
        /// <summary>
        /// 接收一个描述字符串
        /// </summary>
        /// <param name="remark"></param>
        public RemarkAttribute(string remark)
        {
            _remark = remark;
        }
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <returns></returns>
        public string GetRemark()
        {
            return _remark;
        }
    }

    public static class RemarkExtension
    {
        public static string GetEnumRemark(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo field =  type.GetField(value.ToString());
            //如果找到该特性后,直接拿出该特性
            if (field.IsDefined(typeof(RemarkAttribute), true))
            {
                RemarkAttribute attribute = (RemarkAttribute)field.GetCustomAttribute(typeof(RemarkAttribute), true);
                return attribute.GetRemark();
            }
            else
            {
                return value.ToString();
            }
        }
       
    }

}
