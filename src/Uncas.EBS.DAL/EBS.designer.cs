﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3074
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Uncas.EBS.DAL
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
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="EBS")]
	public partial class EBSDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertIssue(Issue instance);
    partial void UpdateIssue(Issue instance);
    partial void DeleteIssue(Issue instance);
    partial void InsertTask(Task instance);
    partial void UpdateTask(Task instance);
    partial void DeleteTask(Task instance);
    partial void InsertProject(Project instance);
    partial void UpdateProject(Project instance);
    partial void DeleteProject(Project instance);
    partial void InsertStatus(Status instance);
    partial void UpdateStatus(Status instance);
    partial void DeleteStatus(Status instance);
    #endregion
		
		public EBSDataContext() : 
				base(global::Uncas.EBS.DAL.Properties.Settings.Default.EBSConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public EBSDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EBSDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EBSDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EBSDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Issue> Issues
		{
			get
			{
				return this.GetTable<Issue>();
			}
		}
		
		public System.Data.Linq.Table<Task> Tasks
		{
			get
			{
				return this.GetTable<Task>();
			}
		}
		
		public System.Data.Linq.Table<Project> Projects
		{
			get
			{
				return this.GetTable<Project>();
			}
		}
		
		public System.Data.Linq.Table<Status> Status
		{
			get
			{
				return this.GetTable<Status>();
			}
		}
	}
	
	[Table(Name="dbo.Issue")]
	public partial class Issue : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IssueId;
		
		private string _Title;
		
		private System.DateTime _CreatedDate;
		
		private int _RefProjectId;
		
		private int _RefStatusId;
		
		private int _Priority;
		
		private EntitySet<Task> _Tasks;
		
		private EntityRef<Project> _Project;
		
		private EntityRef<Status> _Status;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIssueIdChanging(int value);
    partial void OnIssueIdChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnCreatedDateChanging(System.DateTime value);
    partial void OnCreatedDateChanged();
    partial void OnRefProjectIdChanging(int value);
    partial void OnRefProjectIdChanged();
    partial void OnRefStatusIdChanging(int value);
    partial void OnRefStatusIdChanged();
    partial void OnPriorityChanging(int value);
    partial void OnPriorityChanged();
    #endregion
		
		public Issue()
		{
			this._Tasks = new EntitySet<Task>(new Action<Task>(this.attach_Tasks), new Action<Task>(this.detach_Tasks));
			this._Project = default(EntityRef<Project>);
			this._Status = default(EntityRef<Status>);
			OnCreated();
		}
		
		[Column(Storage="_IssueId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int IssueId
		{
			get
			{
				return this._IssueId;
			}
			set
			{
				if ((this._IssueId != value))
				{
					this.OnIssueIdChanging(value);
					this.SendPropertyChanging();
					this._IssueId = value;
					this.SendPropertyChanged("IssueId");
					this.OnIssueIdChanged();
				}
			}
		}
		
		[Column(Storage="_Title", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this.OnTitleChanging(value);
					this.SendPropertyChanging();
					this._Title = value;
					this.SendPropertyChanged("Title");
					this.OnTitleChanged();
				}
			}
		}
		
		[Column(Storage="_CreatedDate", DbType="DateTime NOT NULL")]
		public System.DateTime CreatedDate
		{
			get
			{
				return this._CreatedDate;
			}
			set
			{
				if ((this._CreatedDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._CreatedDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_RefProjectId", DbType="Int NOT NULL")]
		public int RefProjectId
		{
			get
			{
				return this._RefProjectId;
			}
			set
			{
				if ((this._RefProjectId != value))
				{
					if (this._Project.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRefProjectIdChanging(value);
					this.SendPropertyChanging();
					this._RefProjectId = value;
					this.SendPropertyChanged("RefProjectId");
					this.OnRefProjectIdChanged();
				}
			}
		}
		
		[Column(Storage="_RefStatusId", DbType="Int NOT NULL")]
		public int RefStatusId
		{
			get
			{
				return this._RefStatusId;
			}
			set
			{
				if ((this._RefStatusId != value))
				{
					if (this._Status.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRefStatusIdChanging(value);
					this.SendPropertyChanging();
					this._RefStatusId = value;
					this.SendPropertyChanged("RefStatusId");
					this.OnRefStatusIdChanged();
				}
			}
		}
		
		[Column(Storage="_Priority", DbType="Int NOT NULL")]
		public int Priority
		{
			get
			{
				return this._Priority;
			}
			set
			{
				if ((this._Priority != value))
				{
					this.OnPriorityChanging(value);
					this.SendPropertyChanging();
					this._Priority = value;
					this.SendPropertyChanged("Priority");
					this.OnPriorityChanged();
				}
			}
		}
		
		[Association(Name="Issue_Task", Storage="_Tasks", ThisKey="IssueId", OtherKey="RefIssueId")]
		public EntitySet<Task> Tasks
		{
			get
			{
				return this._Tasks;
			}
			set
			{
				this._Tasks.Assign(value);
			}
		}
		
		[Association(Name="Project_Issue", Storage="_Project", ThisKey="RefProjectId", OtherKey="ProjectId", IsForeignKey=true)]
		public Project Project
		{
			get
			{
				return this._Project.Entity;
			}
			set
			{
				Project previousValue = this._Project.Entity;
				if (((previousValue != value) 
							|| (this._Project.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Project.Entity = null;
						previousValue.Issues.Remove(this);
					}
					this._Project.Entity = value;
					if ((value != null))
					{
						value.Issues.Add(this);
						this._RefProjectId = value.ProjectId;
					}
					else
					{
						this._RefProjectId = default(int);
					}
					this.SendPropertyChanged("Project");
				}
			}
		}
		
		[Association(Name="Status_Issue", Storage="_Status", ThisKey="RefStatusId", OtherKey="StatusId", IsForeignKey=true)]
		public Status Status
		{
			get
			{
				return this._Status.Entity;
			}
			set
			{
				Status previousValue = this._Status.Entity;
				if (((previousValue != value) 
							|| (this._Status.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Status.Entity = null;
						previousValue.Issues.Remove(this);
					}
					this._Status.Entity = value;
					if ((value != null))
					{
						value.Issues.Add(this);
						this._RefStatusId = value.StatusId;
					}
					else
					{
						this._RefStatusId = default(int);
					}
					this.SendPropertyChanged("Status");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Tasks(Task entity)
		{
			this.SendPropertyChanging();
			entity.Issue = this;
		}
		
		private void detach_Tasks(Task entity)
		{
			this.SendPropertyChanging();
			entity.Issue = null;
		}
	}
	
	[Table(Name="dbo.Task")]
	public partial class Task : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _TaskId;
		
		private string _Description;
		
		private System.DateTime _CreatedDate;
		
		private int _RefIssueId;
		
		private int _RefStatusId;
		
		private int _Sequence;
		
		private System.Nullable<System.DateTime> _StartDate;
		
		private System.Nullable<System.DateTime> _EndDate;
		
		private decimal _OriginalEstimateInHours;
		
		private decimal _CurrentEstimateInHours;
		
		private decimal _ElapsedHours;
		
		private EntityRef<Issue> _Issue;
		
		private EntityRef<Status> _Status;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnTaskIdChanging(int value);
    partial void OnTaskIdChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnCreatedDateChanging(System.DateTime value);
    partial void OnCreatedDateChanged();
    partial void OnRefIssueIdChanging(int value);
    partial void OnRefIssueIdChanged();
    partial void OnRefStatusIdChanging(int value);
    partial void OnRefStatusIdChanged();
    partial void OnSequenceChanging(int value);
    partial void OnSequenceChanged();
    partial void OnStartDateChanging(System.Nullable<System.DateTime> value);
    partial void OnStartDateChanged();
    partial void OnEndDateChanging(System.Nullable<System.DateTime> value);
    partial void OnEndDateChanged();
    partial void OnOriginalEstimateInHoursChanging(decimal value);
    partial void OnOriginalEstimateInHoursChanged();
    partial void OnCurrentEstimateInHoursChanging(decimal value);
    partial void OnCurrentEstimateInHoursChanged();
    partial void OnElapsedHoursChanging(decimal value);
    partial void OnElapsedHoursChanged();
    #endregion
		
		public Task()
		{
			this._Issue = default(EntityRef<Issue>);
			this._Status = default(EntityRef<Status>);
			OnCreated();
		}
		
		[Column(Storage="_TaskId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int TaskId
		{
			get
			{
				return this._TaskId;
			}
			set
			{
				if ((this._TaskId != value))
				{
					this.OnTaskIdChanging(value);
					this.SendPropertyChanging();
					this._TaskId = value;
					this.SendPropertyChanged("TaskId");
					this.OnTaskIdChanged();
				}
			}
		}
		
		[Column(Storage="_Description", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_CreatedDate", DbType="DateTime NOT NULL")]
		public System.DateTime CreatedDate
		{
			get
			{
				return this._CreatedDate;
			}
			set
			{
				if ((this._CreatedDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._CreatedDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Column(Storage="_RefIssueId", DbType="Int NOT NULL")]
		public int RefIssueId
		{
			get
			{
				return this._RefIssueId;
			}
			set
			{
				if ((this._RefIssueId != value))
				{
					if (this._Issue.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRefIssueIdChanging(value);
					this.SendPropertyChanging();
					this._RefIssueId = value;
					this.SendPropertyChanged("RefIssueId");
					this.OnRefIssueIdChanged();
				}
			}
		}
		
		[Column(Storage="_RefStatusId", DbType="Int NOT NULL")]
		public int RefStatusId
		{
			get
			{
				return this._RefStatusId;
			}
			set
			{
				if ((this._RefStatusId != value))
				{
					if (this._Status.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRefStatusIdChanging(value);
					this.SendPropertyChanging();
					this._RefStatusId = value;
					this.SendPropertyChanged("RefStatusId");
					this.OnRefStatusIdChanged();
				}
			}
		}
		
		[Column(Storage="_Sequence", DbType="Int NOT NULL")]
		public int Sequence
		{
			get
			{
				return this._Sequence;
			}
			set
			{
				if ((this._Sequence != value))
				{
					this.OnSequenceChanging(value);
					this.SendPropertyChanging();
					this._Sequence = value;
					this.SendPropertyChanged("Sequence");
					this.OnSequenceChanged();
				}
			}
		}
		
		[Column(Storage="_StartDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> StartDate
		{
			get
			{
				return this._StartDate;
			}
			set
			{
				if ((this._StartDate != value))
				{
					this.OnStartDateChanging(value);
					this.SendPropertyChanging();
					this._StartDate = value;
					this.SendPropertyChanged("StartDate");
					this.OnStartDateChanged();
				}
			}
		}
		
		[Column(Storage="_EndDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> EndDate
		{
			get
			{
				return this._EndDate;
			}
			set
			{
				if ((this._EndDate != value))
				{
					this.OnEndDateChanging(value);
					this.SendPropertyChanging();
					this._EndDate = value;
					this.SendPropertyChanged("EndDate");
					this.OnEndDateChanged();
				}
			}
		}
		
		[Column(Storage="_OriginalEstimateInHours", DbType="Decimal(10,2) NOT NULL")]
		public decimal OriginalEstimateInHours
		{
			get
			{
				return this._OriginalEstimateInHours;
			}
			set
			{
				if ((this._OriginalEstimateInHours != value))
				{
					this.OnOriginalEstimateInHoursChanging(value);
					this.SendPropertyChanging();
					this._OriginalEstimateInHours = value;
					this.SendPropertyChanged("OriginalEstimateInHours");
					this.OnOriginalEstimateInHoursChanged();
				}
			}
		}
		
		[Column(Storage="_CurrentEstimateInHours", DbType="Decimal(10,2) NOT NULL")]
		public decimal CurrentEstimateInHours
		{
			get
			{
				return this._CurrentEstimateInHours;
			}
			set
			{
				if ((this._CurrentEstimateInHours != value))
				{
					this.OnCurrentEstimateInHoursChanging(value);
					this.SendPropertyChanging();
					this._CurrentEstimateInHours = value;
					this.SendPropertyChanged("CurrentEstimateInHours");
					this.OnCurrentEstimateInHoursChanged();
				}
			}
		}
		
		[Column(Storage="_ElapsedHours", DbType="Decimal(10,2) NOT NULL")]
		public decimal ElapsedHours
		{
			get
			{
				return this._ElapsedHours;
			}
			set
			{
				if ((this._ElapsedHours != value))
				{
					this.OnElapsedHoursChanging(value);
					this.SendPropertyChanging();
					this._ElapsedHours = value;
					this.SendPropertyChanged("ElapsedHours");
					this.OnElapsedHoursChanged();
				}
			}
		}
		
		[Association(Name="Issue_Task", Storage="_Issue", ThisKey="RefIssueId", OtherKey="IssueId", IsForeignKey=true)]
		public Issue Issue
		{
			get
			{
				return this._Issue.Entity;
			}
			set
			{
				Issue previousValue = this._Issue.Entity;
				if (((previousValue != value) 
							|| (this._Issue.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Issue.Entity = null;
						previousValue.Tasks.Remove(this);
					}
					this._Issue.Entity = value;
					if ((value != null))
					{
						value.Tasks.Add(this);
						this._RefIssueId = value.IssueId;
					}
					else
					{
						this._RefIssueId = default(int);
					}
					this.SendPropertyChanged("Issue");
				}
			}
		}
		
		[Association(Name="Status_Task", Storage="_Status", ThisKey="RefStatusId", OtherKey="StatusId", IsForeignKey=true)]
		public Status Status
		{
			get
			{
				return this._Status.Entity;
			}
			set
			{
				Status previousValue = this._Status.Entity;
				if (((previousValue != value) 
							|| (this._Status.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Status.Entity = null;
						previousValue.Tasks.Remove(this);
					}
					this._Status.Entity = value;
					if ((value != null))
					{
						value.Tasks.Add(this);
						this._RefStatusId = value.StatusId;
					}
					else
					{
						this._RefStatusId = default(int);
					}
					this.SendPropertyChanged("Status");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="dbo.Project")]
	public partial class Project : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ProjectId;
		
		private string _ProjectName;
		
		private System.DateTime _CreatedDate;
		
		private EntitySet<Issue> _Issues;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnProjectIdChanging(int value);
    partial void OnProjectIdChanged();
    partial void OnProjectNameChanging(string value);
    partial void OnProjectNameChanged();
    partial void OnCreatedDateChanging(System.DateTime value);
    partial void OnCreatedDateChanged();
    #endregion
		
		public Project()
		{
			this._Issues = new EntitySet<Issue>(new Action<Issue>(this.attach_Issues), new Action<Issue>(this.detach_Issues));
			OnCreated();
		}
		
		[Column(Storage="_ProjectId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ProjectId
		{
			get
			{
				return this._ProjectId;
			}
			set
			{
				if ((this._ProjectId != value))
				{
					this.OnProjectIdChanging(value);
					this.SendPropertyChanging();
					this._ProjectId = value;
					this.SendPropertyChanged("ProjectId");
					this.OnProjectIdChanged();
				}
			}
		}
		
		[Column(Storage="_ProjectName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string ProjectName
		{
			get
			{
				return this._ProjectName;
			}
			set
			{
				if ((this._ProjectName != value))
				{
					this.OnProjectNameChanging(value);
					this.SendPropertyChanging();
					this._ProjectName = value;
					this.SendPropertyChanged("ProjectName");
					this.OnProjectNameChanged();
				}
			}
		}
		
		[Column(Storage="_CreatedDate", DbType="DateTime NOT NULL")]
		public System.DateTime CreatedDate
		{
			get
			{
				return this._CreatedDate;
			}
			set
			{
				if ((this._CreatedDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._CreatedDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[Association(Name="Project_Issue", Storage="_Issues", ThisKey="ProjectId", OtherKey="RefProjectId")]
		public EntitySet<Issue> Issues
		{
			get
			{
				return this._Issues;
			}
			set
			{
				this._Issues.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Issues(Issue entity)
		{
			this.SendPropertyChanging();
			entity.Project = this;
		}
		
		private void detach_Issues(Issue entity)
		{
			this.SendPropertyChanging();
			entity.Project = null;
		}
	}
	
	[Table(Name="dbo.Status")]
	public partial class Status : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _StatusId;
		
		private string _StatusName;
		
		private EntitySet<Issue> _Issues;
		
		private EntitySet<Task> _Tasks;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnStatusIdChanging(int value);
    partial void OnStatusIdChanged();
    partial void OnStatusNameChanging(string value);
    partial void OnStatusNameChanged();
    #endregion
		
		public Status()
		{
			this._Issues = new EntitySet<Issue>(new Action<Issue>(this.attach_Issues), new Action<Issue>(this.detach_Issues));
			this._Tasks = new EntitySet<Task>(new Action<Task>(this.attach_Tasks), new Action<Task>(this.detach_Tasks));
			OnCreated();
		}
		
		[Column(Storage="_StatusId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int StatusId
		{
			get
			{
				return this._StatusId;
			}
			set
			{
				if ((this._StatusId != value))
				{
					this.OnStatusIdChanging(value);
					this.SendPropertyChanging();
					this._StatusId = value;
					this.SendPropertyChanged("StatusId");
					this.OnStatusIdChanged();
				}
			}
		}
		
		[Column(Storage="_StatusName", DbType="NVarChar(20) NOT NULL", CanBeNull=false)]
		public string StatusName
		{
			get
			{
				return this._StatusName;
			}
			set
			{
				if ((this._StatusName != value))
				{
					this.OnStatusNameChanging(value);
					this.SendPropertyChanging();
					this._StatusName = value;
					this.SendPropertyChanged("StatusName");
					this.OnStatusNameChanged();
				}
			}
		}
		
		[Association(Name="Status_Issue", Storage="_Issues", ThisKey="StatusId", OtherKey="RefStatusId")]
		public EntitySet<Issue> Issues
		{
			get
			{
				return this._Issues;
			}
			set
			{
				this._Issues.Assign(value);
			}
		}
		
		[Association(Name="Status_Task", Storage="_Tasks", ThisKey="StatusId", OtherKey="RefStatusId")]
		public EntitySet<Task> Tasks
		{
			get
			{
				return this._Tasks;
			}
			set
			{
				this._Tasks.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Issues(Issue entity)
		{
			this.SendPropertyChanging();
			entity.Status = this;
		}
		
		private void detach_Issues(Issue entity)
		{
			this.SendPropertyChanging();
			entity.Status = null;
		}
		
		private void attach_Tasks(Task entity)
		{
			this.SendPropertyChanging();
			entity.Status = this;
		}
		
		private void detach_Tasks(Task entity)
		{
			this.SendPropertyChanging();
			entity.Status = null;
		}
	}
}
#pragma warning restore 1591
