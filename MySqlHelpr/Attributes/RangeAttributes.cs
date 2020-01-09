namespace MySqlHelpr.Attributes
{

    /// <summary>
    /// 数据范围验证
    /// </summary>
    public class RangeAttribute : AbstractValidateAttribute
    {
        public long _Max { get; set; }

        public long _Min { get; set; }

        public RangeAttribute(long max, long min)
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
