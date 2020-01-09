using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MySqlHelpr.Attributes
{
    /// <summary>
    /// 非空判断
    /// </summary>
    public class RequiredAttribute : AbstractValidateAttribute
    {
        public override bool Validate(object oValue)
        {
            if (oValue != null && !string.IsNullOrWhiteSpace(oValue.ToString()))
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    ///  正则匹配
    /// </summary>
    public class RegularExgAttribute : AbstractValidateAttribute
    {
        private string _text;
        public RegularExgAttribute(string text)
        {
            _text = text;
        }
        public override bool Validate(object oValue)
        {
            if (oValue == null) return false;
            if (Regex.IsMatch(oValue.ToString(), this._text))
            {
                return true;
            }
            return false;
        }
    }

}
