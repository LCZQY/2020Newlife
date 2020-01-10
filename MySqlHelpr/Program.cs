using MySqlHelpr.Attributes;
using MySqlHelpr.Model;
using System;

namespace MySqlHelpr
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            string id = Guid.NewGuid().ToString();
            //插入
            {
                IdentityRole identity = new IdentityRole()
                {
                    Type = "1",
                    Discriminator = "测试",
                    IsDeleted = true,
                    ConcurrencyStamp = "新增",
                    Id = id,
                    Name = "管理员",
                    NormalizedName = "测试",
                    OrganizationId = "1"
                };
                if (identity.IsValid())
                {
                    if (await Commom.MySqlHelper.InSertAsync(identity))
                    {
                        Console.WriteLine("新增成功啦！！");
                    }
                    else
                    {
                        Console.WriteLine("新增失败啦！！");
                    }
                }
                else
                {
                    Console.WriteLine("模型验证失败！！");
                }
            }

            //查询
            {
                var list = await Commom.MySqlHelper.GetListAsync(new IdentityRole { });
                foreach (var item in list)
                {
                    Console.WriteLine($"{nameof(item.Id)}:{item.Id}, {nameof(item.Name)}:{item.Name},{nameof(item.Discriminator)}:{item.Discriminator}, {nameof(item.ConcurrencyStamp)}:{item.ConcurrencyStamp}, {nameof(item.IsDeleted)}:{item.IsDeleted}, {nameof(item.OrganizationId)}:{item.OrganizationId} , {nameof(item.Type)}:{item.Type} ,");
                }

                var list11 = await Commom.MySqlHelper.GetListAsync(new IdentityRole { }, id);
                foreach (var item in list11)
                {
                    Console.WriteLine($"{nameof(item.Id)}:{item.Id}, {nameof(item.Name)}:{item.Name},{nameof(item.Discriminator)}:{item.Discriminator}, {nameof(item.ConcurrencyStamp)}:{item.ConcurrencyStamp}, {nameof(item.IsDeleted)}:{item.IsDeleted}, {nameof(item.OrganizationId)}:{item.OrganizationId} , {nameof(item.Type)}:{item.Type} ,");
                }
            }

            //修改
            {
                IdentityRole identity = new IdentityRole()
                {
                    Type = "1",
                    Discriminator = "修改测试",
                    IsDeleted = true,
                    ConcurrencyStamp = "修改",
                    Id = id,
                    Name = "修改管理员",
                    NormalizedName = "修改测试",
                    OrganizationId = "1"
                };
                if (identity.IsValid())
                {
                    if (await Commom.MySqlHelper.UpdateAsync(identity, id))
                    {
                        Console.WriteLine("更新成功啦！！");
                    }
                    else
                    {
                        Console.WriteLine("更新失败啦！！");
                    }
                }
                else
                {
                    Console.WriteLine("模型验证失败！！");
                }
            }

            //删除
            {

                if (await Commom.MySqlHelper.RemoveAsync(new IdentityRole() { }, id))
                {
                    Console.WriteLine("删除成功啦！！");
                }
                else
                {
                    Console.WriteLine("删除失败啦！！");
                }
            }

        }
    }
}
