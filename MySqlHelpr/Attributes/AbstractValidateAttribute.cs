using System;
using System.Collections.Generic;
using System.Text;

namespace MySqlHelpr.Attributes
{
    /// <summary>
    /// 使用一个抽象的基类，让所有自定义基类都继承，统一重写验证方法即可
    /// </summary>
    public abstract class AbstractValidateAttribute : Attribute
    {
        public abstract bool Validate(object oValue);
    }


    /// <summary>
    /// 使用反射找到该基类的特性类调用该验证方法
    /// </summary>
    public static class ValidateExtension
    {
        /// <summary>
        /// 模型验证
        /// </summary>
        /// <param name="oObject"></param>
        /// <returns></returns>
        public static bool IsValid(this Object oObject)
        {
            Type type = oObject.GetType();
            foreach (var proper in type.GetProperties()) //遍历所有的属性
            {
                if (proper.IsDefined(typeof(AbstractValidateAttribute), true))
                {
                    object[] attributeArray = proper.GetCustomAttributes(typeof(AbstractValidateAttribute), true);
                    foreach (AbstractValidateAttribute attribute in attributeArray)
                    {
                        if (!attribute.Validate(proper.GetValue(oObject)))
                            return false;
                    }
                }
            }
            return true;
        }
    }

}
