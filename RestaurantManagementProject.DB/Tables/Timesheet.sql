CREATE TABLE [dbo].[Timesheet] (
    [Id]                INT      IDENTITY (1, 1) NOT NULL,
    [TimeIn]            DATETIME NOT NULL,
    [TimeOut]           DATETIME NOT NULL,
    [Users_Employee_Id] INT      NOT NULL,
    CONSTRAINT [PK_Timesheet] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_TimesheetUsers_Employee]
    ON [dbo].[Timesheet]([Users_Employee_Id] ASC);

