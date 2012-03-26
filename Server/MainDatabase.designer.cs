﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MMO3D.Server
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
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="MMO3D")]
	public partial class MainDatabaseDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public MainDatabaseDataContext() : 
				base(global::MMO3D.Server.Properties.Settings.Default.MMO3DConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public MainDatabaseDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MainDatabaseDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MainDatabaseDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MainDatabaseDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<BlockedAddress> BlockedAddresses
		{
			get
			{
				return this.GetTable<BlockedAddress>();
			}
		}
		
		public System.Data.Linq.Table<Player> Players
		{
			get
			{
				return this.GetTable<Player>();
			}
		}
		
		public System.Data.Linq.Table<FilteredWord> FilteredWords
		{
			get
			{
				return this.GetTable<FilteredWord>();
			}
		}
	}
	
	[Table(Name="dbo.BlockedAddresses")]
	public partial class BlockedAddress
	{
		
		private long _Address;
		
		public BlockedAddress()
		{
		}
		
		[Column(Storage="_Address", DbType="BigInt NOT NULL")]
		public long Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				if ((this._Address != value))
				{
					this._Address = value;
				}
			}
		}
	}
	
	[Table(Name="dbo.Players")]
	public partial class Player
	{
		
		private long _ID;
		
		private string _UserName;
		
		private string _Password;
		
		private int _UserLevel;
		
		private int _DisableReason;
		
		private bool _LoggedIn;
		
		private long _LastLogOnAttempt;
		
		private long _LastLogOnSuccess;
		
		private long _LastLogOnAttemptIP;
		
		private long _LastLogOnSuccessIP;
		
		private int _LogOnAttempts;
		
		private float _PositionX;
		
		private float _PositionY;
		
		private float _PositionZ;
		
		private float _RotationX;
		
		private float _RotationY;
		
		private float _RotationZ;
		
		public Player()
		{
		}
		
		[Column(Storage="_ID", AutoSync=AutoSync.Always, DbType="BigInt NOT NULL IDENTITY", IsDbGenerated=true)]
		public long ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this._ID = value;
				}
			}
		}
		
		[Column(Storage="_UserName", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				if ((this._UserName != value))
				{
					this._UserName = value;
				}
			}
		}
		
		[Column(Storage="_Password", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this._Password = value;
				}
			}
		}
		
		[Column(Storage="_UserLevel", DbType="Int NOT NULL")]
		public int UserLevel
		{
			get
			{
				return this._UserLevel;
			}
			set
			{
				if ((this._UserLevel != value))
				{
					this._UserLevel = value;
				}
			}
		}
		
		[Column(Storage="_DisableReason", DbType="Int NOT NULL")]
		public int DisableReason
		{
			get
			{
				return this._DisableReason;
			}
			set
			{
				if ((this._DisableReason != value))
				{
					this._DisableReason = value;
				}
			}
		}
		
		[Column(Storage="_LoggedIn", DbType="Bit NOT NULL")]
		public bool LoggedIn
		{
			get
			{
				return this._LoggedIn;
			}
			set
			{
				if ((this._LoggedIn != value))
				{
					this._LoggedIn = value;
				}
			}
		}
		
		[Column(Storage="_LastLogOnAttempt", DbType="BigInt NOT NULL")]
		public long LastLogOnAttempt
		{
			get
			{
				return this._LastLogOnAttempt;
			}
			set
			{
				if ((this._LastLogOnAttempt != value))
				{
					this._LastLogOnAttempt = value;
				}
			}
		}
		
		[Column(Storage="_LastLogOnSuccess", DbType="BigInt NOT NULL")]
		public long LastLogOnSuccess
		{
			get
			{
				return this._LastLogOnSuccess;
			}
			set
			{
				if ((this._LastLogOnSuccess != value))
				{
					this._LastLogOnSuccess = value;
				}
			}
		}
		
		[Column(Storage="_LastLogOnAttemptIP", DbType="BigInt NOT NULL")]
		public long LastLogOnAttemptIP
		{
			get
			{
				return this._LastLogOnAttemptIP;
			}
			set
			{
				if ((this._LastLogOnAttemptIP != value))
				{
					this._LastLogOnAttemptIP = value;
				}
			}
		}
		
		[Column(Storage="_LastLogOnSuccessIP", DbType="BigInt NOT NULL")]
		public long LastLogOnSuccessIP
		{
			get
			{
				return this._LastLogOnSuccessIP;
			}
			set
			{
				if ((this._LastLogOnSuccessIP != value))
				{
					this._LastLogOnSuccessIP = value;
				}
			}
		}
		
		[Column(Storage="_LogOnAttempts", DbType="Int NOT NULL")]
		public int LogOnAttempts
		{
			get
			{
				return this._LogOnAttempts;
			}
			set
			{
				if ((this._LogOnAttempts != value))
				{
					this._LogOnAttempts = value;
				}
			}
		}
		
		[Column(Storage="_PositionX", DbType="Real NOT NULL")]
		public float PositionX
		{
			get
			{
				return this._PositionX;
			}
			set
			{
				if ((this._PositionX != value))
				{
					this._PositionX = value;
				}
			}
		}
		
		[Column(Storage="_PositionY", DbType="Real NOT NULL")]
		public float PositionY
		{
			get
			{
				return this._PositionY;
			}
			set
			{
				if ((this._PositionY != value))
				{
					this._PositionY = value;
				}
			}
		}
		
		[Column(Storage="_PositionZ", DbType="Real NOT NULL")]
		public float PositionZ
		{
			get
			{
				return this._PositionZ;
			}
			set
			{
				if ((this._PositionZ != value))
				{
					this._PositionZ = value;
				}
			}
		}
		
		[Column(Storage="_RotationX", DbType="Real NOT NULL")]
		public float RotationX
		{
			get
			{
				return this._RotationX;
			}
			set
			{
				if ((this._RotationX != value))
				{
					this._RotationX = value;
				}
			}
		}
		
		[Column(Storage="_RotationY", DbType="Real NOT NULL")]
		public float RotationY
		{
			get
			{
				return this._RotationY;
			}
			set
			{
				if ((this._RotationY != value))
				{
					this._RotationY = value;
				}
			}
		}
		
		[Column(Storage="_RotationZ", DbType="Real NOT NULL")]
		public float RotationZ
		{
			get
			{
				return this._RotationZ;
			}
			set
			{
				if ((this._RotationZ != value))
				{
					this._RotationZ = value;
				}
			}
		}
	}
	
	[Table(Name="dbo.FilteredWords")]
	public partial class FilteredWord
	{
		
		private string _Word;
		
		public FilteredWord()
		{
		}
		
		[Column(Storage="_Word", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Word
		{
			get
			{
				return this._Word;
			}
			set
			{
				if ((this._Word != value))
				{
					this._Word = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
