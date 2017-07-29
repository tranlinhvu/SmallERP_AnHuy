﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IMS
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="SmallERP_HuongTrinh")]
	public partial class IMS_ReportDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public IMS_ReportDataContext() : 
				base(global::IMS.Properties.Settings.Default.SmallERPConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public IMS_ReportDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public IMS_ReportDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public IMS_ReportDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public IMS_ReportDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<View_IOR> View_IORs
		{
			get
			{
				return this.GetTable<View_IOR>();
			}
		}
		
		public System.Data.Linq.Table<View_IOR_Kind> View_IOR_Kinds
		{
			get
			{
				return this.GetTable<View_IOR_Kind>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.View_IOR")]
	public partial class View_IOR
	{
		
		private string _Code;
		
		private string _ProductName;
		
		private System.Nullable<double> _Available;
		
		private System.Nullable<double> _QuantityIn;
		
		private System.Nullable<double> _QuantityOut;
		
		private System.Nullable<int> _RollNoIn;
		
		private System.Nullable<int> _RollNoOut;
		
		public View_IOR()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Code", DbType="NVarChar(50)")]
		public string Code
		{
			get
			{
				return this._Code;
			}
			set
			{
				if ((this._Code != value))
				{
					this._Code = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProductName", DbType="NVarChar(50)")]
		public string ProductName
		{
			get
			{
				return this._ProductName;
			}
			set
			{
				if ((this._ProductName != value))
				{
					this._ProductName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Available", DbType="Float")]
		public System.Nullable<double> Available
		{
			get
			{
				return this._Available;
			}
			set
			{
				if ((this._Available != value))
				{
					this._Available = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_QuantityIn", DbType="Float")]
		public System.Nullable<double> QuantityIn
		{
			get
			{
				return this._QuantityIn;
			}
			set
			{
				if ((this._QuantityIn != value))
				{
					this._QuantityIn = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_QuantityOut", DbType="Float")]
		public System.Nullable<double> QuantityOut
		{
			get
			{
				return this._QuantityOut;
			}
			set
			{
				if ((this._QuantityOut != value))
				{
					this._QuantityOut = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RollNoIn", DbType="Int")]
		public System.Nullable<int> RollNoIn
		{
			get
			{
				return this._RollNoIn;
			}
			set
			{
				if ((this._RollNoIn != value))
				{
					this._RollNoIn = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RollNoOut", DbType="Int")]
		public System.Nullable<int> RollNoOut
		{
			get
			{
				return this._RollNoOut;
			}
			set
			{
				if ((this._RollNoOut != value))
				{
					this._RollNoOut = value;
				}
			}
		}

        public int RowNumber { get; internal set; }
        public int? AvailableRollNo { get; internal set; }
    }
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.View_IOR_Kind")]
	public partial class View_IOR_Kind
	{
		
		private string _Code;
		
		private string _KindName;
		
		private System.Nullable<double> _Available;
		
		private System.Nullable<double> _QuantityIn;
		
		private System.Nullable<double> _QuantityOut;
		
		private System.Nullable<int> _RollNoIn;
		
		private System.Nullable<int> _RollNoOut;
		
		public View_IOR_Kind()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Code", DbType="NVarChar(5)")]
		public string Code
		{
			get
			{
				return this._Code;
			}
			set
			{
				if ((this._Code != value))
				{
					this._Code = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_KindName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string KindName
		{
			get
			{
				return this._KindName;
			}
			set
			{
				if ((this._KindName != value))
				{
					this._KindName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Available", DbType="Float")]
		public System.Nullable<double> Available
		{
			get
			{
				return this._Available;
			}
			set
			{
				if ((this._Available != value))
				{
					this._Available = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_QuantityIn", DbType="Float")]
		public System.Nullable<double> QuantityIn
		{
			get
			{
				return this._QuantityIn;
			}
			set
			{
				if ((this._QuantityIn != value))
				{
					this._QuantityIn = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_QuantityOut", DbType="Float")]
		public System.Nullable<double> QuantityOut
		{
			get
			{
				return this._QuantityOut;
			}
			set
			{
				if ((this._QuantityOut != value))
				{
					this._QuantityOut = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RollNoIn", DbType="Int")]
		public System.Nullable<int> RollNoIn
		{
			get
			{
				return this._RollNoIn;
			}
			set
			{
				if ((this._RollNoIn != value))
				{
					this._RollNoIn = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RollNoOut", DbType="Int")]
		public System.Nullable<int> RollNoOut
		{
			get
			{
				return this._RollNoOut;
			}
			set
			{
				if ((this._RollNoOut != value))
				{
					this._RollNoOut = value;
				}
			}
		}

        public int RowNumber { get; internal set; }
        public int? AvailableRollNo { get; internal set; }
    }
}
#pragma warning restore 1591
