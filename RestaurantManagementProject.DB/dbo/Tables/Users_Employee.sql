﻿CREATE TABLE [dbo].[Users_Employee] (
    [HoursPerWeek] INT          NOT NULL,
    [PayRate]      DECIMAL (18) NULL,
    [Salary]       DECIMAL (18) NULL,
    [Id]           INT          NOT NULL,
    CONSTRAINT [PK_Users_Employee] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employee_inherits_User] FOREIGN KEY ([Id]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);

