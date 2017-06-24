using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rc.PetaPoco;

namespace Rc.Test
{
    [TestClass]
    public class SqlHelperTest
    {
        SqlHelper helper;
        public SqlHelperTest()
        {
            helper = new SqlHelper(DataBaseType.Oracle);
        }

        [TestMethod]
        public void TestBuild1()
        {
            helper.Build<ORG_DEPARTMENT.SqlAttr>((f, d) =>
            f.Select(d.AllColumns)
            .From(d.TableName)
            .Where("1=1"));

            Console.Write(helper.Result.Sql);
        }

        [TestMethod]
        public void TestBuild2()
        {
            helper.Build<ORG_USER.SqlAttr, ORG_DEPARTMENT.SqlAttr>((f, t, c) =>
             f.Select(t.ID, t.NAME, t.BIND_IP, t.DESCRIPTION, c.NAME.Alias("CNAME"), c.FAX, c.REMARK)
             .From(t.TableName).InnerJoin(c.TableName).On(c.ID == t.DEPT_ID)
             .Where((t.ID == "@0" | t.SORT_INDEX >= "5") & t.MOBILE % "@1", "200908281049324536039", "188%"));

            Console.Write(helper.Result.Sql + "\r\n" + "\r\n" + Newtonsoft.Json.JsonConvert.SerializeObject(helper.Result.Args));
        }

        [TestMethod]
        public void TestInsert()
        {
            helper.Insert(new ORG_DEPARTMENT
            {
                ID = "123",
                NAME = "测试",
                NO = "778885"
            });

            Console.Write(helper.Result.Sql + "\r\n" + "\r\n" + Newtonsoft.Json.JsonConvert.SerializeObject(helper.Result.Args));
        }

        [TestMethod]
        public void TestUpdate()
        {
            //helper.Update(new ORG_DEPARTMENT
            //{
            //    ID = "123",
            //    NAME = "测试",
            //    NO = "778885"
            //}, "NAME", "NO");

            helper.Cud<ORG_DEPARTMENT.SqlAttr>((f, d) =>
            f.Update(d.TableName)
            .Set(d.PATH, d.SORT_INDEX, d.REMARK).Args("54565", 6, "我是测试的")
            .Where(d.NO % "@0", "lkij%"));

            Console.Write(helper.Result.Sql + "\r\n" + "\r\n" + Newtonsoft.Json.JsonConvert.SerializeObject(helper.Result.Args));
        }

        [TestMethod]
        public void TestDelete()
        {
            //helper.Delete(new ORG_DEPARTMENT
            //{
            //    ID = "123",
            //    NAME = "测试",
            //    NO = "778885"
            //});

            helper.Cud<ORG_DEPARTMENT.SqlAttr>((f, t) =>
                f.Delete(t.TableName)
                .Where(t.ID == "@0", "14598"));
            
            Console.Write(helper.Result.Sql + "\r\n" + "\r\n" + Newtonsoft.Json.JsonConvert.SerializeObject(helper.Result.Args));
        }
    }
}
