
using Ruanmou.DB.Interface;
using System;
using System.Reflection; // net 内置的反射帮助类
using Ruanmou.Model;
namespace Example02
{
    class Program
    {
        static void Main(string[] args)
        {
            //通过反射访问调用方法名称
            {
                Assembly assembly = Assembly.Load("Ruanmou.DB.MySql"); //dll 名称；1.  从当前目录加载
                                                                       //Assembly assembly1 = Assembly.LoadFile("@D//a.dll"); //完整路径的加载 可以是别的目录 加载不会报错，但是如果没有依赖项，使用的时候会错
                                                                       //Assembly assembly2 = Assembly.LoadFrom("a.dll"); //带后缀或者完整功能路径加载
                foreach (var item in assembly.GetModules())
                {
                    Console.WriteLine(item.FullyQualifiedName, "该 *.dll全路径 **********************");
                }
                foreach (var item in assembly.GetTypes())
                {
                    Console.WriteLine(item.FullName, "获取类名称，包括命名空间**********************");
                }
                Type type = assembly.GetType("Ruanmou.DB.MySql.MySqlHelper"); //2. 获取类型信息
                object show = Activator.CreateInstance(type); // 3. 创建该对象
                IDBHelper helper = (IDBHelper)show; //4.显示转化类型
                helper.Query(); //5. 调用方法
            }

            //访问重写后的构造函数
            {
                Assembly assembly = Assembly.Load("Ruanmou.DB.SqlServer");
                Type type = assembly.GetType("Ruanmou.DB.SqlServer.ReflectionTest");
                object oReflectionTest1 = Activator.CreateInstance(type);
                object oReflectionTest2 = Activator.CreateInstance(type, new object[] { 123 });
                object oReflectionTest3 = Activator.CreateInstance(type, new object[] { "123" });

            }
            //读取方法
            {
                Assembly assembly = Assembly.Load("Ruanmou.DB.SqlServer"); //读取dll
                Type type = assembly.GetType("Ruanmou.DB.SqlServer.ReflectionTest"); //读去类
                object oReflectionTest = Activator.CreateInstance(type); //创建对像
                MethodInfo method = type.GetMethod("Show"); // 获取方法
                //method.Invoke(oReflectionTest, null); // 执行方法（该方法没有参数）


                MethodInfo method1 = type.GetMethod("Show2");
                //method.Invoke(oReflectionTest, new object[] { 123 });// 执行方法（该方法有一个参数）

            }
            //读取泛型方法
            {
                Assembly assembly = Assembly.Load("Ruanmou.DB.SqlServer");
                Type type = assembly.GetType("Ruanmou.DB.SqlServer.GenericClass`3");
                //object oGeneric = Activator.CreateInstance(type);
                Type newType = type.MakeGenericType(new Type[] { typeof(int), typeof(string), typeof(DateTime) });
                object oGeneric = Activator.CreateInstance(newType);
            }

            //利用泛型实现 DTO 赋值
            People people = new People();
            people.Id = 123;
            people.Name = "Lutte";
            people.Description = "高级班的新学员";
            Type typePeople = typeof(People);
            Type typePeopleDTO = typeof(PeopleDTO);
            object peopleDTO = Activator.CreateInstance(typePeopleDTO);
            foreach (var item in typePeopleDTO.GetProperties()) //遍历赋值对象所有属性
            {
                object value = typePeople.GetProperty(item.Name).GetValue(people);
                item.SetValue(peopleDTO, value);

                foreach (var filed in typePeopleDTO.GetFields()) //遍历对象所有的字段
                {
                    object value1 = typePeople.GetField(filed.Name).GetValue(people);
                    filed.SetValue(peopleDTO, value1);
                }
            }

            var dto =  AutoMapper.CreateDto<PeopleDTO, People>(people);

            Console.ReadKey();
        }
    }
}
