using System;
using System.Collections.Generic;
using System.Text;

namespace MySqlHelpr.Attributes
{
    /// <summary>
    /// 字符长度验证
    /// </summary>
    public class StringLengthAttribute : AbstractValidateAttribute
    {

        public int _Max { get; set; }

        public int _Min { get; set; }

        public StringLengthAttribute(int min, int max)
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
}
