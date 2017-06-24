/*
*   描述：各数据库规则接口
*               
*   作者：Simon
*   时间：2017.06.14
*/

namespace Rc.PetaPoco
{
    public interface IRule
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        DataBaseType DbType { get; }

        /// <summary>
        /// 对表名进行处理
        ///     实体名可能携带用户、架构等信息
        /// </summary>
        /// <param name="tableName">The name of the table (as specified by the client program, or as attributes on the associated POCO class.</param>
        /// <returns>The escaped table name</returns>
        string EscapeTableName(string tableName);

        /// <summary>
        /// 表名、列名处理
        ///     如 Oracle 加 ""，SqlServer加 [] 等      
        /// </summary>
        /// <param name="sqlIdentifier">表名/列名等</param>
        /// <returns>The escaped identifier</returns>
        string EscapeSqlIdentifier(string sqlIdentifier);

        /// <summary>
        /// 获取参数前缀
        /// </summary>
        /// <returns>参数前缀</returns>
        string GetParameterPrefix();
    }
}
