using Example03.Attributes;
using System;

namespace Example03
{
    public class Student
    {

        [CustomAttribute]
        public int Id { get; set; }
        [Leng(5, 10)]//还有各种检查
        public string Name { get; set; }
        [Leng(20, 50)]
        public string Accont { get; set; }

        /// <summary>
        /// 10001~999999999999
        /// </summary>
        [Long(10001, 999999999999)]
        public long QQ { get; set; }


        //private long _QQ2 = 0;//解决数据合法性，给属性增加了太多的事儿
        //public long QQ2
        //{
        //    get
        //    {
        //        return this._QQ2;
        //    }
        //    set
        //    {
        //        if (value > 10001 && value < 999999999999)
        //        {
        //            _QQ2 = value;
        //        }
        //        else
        //        {
        //            throw new Exception();
        //        }
        //    }
        //}


        [CustomAttribute]
        public void Study()
        {
            Console.WriteLine($"这里是{this.Name}跟着Eleven老师学习");
        }

        [Custom()] //特性也可修饰方法
        [return: Custom()] //特性也可修饰返回体
        public string Answer([Custom]string name) //特性也可修饰参数
        {
            return $"This is {name}";
        }

    }

    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserState
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Remark("正常")]
        Normal = 0,//左边是字段名称  右边是数据库值   哪里放描述？  注释
        /// <summary>
        /// 冻结
        /// </summary>
        [Remark("冻结")]
        Frozen = 1,
        /// <summary>
        /// 删除
        /// </summary>
        //[Remark("删除")]
        Deleted = 2
    }


}
