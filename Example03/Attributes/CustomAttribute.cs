using Example03.ManagerAttributes;
using System;

namespace Example03
{
    public class CustomAttribute : Attribute
    {
    }

    /// <summary>
    /// 检查字符长度特性(继承于基类特性，重写验证方法)
    /// </summary>
    public class LengAttribute : AbstractValidateAttribute
    {
        public int _Max { get; set; }

        public int _Min { get; set; }

        public LengAttribute(int max, int min)
        {
            _Max = max;
            _Min = min;
        }

        public override bool Validate(object oValue)
        {
            if (oValue != null && !string.IsNullOrWhiteSpace(oValue.ToString()))
            {
                int length = oValue.ToString().Length;
                if (length > this._Min && length < this._Max)
                {
                    return true;
                }
            }
            return false;
        }
    }



    /// <summary>
    /// 检查数据范围特性(继承于基类特性，重写验证方法)
    /// </summary>
    public class LongAttribute : AbstractValidateAttribute
    {
        public long _Max { get; set; }

        public long _Min { get; set; }

        public LongAttribute(long max, long min)
        {
            _Max = max;
            _Min = min;
        }

        public override bool Validate(object value)
        {
            //可以改成一句话
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                if (long.TryParse(value.ToString(), out long lResult))
                {
                    if (lResult > this._Min && lResult < this._Max)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

}