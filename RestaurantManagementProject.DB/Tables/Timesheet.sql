CREATE TABLE [dbo].[Timesheets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TimeIn] datetime  NOT NULL,
    [TimeOut] datetime  NULL,
    [Users_Employee_Id] int  NOT NULL
	 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
	 CONSTRAINT [FK_Timesheet_Employee] FOREIGN KEY ([Id]) REFERENCES [dbo].[Users_Employee] ([Id])
);