/*
*   描述：SqlAttr接口
*                   被用于实体表属性，用于设置一些参数
*               
*   作者：Simon
*   时间：2017.06.12
*/

namespace Rc.PetaPoco
{
    public interface ISqlAttr
    {
        /// <summary>
        /// 设置表信息
        ///     链式赋值写法
        /// </summary>
        /// <param name="tblAlias">表别名</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns>当前赋值对象</returns>
        ISqlAttr SetTblInfo(string tblAlias, DataBaseType dbType);
    }
}
