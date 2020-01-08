
using Example03.Attributes;
using Example03.ManagerAttributes;
using System;

namespace Example03
{
    /// <summary>
    /// 特性：     
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            {
                //实战一： 使用特性获取枚举中的描述值
                Console.WriteLine(UserState.Frozen.GetEnumRemark());
                Console.WriteLine(UserState.Normal.GetEnumRemark());
                Console.WriteLine(UserState.Deleted.GetEnumRemark());
            }

            {
                //实战二：  使用特性做模型验证
                Student student = new Student
                {
                    Accont = "郑讫那个用",
                    Id = 12345,
                    Name = "老郑",
                    QQ = 12345678
                };
                Console.WriteLine(student.IsValidate() ? "数据正常" : "数据异常");
            }

            Console.ReadKey();
        }
    }
}
