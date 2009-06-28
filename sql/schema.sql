CREATE TABLE Status
(
	StatusId  int  NOT NULL
		CONSTRAINT PK_Status PRIMARY KEY CLUSTERED
	, StatusName nvarchar(20) NOT NULL
        CONSTRAINT UK_Status_Name UNIQUE
)

GO

INSERT INTO Status
(StatusId, StatusName)
VALUES (1, 'Open')

INSERT INTO Status
(StatusId, StatusName)
VALUES (2, 'Closed')

GO

CREATE TABLE Project
(
	ProjectId  int  NOT NULL  IDENTITY(1,1)
		CONSTRAINT PK_Project PRIMARY KEY CLUSTERED
	, ProjectName nvarchar(50) NOT NULL
        CONSTRAINT UK_Project_Name UNIQUE
	, CreatedDate datetime NOT NULL
		CONSTRAINT DF_Project_CreatedDate DEFAULT GETDATE()
)

GO

CREATE TABLE Issue
(
	IssueId  int  NOT NULL  IDENTITY(1,1)
		CONSTRAINT PK_Issue PRIMARY KEY CLUSTERED
	, Title nvarchar(100) NOT NULL
	, CreatedDate datetime NOT NULL
		CONSTRAINT DF_Issue_CreatedDate DEFAULT GETDATE()
	, RefProjectId int NOT NULL
		CONSTRAINT FK_Issue_Project FOREIGN KEY REFERENCES Project(ProjectId)
	, RefStatusId int NOT NULL
		CONSTRAINT FK_Issue_Status FOREIGN KEY REFERENCES Status(StatusId)
		CONSTRAINT DF_Issue_RefStatusId DEFAULT 1
	, Priority int NOT NULL
		CONSTRAINT DF_Issue_Priority DEFAULT 1
)

GO

CREATE TABLE Task
(
	TaskId  int  NOT NULL  IDENTITY(1,1)
		CONSTRAINT PK_Task PRIMARY KEY CLUSTERED
	, Description nvarchar(100) NOT NULL
	, CreatedDate datetime NOT NULL
		CONSTRAINT DF_Task_CreatedDate DEFAULT GETDATE()
	, RefIssueId int NOT NULL
		CONSTRAINT Task_Issue FOREIGN KEY REFERENCES Issue(IssueId)
	, RefStatusId int NOT NULL
		CONSTRAINT Task_Status FOREIGN KEY REFERENCES Status(StatusId)
	, Sequence int NOT NULL
		CONSTRAINT DF_Task_Sequence DEFAULT 1
	, StartDate datetime NULL
	, EndDate datetime NULL
	, OriginalEstimateInHours decimal(10,2) NOT NULL
	, CurrentEstimateInHours decimal(10,2) NOT NULL
	, ElapsedHours decimal(10,2) NOT NULL
		CONSTRAINT DF_Task_Elapsed DEFAULT 0
)

GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Issue]') AND name = N'IX_Issue_Project')
DROP INDEX [IX_Issue_Project] ON [dbo].[Issue] WITH ( ONLINE = OFF )

GO

CREATE NONCLUSTERED INDEX [IX_Issue_Project] ON Issue
(
	RefProjectID ASC
)

GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Issue]') AND name = N'IX_Issue_Status')
DROP INDEX [IX_Issue_Status] ON [dbo].[Issue] WITH ( ONLINE = OFF )

GO

CREATE NONCLUSTERED INDEX [IX_Issue_Status] ON Issue
(
	RefStatusID ASC
)

GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Issue]') AND name = N'IX_Issue_ProjectStatus')
DROP INDEX [IX_Issue_ProjectStatus] ON [dbo].[Issue] WITH ( ONLINE = OFF )

GO

CREATE NONCLUSTERED INDEX [IX_Issue_ProjectStatus] ON Issue
(
	RefProjectID ASC
	, RefStatusID ASC
)

GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Task]') AND name = N'IX_Task_Status')
DROP INDEX [IX_Task_Status] ON [dbo].[Task] WITH ( ONLINE = OFF )

GO

CREATE NONCLUSTERED INDEX [IX_Task_Status] ON Task
(
	RefStatusID ASC
)

GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Task]') AND name = N'IX_Task_Issue')
DROP INDEX [IX_Task_Issue] ON [dbo].[Task] WITH ( ONLINE = OFF )

GO

CREATE NONCLUSTERED INDEX [IX_Task_Issue] ON Task
(
	RefIssueID ASC
)

GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Task]') AND name = N'IX_Task_IssueStatus')
DROP INDEX [IX_Task_IssueStatus] ON [dbo].[Task] WITH ( ONLINE = OFF )

GO

CREATE NONCLUSTERED INDEX [IX_Task_IssueStatus] ON Task
(
	RefIssueID ASC
	, RefStatusID ASC
)

GO