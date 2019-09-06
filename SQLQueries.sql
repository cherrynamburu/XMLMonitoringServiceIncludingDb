Create Database XMLMonitoringService

use XMLMonitoringService

CREATE TABLE dbo.Job  
(  
    jobid int NOT NULL,
    jobtitle varchar(255) NOT NULL,
    employer varchar(255) NOT NULL,
    description varchar(5000) NULL,
    EmpHistoryId int NULL,
    CONSTRAINT PK_Job_JobId PRIMARY KEY CLUSTERED (JobId)  
);

CREATE TABLE dbo.EmployementHistory
(
	EmployementHistoryId int NOT NULL IDENTITY(1,1),
	EmployementStart varchar(255) NOT NULL,
	EmployementEnd varchar(255) NOT NULL,
	CONSTRAINT PK_EmployementHistoryId PRIMARY KEY CLUSTERED (EmployementHistoryId)
);

ALTER TABLE dbo.Job ADD CONSTRAINT FK_Job_EmployementHistory FOREIGN KEY (EmpHistoryId) REFERENCES dbo.EmployementHistory (EmployementHistoryId)
    ON DELETE CASCADE;


Insert into EmployementHistory values('June1992','August1998')


SELECT MAX(EmployementHistoryId) from EmployementHistory;

UPDATE Job
SET jobtitle = 'lol',
    employer = 'expression2',
    description = 'expression2'
WHERE jobid = 1001;