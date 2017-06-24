/*
*   描述：表属性及操作
*               
*   作者：Simon
*   时间：2017.06.12
*/

namespace Rc.PetaPoco
{
    public class TblAttr
    {
        #region Fields/Properites
        /// <summary>
        /// 表名称
        /// </summary>
        string _name;
        /// <summary>
        /// 表别名
        /// </summary>
        string _alias;
        /// <summary>
        /// 表主键名称
        /// </summary>
        string _primaryKeyName;
        /// <summary>
        /// 数据库类型
        /// </summary>
        DataBaseType _dbType;
        #endregion

        #region Constructor
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">表名称</param>
        /// <param name="alias">表别名</param>
        private TblAttr(string name,string alias)
        {
            _name = name;
            _alias = alias;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">表名称</param>
        public TblAttr(string name)
            : this(name, "")
        {

        }
        #endregion

        #region Operator
        /// <summary>
        /// 字符串构造函数
        /// </summary>
        /// <param name="name">列名字符串</param>
        public static implicit operator TblAttr(string name)
        {
            return new TblAttr(name);
        }
        /// <summary>
        /// 类型转字符串
        /// </summary>
        /// <param name="tbl">表属性对象</param>
        public static implicit operator string(TblAttr tbl)
        {
            return tbl.ToString();
        }
        #endregion

        #region Methods
        /// <summary>
        /// 设置表别名
        /// </summary>
        /// <param name="alias">表别名</param>
        /// <returns>表属性对象</returns>
        public TblAttr SetAlias(string alias)
        {
            _alias = alias;
            return this;
        }
        /// <summary>
        /// 关联表列属性
        ///     需先设置别名后进行列属性关联
        /// </summary>
        /// <param name="cols">列属性集合</param>
        /// <returns>表属性对象</returns>
        public TblAttr SetColAttrs(DataBaseType dbType,params ColAttr[] cols)
        {
            _dbType = dbType;
            string tblAlias = string.IsNullOrEmpty(_alias) ? _name : _alias;

            foreach (ColAttr item in cols)
            {
                item.SetTblAlias(tblAlias).SetDbType(dbType);
            }
            return this;
        }
        /// <summary>
        /// 设置主键名称
        /// </summary>
        /// <param name="keyName">主键名称</param>
        /// <returns>表属性对象</returns>
        public TblAttr SetPrimaryKey(string keyName)
        {
            _primaryKeyName = keyName;
            return this;
        }
        public override string ToString()
        {
            return string.IsNullOrEmpty(_alias) ? RuleProvider.Rule(_dbType).EscapeSqlIdentifier(_name) : RuleProvider.Rule(_dbType).EscapeSqlIdentifier(_name) + " " + _alias;
        }
        #endregion
    }
}
