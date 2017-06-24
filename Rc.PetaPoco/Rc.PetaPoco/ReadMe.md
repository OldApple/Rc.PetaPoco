基于开源orm框架 PetaPoco.5.1.259
				1.修改了命名空间 PetaPoco -> Rc.PetaPoco
				2.添加Sql类 partial 标识
				3.修改Sql类 隐藏 Set 方法（在分部类中自己定义了方法）
				3.OracleDatabaseProvider.EscapeSqlIdentifier 方法调整：return string.Format("\"{0}\"", sqlIdentifier.ToUpperInvariant());  ->  return string.Format("\"{0}\"", sqlIdentifier);
				4.OracleDatabaseProvider 重写 EscapeTableName 方法：
						public override string EscapeTableName(string tableName)
						{
							return EscapeSqlIdentifier(tableName.Split('.').LastOrDefault());
						}
				5.修改PetaPoco.Generator.ttinclude文件，命名空间修改using PetaPoco;  ->  using Rc.PetaPoco;  并在尾部第4行前添加：
		 public class SqlAttr : ISqlAttr
		{
		<#
		foreach(Column col in from c in tbl.Columns where !c.Ignore select c)
		{
				// Column bindings
		#>

			public readonly ColAttr <#=col.PropertyName #> = "<#=col.PropertyName #>";
		<# } #>

			public readonly ColAttr AllColumns = "*";

			public readonly TblAttr TableName = "<#=tbl.ClassName#>";

			public SqlAttr()
            {
                TableName.SetPrimaryKey("<#= tbl.PK==null?"":tbl.PK.Name#>");
            }

			public ISqlAttr SetTblInfo(string tblAlias, DataBaseType dbType)
			{
				TableName.SetAlias(tblAlias)
					.SetColAttrs(dbType, AllColumns, <#=string.Join(",",tbl.Columns.Select<Column,string>(c=>c.PropertyName)) #>);
				return this;
			}
		}
				6.修改PetaPoco.Core.ttinclude文件，删除最后一行空行。

注意：使用Oracle.ManagedDataAccess驱动时请注意在全局程序集中注册了Oracle.ManagedDataAccess
			* 或者添加Oracle.ManagedDataAccess程序集的引用来解决

使用方法：
        1.注册数据库操作类服务 DIContainer.Register(tt => new Database("DefaultConnection")).As<Database>().SingleInstance();
		2.获取数据库操作服务，如下：
		/// <summary>
        /// 数据库操作对象
        /// </summary>
        public static Database Database 
        { 
            get 
            {
                if (_repo == null)
                {
                    if (!DIContainer.IsRegistered<Database>())
                    {
                        throw new Exception(string.Format("未在 DIContainer 中初始化 实例 {0} ", "PetaPoco.Database"));
                    }
                    _repo = DIContainer.Resolve<Database>();
                }
                return _repo;
            }
        }