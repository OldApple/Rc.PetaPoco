/*
*   描述：SqlServer规则
*               
*   作者：Simon
*   时间：2017.06.14
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.PetaPoco
{
    internal class SqlServerRule : IRule
    {
        public DataBaseType DbType
        {
            get
            {
                return DataBaseType.SqlServer;
            }
        }

        public string EscapeTableName(string tableName)
        {
            //return tableName.IndexOf('.') >= 0 ? tableName : EscapeSqlIdentifier(tableName);
            return EscapeSqlIdentifier(tableName.Split('.').LastOrDefault());
        }

        /// <summary>
        ///     Escape and arbitary SQL identifier into a format suitable for the associated database provider
        /// </summary>
        /// <param name="sqlIdentifier">The SQL identifier to be escaped</param>
        /// <returns>The escaped identifier</returns>
        public string EscapeSqlIdentifier(string sqlIdentifier)
        {
            return string.Format("[{0}]", sqlIdentifier);
        }

        /// <summary>
        ///     Returns the prefix used to delimit parameters in SQL query strings.
        /// </summary>
        /// <returns>The providers character for prefixing a query parameter.</returns>
        public string GetParameterPrefix()
        {
            return "@";
        }
    }
}
