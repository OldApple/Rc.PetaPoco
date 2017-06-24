/*
*   描述：条件描述器
*               
*   作者：Simon
*   时间：2017.06.14
*/
using System;
using System.Text;

namespace Rc.PetaPoco
{
    public class Condition
    {
        #region Fields/Properties
        string _con;
        Condition _rhs;//右侧追加内容
        string _conFinal;
        /// <summary>
        /// 最终结果
        /// </summary>
        internal string Result
        {
            get
            {
                Build();
                return _conFinal;
            }
        }
        #endregion

        #region Constructor
        private Condition()
        {

        }
        internal Condition(string con)
        {
            _con = con;
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// 构造器
        /// </summary>
        public static Condition Builder
        {
            get { return new Condition(); }
        }
        /// <summary>
        /// 判定当前条件是否指定开头
        /// </summary>
        /// <param name="con">当前条件</param>
        /// <param name="contype">条件开头字符串</param>
        /// <returns>true 是；false 否</returns>
        private static bool Is(Condition con, string contype)
        {
            return con != null && con._con != null && con._con.StartsWith(contype, StringComparison.InvariantCultureIgnoreCase);
        }
        /// <summary>
        /// 字符串类型转Condition类型
        /// </summary>
        /// <param name="con">字符串</param>
        public static implicit operator string(Condition con)
        {
            return con.Result;
        }
        /// <summary>
        /// 重载 & 运算符
        ///     返回Sql AND 语句
        /// </summary>
        /// <param name="c1">条件1</param>
        /// <param name="c2">条件2</param>
        /// <returns>AND语句</returns>
        public static Condition operator & (Condition c1,Condition c2)
        {
            return new Condition("(" + c1.Result + ")" + " AND " + "(" + c2.Result + ")");
        }
        /// <summary>
        /// 重载 | 运算符
        ///     返回Sql OR 语句
        /// </summary>
        /// <param name="c1">条件1</param>
        /// <param name="c2">条件2</param>
        /// <returns>OR语句</returns>
        public static Condition operator | (Condition c1,Condition c2)
        {
            return new Condition("(" + c1.Result + ")" + " OR " + "(" + c2.Result + ")");
        }
        #endregion

        #region Methods
        /// <summary>
        /// 编译条件
        /// </summary>
        private void Build()
        {
            // already built?
            if (_conFinal != null)
                return;

            // Build it
            var sb = new StringBuilder();
            Build(sb, null);
            _conFinal = sb.ToString();
        }
        /// <summary>
        /// 编译条件
        /// </summary>
        /// <param name="sb">字符串构造器</param>
        /// <param name="lhs">左侧条件</param>
        private void Build(StringBuilder sb, Condition lhs)
        {
            if (!string.IsNullOrEmpty(_con))
            {
                // 追加当前条件到字符串
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                }
                sb.Append(_con);
            }

            // 扩展右侧条件
            if (_rhs != null)
                _rhs.Build(sb, this);
        }
        /// <summary>
        /// 追加条件
        /// </summary>
        /// <param name="con">被追加的条件</param>
        /// <returns>当前条件</returns>
        private Condition Append(Condition con)
        {
            if (_rhs != null)
                _rhs.Append(con);
            else
                _rhs = con;

            _conFinal = null;
            return this;
        }
        /// <summary>
        /// 追加字符串条件
        /// </summary>
        /// <param name="con">被追加的字符串条件</param>
        /// <returns>当前条件</returns>
        internal Condition Append(string con)
        {
            return Append(new Condition(con));
        }
        /// <summary>
        /// 转字符串
        /// </summary>
        /// <returns>最终字符串</returns>
        public override string ToString()
        {
            return Result;
        }
        #endregion
    }
}
