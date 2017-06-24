/*
*   描述：新增、修改、删除 Sql描述器
*               
*   作者：Simon
*   时间：2017.06.15
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Fasterflect;

namespace Rc.PetaPoco
{
    public class CudSql
    {
        DataBaseType _dbType;

        public static CudSql Builder
        {
            get
            {
                return new CudSql();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        public CudSql(DataBaseType dbType)
        {
            _dbType = dbType;
        }
        /// <summary>
        /// 默认构造函数
        ///     默认数据库类型 Oracle
        /// </summary>
        public CudSql()
            : this(DataBaseType.Oracle)
        {

        }
        /// <summary>
        /// 设置数据库类型
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns>当前Sql描述器</returns>
        internal CudSql SetDbType(DataBaseType dbType)
        {
            _dbType = dbType;
            return this;
        }
        /// <summary>
        /// 自由拼接删除语句
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns>Sql描述器</returns>
        public Sql Delete(string tableName)
        {
            return new Sql(string.Format("DELETE FROM {0} ", tableName));
        }
        public Sql Update(string tableName)
        {
            return new Sql(string.Format("UPDATE {0} ", tableName));
        }
    }
}
