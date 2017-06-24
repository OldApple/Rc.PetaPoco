/*
*   描述：规则提供器
*               
*   作者：Simon
*   时间：2017.06.14
*/

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Rc.PetaPoco
{
    public class RuleProvider
    {
        static SortedDictionary<DataBaseType, IRule> _rules;
        /// <summary>
        /// 数据库类型规则
        /// </summary>
        public static IRule Rule(DataBaseType type)
        {
            return _rules[type];
        }

        static RuleProvider()
        {
            _rules = new SortedDictionary<DataBaseType, IRule>();

            //规则适配器赋值
            Type[] ts = typeof(RuleProvider).Assembly.GetTypes();
            foreach (Type item in ts)
            {
                if (typeof(IRule).IsAssignableFrom(item) && item != typeof(IRule))
                {
                    IRule rule = (IRule)Activator.CreateInstance(item);
                    _rules.Add(rule.DbType, rule);
                }
            }
        }
    }
}
