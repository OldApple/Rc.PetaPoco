/*
*   描述：列属性及操作
*               
*   作者：Simon
*   时间：2017.06.09
*/

namespace Rc.PetaPoco
{
    public class ColAttr
    {
        #region Fields/Properties
        /// <summary>
        /// 列名
        /// </summary>
        string _name;
        /// <summary>
        /// 表别名
        ///     实体列此值不为空
        /// </summary>
        string _tblAlias = string.Empty;
        /// <summary>
        /// 数据库类型
        /// </summary>
        DataBaseType _dbType;
        #endregion

        #region Constructor
        private ColAttr(string name)
        {
            _name = name;
        }
        #endregion

        #region Operator
        /// <summary>
        /// 字符串构造函数
        /// </summary>
        /// <param name="name">列名字符串</param>
        public static implicit operator ColAttr(string name)
        {
            return new ColAttr(name);
        }
        /// <summary>
        /// 类型转字符串
        /// </summary>
        /// <param name="col">列属性对象</param>
        public static implicit operator string(ColAttr col)
        {
            return col.ToString();
        }
        /// <summary>
        /// 重载 == 运算符
        ///     返回Sql = 语句
        /// </summary>
        /// <param name="c1">列1</param>
        /// <param name="c2">列2</param>
        /// <returns>等于条件</returns>
        public static Condition operator == (ColAttr c1,ColAttr c2)
        {
            return new Condition(c1.ToString() + " = " + c2.ToString());
        }
        /// <summary>
        /// 重载 != 运算符
        ///     返回Sql <> 语句
        /// </summary>
        /// <param name="c1">列1</param>
        /// <param name="c2">列2</param>
        /// <returns>不等于条件</returns>
        public static Condition operator != (ColAttr c1,ColAttr c2)
        {
            return new Condition(c1.ToString() + "<>" + c2.ToString());
        }
        /// <summary>
        /// 重载 > 运算符
        ///     返回Sql > 语句
        /// </summary>
        /// <param name="c1">列1</param>
        /// <param name="c2">列2</param>
        /// <returns>大于条件</returns>
        public static Condition operator > (ColAttr c1,ColAttr c2)
        {
            return new Condition(c1.ToString() + " > " + c2.ToString());
        }
        /// <summary>
        /// 重载 < 运算符
        ///     返回Sql < 语句
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns>小于条件</returns>
        public static Condition operator < (ColAttr c1,ColAttr c2)
        {
            return new Condition(c1.ToString() + " < " + c2.ToString());
        }
        /// <summary>
        /// 重载 >= 运算符
        ///     返回Sql >= 语句
        /// </summary>
        /// <param name="c1">列1</param>
        /// <param name="c2">列2</param>
        /// <returns>大于等于条件</returns>
        public static Condition operator >= (ColAttr c1,ColAttr c2)
        {
            return new Condition(c1.ToString() + " >= " + c2.ToString());
        }
        /// <summary>
        /// 重载 <= 运算符
        ///     返回Sql <= 语句
        /// </summary>
        /// <param name="c1">列1</param>
        /// <param name="c2">列2</param>
        /// <returns>小于等于条件</returns>
        public static Condition operator <= (ColAttr c1,ColAttr c2)
        {
            return new Condition(c1.ToString() + " <= " + c2.ToString());
        }
        /// <summary>
        /// 重载 % 运算符
        ///     返回Sql LIKE 语句
        /// </summary>
        /// <param name="c1">列1</param>
        /// <param name="c2">列2</param>
        /// <returns>模糊查询条件</returns>
        public static Condition operator % (ColAttr c1,ColAttr c2)
        {
            return new Condition(c1.ToString() + " LIKE " + c2.ToString());
        }
        #endregion

        #region Methods
        /// <summary>
        /// 设置表别名
        /// </summary>
        /// <param name="tblAlias">表别名</param>
        /// <returns>当前对象</returns>
        public ColAttr SetTblAlias(string tblAlias)
        {
            _tblAlias = tblAlias;
            return this;
        }
        /// <summary>
        /// 设置数据库类型
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns>当前对象</returns>
        public ColAttr SetDbType(DataBaseType dbType)
        {
            _dbType = dbType;
            return this;
        }
        /// <summary>
        /// 别名显示
        ///     Select 语句中是用
        /// </summary>
        /// <param name="alias">别名</param>
        /// <returns>别名字符串</returns>
        public string Alias(string alias)
        {
            return ToString() + " as " + alias;
        }
        /// <summary>
        /// 转字符串
        /// </summary>
        /// <returns>构造的字符串</returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(_tblAlias))
            {
                //非实体列
                return _name;
            }
            else
            {
                //实体列
                if (_name == "*")
                    return _tblAlias + "." + _name;//虚拟所有列
                else
                    return _tblAlias + "." + RuleProvider.Rule(_dbType).EscapeSqlIdentifier(_name);//真正的列
            }
        }
        public override bool Equals(object obj)
        {
            return false;
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        #endregion
    }
}
