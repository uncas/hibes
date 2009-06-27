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