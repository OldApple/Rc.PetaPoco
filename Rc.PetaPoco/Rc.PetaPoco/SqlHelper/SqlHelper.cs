/*
*   描述：Sql语句构造器
*               
*   作者：Simon
*   时间：2017.06.09
*/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Fasterflect;

namespace Rc.PetaPoco
{
    /// <summary>
    /// Sql语句帮助器
    /// </summary>
    public class SqlHelper
    {
        #region Fields/Properties
        /// <summary>
        /// 正则表达式 匹配@参数前缀
        /// </summary>
        private static Regex rxParamsPrefix = new Regex(@"(?<!@)@\w+", RegexOptions.Compiled);
        /// <summary>
        /// 数据库类型
        /// </summary>
        DataBaseType _dbType;
        Sql _sql;
        /// <summary>
        /// 构造成的Sql对象
        ///     该Sql对象在petapoco中使用
        /// </summary>
        public Sql Sql
        {
            get
            {
                return _sql;
            }
            private set
            {
                _sql = value;
                _result.Clear();
            }
        }
        StrSql _result;
        /// <summary>
        /// 获取最终Sql语句及参数
        /// </summary>
        public StrSql Result
        {
            get
            {
                if (string.IsNullOrEmpty(_result.Sql))
                {
                    string sql = _sql.SQL;
                    object[] args = _sql.Arguments;

                    // 完成参数替换
                    string paramPrefix = RuleProvider.Rule(_dbType).GetParameterPrefix();
                    if (paramPrefix != "@")
                        sql = rxParamsPrefix.Replace(sql, m => paramPrefix + m.Value.Substring(1));
                    sql = sql.Replace("@@", "@"); // <- double @@ escapes a single @
                    _result = new StrSql(sql, args);//此处不能使用Sql的原因在于如果参数前缀不为@的话，则编译出的最终结果参数为空（参数帮助器是根据@识别参数的）
                }
                return _result;
            }
        }
        /// <summary>
        /// 获取新实例
        /// </summary>
        public static SqlHelper Instance
        {
            get
            {
                return new SqlHelper();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">数据库类型</param>
        public SqlHelper(DataBaseType type)
        {
            _dbType = type;
        }
        /// <summary>
        /// 构造函数
        ///     数据库类型默认 Oracle
        /// </summary>
        public SqlHelper()
            : this(DataBaseType.Oracle)
        {

        }
        /// <summary>
        /// 设置数据库类型
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <returns>当前实例</returns>
        public SqlHelper SetDbType(DataBaseType type)
        {
            _dbType = type;
            return this;
        }
        #endregion

        #region Build for Select
        /// <summary>
        /// 单表查询Sql语句
        /// </summary>
        /// <typeparam name="T">实体SqlAttr</typeparam>
        /// <param name="func">Sql构造方法</param>
        public void Build<T>(Func<Sql, T, Sql> func) where T : ISqlAttr, new()
        {
            //获取参数名
            ParameterInfo[] pis = func.Method.GetParameters();
            T t1 = new T();
            t1.SetTblInfo(pis[1].Name, _dbType);

            Sql = func(Sql.Builder, t1);
        }
        /// <summary>
        /// 双表查询Sql语句
        /// </summary>
        /// <typeparam name="T1">实体SqlAttr</typeparam>
        /// <typeparam name="T2">实体SqlAttr</typeparam>
        /// <param name="func">Sql构造方法</param>
        public void Build<T1, T2>(Func<Sql, T1, T2, Sql> func) where T1 : ISqlAttr, new() where T2 : ISqlAttr, new()
        {
            //获取参数名
            ParameterInfo[] pis = func.Method.GetParameters();
            T1 t1 = new T1();
            t1.SetTblInfo(pis[1].Name, _dbType);
            T2 t2 = new T2();
            t2.SetTblInfo(pis[2].Name, _dbType);

            Sql = func(Sql.Builder, t1, t2);
        }
        /// <summary>
        /// 三表查询Sql语句
        /// </summary>
        /// <typeparam name="T1">实体SqlAttr</typeparam>
        /// <typeparam name="T2">实体SqlAttr</typeparam>
        /// <typeparam name="T3">实体SqlAttr</typeparam>
        /// <param name="func">Sql构造方法</param>
        public void Build<T1, T2, T3>(Func<Sql, T1, T2, T3, Sql> func) where T1 : ISqlAttr, new() where T2 : ISqlAttr, new() where T3 : ISqlAttr, new()
        {
            //获取参数名
            ParameterInfo[] pis = func.Method.GetParameters();
            T1 t1 = new T1();
            t1.SetTblInfo(pis[1].Name, _dbType);
            T2 t2 = new T2();
            t2.SetTblInfo(pis[2].Name, _dbType);
            T3 t3 = new T3();
            t3.SetTblInfo(pis[3].Name, _dbType);

            Sql = func(Sql.Builder, t1, t2, t3);
        }
        /// <summary>
        /// 四表查询Sql语句
        /// </summary>
        /// <typeparam name="T1">实体SqlAttr</typeparam>
        /// <typeparam name="T2">实体SqlAttr</typeparam>
        /// <typeparam name="T3">实体SqlAttr</typeparam>
        /// <typeparam name="T4">实体SqlAttr</typeparam>
        /// <param name="func">Sql构造方法</param>
        public void Build<T1, T2, T3, T4>(Func<Sql, T1, T2, T3, T4, Sql> func) where T1 : ISqlAttr, new() where T2 : ISqlAttr, new() where T3 : ISqlAttr, new() where T4 : ISqlAttr, new()
        {
            //获取参数名
            ParameterInfo[] pis = func.Method.GetParameters();
            T1 t1 = new T1();
            t1.SetTblInfo(pis[1].Name, _dbType);
            T2 t2 = new T2();
            t2.SetTblInfo(pis[2].Name, _dbType);
            T3 t3 = new T3();
            t3.SetTblInfo(pis[3].Name, _dbType);
            T4 t4 = new T4();
            t4.SetTblInfo(pis[4].Name, _dbType);

            Sql = func(Sql.Builder, t1, t2, t3, t4);
        }
        /// <summary>
        /// 五表查询Sql语句
        /// </summary>
        /// <typeparam name="T1">实体SqlAttr</typeparam>
        /// <typeparam name="T2">实体SqlAttr</typeparam>
        /// <typeparam name="T3">实体SqlAttr</typeparam>
        /// <typeparam name="T4">实体SqlAttr</typeparam>
        /// <typeparam name="T5">实体SqlAttr</typeparam>
        /// <param name="func">Sql构造方法</param>
        public void Build<T1, T2, T3, T4, T5>(Func<Sql, T1, T2, T3, T4, T5, Sql> func) where T1 : ISqlAttr, new() where T2 : ISqlAttr, new() where T3 : ISqlAttr, new() where T4 : ISqlAttr, new() where T5 : ISqlAttr, new()
        {
            //获取参数名
            ParameterInfo[] pis = func.Method.GetParameters();
            T1 t1 = new T1();
            t1.SetTblInfo(pis[1].Name, _dbType);
            T2 t2 = new T2();
            t2.SetTblInfo(pis[2].Name, _dbType);
            T3 t3 = new T3();
            t3.SetTblInfo(pis[3].Name, _dbType);
            T4 t4 = new T4();
            t4.SetTblInfo(pis[4].Name, _dbType);
            T5 t5 = new T5();
            t5.SetTblInfo(pis[5].Name, _dbType);

            Sql = func(Sql.Builder, t1, t2, t3, t4, t5);
        }
        /// <summary>
        /// 六表查询Sql语句
        /// </summary>
        /// <typeparam name="T1">实体SqlAttr</typeparam>
        /// <typeparam name="T2">实体SqlAttr</typeparam>
        /// <typeparam name="T3">实体SqlAttr</typeparam>
        /// <typeparam name="T4">实体SqlAttr</typeparam>
        /// <typeparam name="T5">实体SqlAttr</typeparam>
        /// <typeparam name="T6">实体SqlAttr</typeparam>
        /// <param name="func">Sql构造方法</param>
        public void Build<T1, T2, T3, T4, T5, T6>(Func<Sql, T1, T2, T3, T4, T5, T6, Sql> func) where T1 : ISqlAttr, new() where T2 : ISqlAttr, new() where T3 : ISqlAttr, new() where T4 : ISqlAttr, new() where T5 : ISqlAttr, new() where T6 : ISqlAttr, new()
        {
            //获取参数名
            ParameterInfo[] pis = func.Method.GetParameters();
            T1 t1 = new T1();
            t1.SetTblInfo(pis[1].Name, _dbType);
            T2 t2 = new T2();
            t2.SetTblInfo(pis[2].Name, _dbType);
            T3 t3 = new T3();
            t3.SetTblInfo(pis[3].Name, _dbType);
            T4 t4 = new T4();
            t4.SetTblInfo(pis[4].Name, _dbType);
            T5 t5 = new T5();
            t5.SetTblInfo(pis[5].Name, _dbType);
            T6 t6 = new T6();
            t6.SetTblInfo(pis[6].Name, _dbType);

            Sql = func(Sql.Builder, t1, t2, t3, t4, t5, t6);
        }
        /// <summary>
        /// 七表查询Sql语句
        /// </summary>
        /// <typeparam name="T1">实体SqlAttr</typeparam>
        /// <typeparam name="T2">实体SqlAttr</typeparam>
        /// <typeparam name="T3">实体SqlAttr</typeparam>
        /// <typeparam name="T4">实体SqlAttr</typeparam>
        /// <typeparam name="T5">实体SqlAttr</typeparam>
        /// <typeparam name="T6">实体SqlAttr</typeparam>
        /// <typeparam name="T7">实体SqlAttr</typeparam>
        /// <param name="func">Sql构造方法</param>
        public void Build<T1, T2, T3, T4, T5, T6, T7>(Func<Sql, T1, T2, T3, T4, T5, T6, T7, Sql> func) where T1 : ISqlAttr, new() where T2 : ISqlAttr, new() where T3 : ISqlAttr, new() where T4 : ISqlAttr, new() where T5 : ISqlAttr, new() where T6 : ISqlAttr, new() where T7 : ISqlAttr, new()
        {
            //获取参数名
            ParameterInfo[] pis = func.Method.GetParameters();
            T1 t1 = new T1();
            t1.SetTblInfo(pis[1].Name, _dbType);
            T2 t2 = new T2();
            t2.SetTblInfo(pis[2].Name, _dbType);
            T3 t3 = new T3();
            t3.SetTblInfo(pis[3].Name, _dbType);
            T4 t4 = new T4();
            t4.SetTblInfo(pis[4].Name, _dbType);
            T5 t5 = new T5();
            t5.SetTblInfo(pis[5].Name, _dbType);
            T6 t6 = new T6();
            t6.SetTblInfo(pis[6].Name, _dbType);
            T7 t7 = new T7();
            t7.SetTblInfo(pis[7].Name, _dbType);

            Sql = func(Sql.Builder, t1, t2, t3, t4, t5, t6, t7);
        }
        /// <summary>
        /// 八表查询Sql语句
        /// </summary>
        /// <typeparam name="T1">实体SqlAttr</typeparam>
        /// <typeparam name="T2">实体SqlAttr</typeparam>
        /// <typeparam name="T3">实体SqlAttr</typeparam>
        /// <typeparam name="T4">实体SqlAttr</typeparam>
        /// <typeparam name="T5">实体SqlAttr</typeparam>
        /// <typeparam name="T6">实体SqlAttr</typeparam>
        /// <typeparam name="T7">实体SqlAttr</typeparam>
        /// <typeparam name="T8">实体SqlAttr</typeparam>
        /// <param name="func">Sql构造方法</param>
        public void Build<T1, T2, T3, T4, T5, T6, T7, T8>(Func<Sql, T1, T2, T3, T4, T5, T6, T7, T8, Sql> func) where T1 : ISqlAttr, new() where T2 : ISqlAttr, new() where T3 : ISqlAttr, new() where T4 : ISqlAttr, new() where T5 : ISqlAttr, new() where T6 : ISqlAttr, new() where T7 : ISqlAttr, new() where T8 : ISqlAttr, new()
        {
            //获取参数名
            ParameterInfo[] pis = func.Method.GetParameters();
            T1 t1 = new T1();
            t1.SetTblInfo(pis[1].Name, _dbType);
            T2 t2 = new T2();
            t2.SetTblInfo(pis[2].Name, _dbType);
            T3 t3 = new T3();
            t3.SetTblInfo(pis[3].Name, _dbType);
            T4 t4 = new T4();
            t4.SetTblInfo(pis[4].Name, _dbType);
            T5 t5 = new T5();
            t5.SetTblInfo(pis[5].Name, _dbType);
            T6 t6 = new T6();
            t6.SetTblInfo(pis[6].Name, _dbType);
            T7 t7 = new T7();
            t7.SetTblInfo(pis[7].Name, _dbType);
            T8 t8 = new T8();
            t8.SetTblInfo(pis[8].Name, _dbType);

            Sql = func(Sql.Builder, t1, t2, t3, t4, t5, t6, t7, t8);
        }
        /// <summary>
        /// 九表查询Sql语句
        /// </summary>
        /// <typeparam name="T1">实体SqlAttr</typeparam>
        /// <typeparam name="T2">实体SqlAttr</typeparam>
        /// <typeparam name="T3">实体SqlAttr</typeparam>
        /// <typeparam name="T4">实体SqlAttr</typeparam>
        /// <typeparam name="T5">实体SqlAttr</typeparam>
        /// <typeparam name="T6">实体SqlAttr</typeparam>
        /// <typeparam name="T7">实体SqlAttr</typeparam>
        /// <typeparam name="T8">实体SqlAttr</typeparam>
        /// <typeparam name="T9">实体SqlAttr</typeparam>
        /// <param name="func">Sql构造方法</param>
        public void Build<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<Sql, T1, T2, T3, T4, T5, T6, T7, T8, T9, Sql> func) where T1 : ISqlAttr, new() where T2 : ISqlAttr, new() where T3 : ISqlAttr, new() where T4 : ISqlAttr, new() where T5 : ISqlAttr, new() where T6 : ISqlAttr, new() where T7 : ISqlAttr, new() where T8 : ISqlAttr, new() where T9 : ISqlAttr, new()
        {
            //获取参数名
            ParameterInfo[] pis = func.Method.GetParameters();
            T1 t1 = new T1();
            t1.SetTblInfo(pis[1].Name, _dbType);
            T2 t2 = new T2();
            t2.SetTblInfo(pis[2].Name, _dbType);
            T3 t3 = new T3();
            t3.SetTblInfo(pis[3].Name, _dbType);
            T4 t4 = new T4();
            t4.SetTblInfo(pis[4].Name, _dbType);
            T5 t5 = new T5();
            t5.SetTblInfo(pis[5].Name, _dbType);
            T6 t6 = new T6();
            t6.SetTblInfo(pis[6].Name, _dbType);
            T7 t7 = new T7();
            t7.SetTblInfo(pis[7].Name, _dbType);
            T8 t8 = new T8();
            t8.SetTblInfo(pis[8].Name, _dbType);
            T9 t9 = new T9();
            t9.SetTblInfo(pis[9].Name, _dbType);

            Sql = func(Sql.Builder, t1, t2, t3, t4, t5, t6, t7, t8, t9);
        }
        #endregion

        #region Build for Cud
        public void Cud<T>(Func<CudSql, T,Sql> func) where T : new()
        {
            Sql = func(CudSql.Builder.SetDbType(_dbType), new T());
        }
        #endregion

        #region Cud
        /// <summary>
        /// 实体生成插入语句
        /// </summary>
        /// <param name="poco">poco实体</param>
        public void Insert(object poco)
        {
            try
            {
                if (poco == null)
                    throw new Exception("输入参数 poco 不能为空！");

                Type type = poco.GetType();
                var names = new List<string>();
                var values = new List<string>();
                var paras = new List<object>();
                var index = 0;
                foreach (PropertyInfo pi in type.Properties())
                {
                    // Don't insert result columns
                    if (pi.GetCustomAttributes(typeof(ResultColumnAttribute), false).Length > 0)
                        continue;

                    names.Add(RuleProvider.Rule(_dbType).EscapeSqlIdentifier(pi.Name));
                    values.Add(string.Format("{0}{1}", "@", index++));
                    paras.Add(poco.GetPropertyValue(pi.Name));
                }

                string outputClause = String.Empty;

                Sql = new Sql(string.Format("INSERT INTO {0} ({1}){2} VALUES ({3})",
                        RuleProvider.Rule(_dbType).EscapeTableName(type.Name),
                        string.Join(",", names.ToArray()),
                        outputClause,
                        string.Join(",", values.ToArray())
                        ), paras.ToArray());
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        /// <summary>
        /// 实体生成更新语句
        /// </summary>
        /// <param name="poco">poco实体</param>
        /// <param name="columns">要更新的列集合</param>
        public void Update(object poco,params string[] columns)
        {
            if (poco == null)
                throw new ArgumentNullException("输入的参数 poco 不能为null");
            Type type = poco.GetType();
            object[] tna = type.GetCustomAttributes(typeof(TableNameAttribute), false);
            if (tna == null || tna.Length < 1)
                throw new Exception("poco实体未找到 TableName 特性");
            object[] pkn = type.GetCustomAttributes(typeof(PrimaryKeyAttribute), false);
            if (pkn == null || pkn.Length < 1)
                throw new Exception("poco实体未找到 PrimaryKey 特性");

            ExecuteUpdate((tna[0] as TableNameAttribute).Value, (pkn[0] as PrimaryKeyAttribute).Value, poco, null, columns);
        }
        /// <summary>
        /// 实体生成更新语句
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="primaryKeyName">主键名称</param>
        /// <param name="poco">poco实体</param>
        /// <param name="primaryKeyValue">主键值</param>
        /// <param name="columns">更新的列名</param>
        private void ExecuteUpdate(string tableName, string primaryKeyName, object poco, object primaryKeyValue, params string[] columns)
        {
            try
            {
                var sb = new StringBuilder();
                var index = 0;
                var paras = new List<object>();
                if (columns == null || columns.Length < 1)
                {
                    Type type = poco.GetType();
                    foreach (PropertyInfo pi in type.Properties())
                    {
                        // Don't update result columns
                        if (pi.GetCustomAttributes(typeof(ResultColumnAttribute), false).Length > 0)
                            continue;

                        // Don't update the primary key, but grab the value if we don't have it
                        if (string.Compare(pi.Name, primaryKeyName, true) == 0)
                        {
                            if (primaryKeyValue == null)
                                primaryKeyValue = poco.GetPropertyValue(pi.Name);
                            continue;
                        }

                        // Build the sql
                        if (index > 0)
                            sb.Append(", ");
                        sb.AppendFormat("{0} = {1}{2}", RuleProvider.Rule(_dbType).EscapeSqlIdentifier(pi.Name), "@", index++);

                        // Store the parameter in the command
                        paras.Add(poco.GetPropertyValue(pi.Name));
                    }
                }
                else
                {
                    foreach (var colname in columns)
                    {
                        // Build the sql
                        if (index > 0)
                            sb.Append(", ");
                        sb.AppendFormat("{0} = {1}{2}", RuleProvider.Rule(_dbType).EscapeSqlIdentifier(colname), "@", index++);

                        // Store the parameter in the command
                        paras.Add(poco.GetPropertyValue(colname));
                    }

                    // Grab primary key value
                    if (primaryKeyValue == null)
                    {
                        primaryKeyValue = poco.GetPropertyValue(primaryKeyName);
                    }
                }

                //添加主键参数
                paras.Add(primaryKeyValue);

                Sql = new Sql(string.Format("UPDATE {0} SET {1} WHERE {2} = {3}{4}",
                     RuleProvider.Rule(_dbType).EscapeTableName(tableName), sb.ToString(), RuleProvider.Rule(_dbType).EscapeSqlIdentifier(primaryKeyName), "@", index++), paras.ToArray());
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        /// <summary>
        /// 实体生成删除语句
        /// </summary>
        /// <param name="poco">poco实体</param>
        public void Delete(object poco)
        {
            if (poco == null)
                throw new ArgumentNullException("输入的参数 poco 不能为null");
            Type type = poco.GetType();

            object[] tna = type.GetCustomAttributes(typeof(TableNameAttribute), false);
            if (tna == null || tna.Length < 1)
                throw new Exception("poco实体未找到 TableName 特性");
            string tableName = (tna[0] as TableNameAttribute).Value;

            object[] pkn = type.GetCustomAttributes(typeof(PrimaryKeyAttribute), false);
            if (pkn == null || pkn.Length < 1)
                throw new Exception("poco实体未找到 PrimaryKey 特性");
            string primaryName = (pkn[0] as PrimaryKeyAttribute).Value;

            Delete(tableName, primaryName, poco.GetPropertyValue(primaryName));
        }
        /// <summary>
        /// 实体生成删除语句
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="primaryKeyName">主键名称</param>
        /// <param name="primaryKeyValue">主键值</param>
        private void Delete(string tableName, string primaryKeyName, object primaryKeyValue)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException("参数 tableName 不能为null");
            if (string.IsNullOrEmpty(primaryKeyName))
                throw new ArgumentNullException("参数 primaryKeyName 不能为null");
            if (primaryKeyValue == null)
                throw new ArgumentNullException("参数 primaryKeyValue 不能为null");

            // Do it
            Sql = new Sql(string.Format("DELETE FROM {0} WHERE {1}=@0", RuleProvider.Rule(_dbType).EscapeTableName(tableName), RuleProvider.Rule(_dbType).EscapeSqlIdentifier(primaryKeyName)), primaryKeyValue);
        }
        #endregion
    }

    [Serializable]
    public struct StrSql
    {
        /// <summary>
        /// Sql语句
        /// </summary>
        public string Sql { get; private set; }
        /// <summary>
        /// 参数集合
        /// </summary>
        public object[] Args { get; private set; }

        public StrSql(string s,object[] a)
        {
            Sql = s;
            Args = a;
        }
        /// <summary>
        /// 清除当前值
        /// </summary>
        public void Clear()
        {
            Sql = null;
            Args = null;
        }
    }
}
