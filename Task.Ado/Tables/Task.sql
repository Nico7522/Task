﻿CREATE TABLE [dbo].[Task]
(
	[TaskId] INT IDENTITY NOT NULL, 
	Title NVARCHAR(100) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	IsFinished BIT DEFAULT 0,
	PersonId INT
    CONSTRAINT [PK_Task] PRIMARY KEY ([TaskId]) 
	CONSTRAINT [FK_PersonId] FOREIGN KEY (PersonId) REFERENCES Person (PersonId)

)
