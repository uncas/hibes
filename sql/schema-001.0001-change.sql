/*ALTER TABLE Task DROP FK_Task_Person
ALTER TABLE Task DROP COLUMN RefPersonId
ALTER TABLE PersonOff DROP FK_PersonOff_Person
ALTER TABLE PersonOff DROP COLUMN RefPersonId
DROP TABLE Person*/

GO

CREATE TABLE Person
(
	PersonId  int  NOT NULL  IDENTITY(1,1)
		CONSTRAINT PK_Person PRIMARY KEY CLUSTERED
	, PersonName nvarchar(50) NOT NULL
        CONSTRAINT UK_Person_Name UNIQUE
	, DaysPerWeek  int  NOT NULL
		CONSTRAINT DF_Person_DaysPerWeek DEFAULT 5
	, HoursPerDay  decimal(10,2)  NOT NULL
		CONSTRAINT DF_Person_HoursPerDay DEFAULT 7.5
)

GO

INSERT INTO Person
(PersonName)
VALUES
('N.N.')

GO

ALTER TABLE Task
	ADD
		RefPersonId  int  NULL
		CONSTRAINT FK_Task_Person FOREIGN KEY REFERENCES Person(PersonId)
GO

UPDATE Task
SET RefPersonId = 1

GO

ALTER TABLE Task
	ALTER
		COLUMN RefPersonId  int  NOT NULL

GO

ALTER TABLE PersonOff
	ADD
		RefPersonId  int  NULL
		CONSTRAINT FK_PersonOff_Person FOREIGN KEY REFERENCES Person(PersonId)

GO

UPDATE PersonOff
SET RefPersonId = 1

GO

ALTER TABLE PersonOff
	ALTER
		COLUMN RefPersonId  int  NOT NULL
