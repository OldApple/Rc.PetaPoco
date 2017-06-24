/*
*   描述：Oracle规则
*               
*   作者：Simon
*   时间：2017.06.14
*/
using System;
using System.Linq;

namespace Rc.PetaPoco
{
    internal class OracleRule : IRule
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataBaseType DbType
        {
            get
            {
                return DataBaseType.Oracle;
            }
        }

        public string EscapeTableName(string tableName)
        {
            return EscapeSqlIdentifier(tableName.Split('.').LastOrDefault());
        }

        public string EscapeSqlIdentifier(string sqlIdentifier)
        {
            return string.Format("\"{0}\"", sqlIdentifier);
        }

        public string GetParameterPrefix()
        {
            return ":";
        }
    }
}
