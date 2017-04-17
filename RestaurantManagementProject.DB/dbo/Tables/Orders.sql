CREATE TABLE [dbo].[Orders] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [TotalPrice]    NVARCHAR (MAX) NULL,
    [Tip]           NVARCHAR (MAX) NULL,
    [State]         NVARCHAR (MAX) NOT NULL,
    [TimeCreated]   DATETIME       NOT NULL,
    [TimeCompleted] DATETIME       NULL,
    [Table_Id]      INT            NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tables] FOREIGN KEY ([Table_Id]) REFERENCES [dbo].[Tables] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Tables]
    ON [dbo].[Orders]([Table_Id] ASC);

