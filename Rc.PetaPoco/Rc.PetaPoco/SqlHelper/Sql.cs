using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.PetaPoco
{
    public partial class Sql
    {
        /// <summary>
        /// 设置列
        /// </summary>
        /// <param name="cols">列集合</param>
        /// <returns></returns>
        public SqlSetClause Set(params object[] cols)
        {
            if (cols == null || cols.Length < 1)
                throw new ArgumentNullException("输入参数 cols 不能为空");

            return new SqlSetClause(Append(new Sql("SET ")), cols);
        }

        public class SqlSetClause
        {
            readonly Sql _sql;
            readonly object[] _cols;

            public SqlSetClause(Sql sql,object[] cols)
            {
                _sql = sql;
                _cols = cols;
            }

            public Sql Args(params object [] args)
            {
                if (args == null || args.Length < 1)
                    throw new ArgumentNullException("输入参数 args 不能为空");
                if (_cols.Length != args.Length)
                    throw new Exception(string.Format("输入列数量{0} 参数数量{1} 不匹配！", _cols.Length, args.Length));

                string r = string.Empty;
                for (int i = 0; i < _cols.Length; i++)
                {
                    if (string.IsNullOrEmpty(r))
                        r += _cols[i].ToString() + "=@" + i;
                    else
                        r += "," + _cols[i].ToString() + "=@" + i;
                }
                return _sql.Append(r, args);
            }
        }
    }
}
