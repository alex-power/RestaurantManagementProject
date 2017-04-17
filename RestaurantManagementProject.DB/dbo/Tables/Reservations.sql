CREATE TABLE [dbo].[Reservations] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [DateTime]     DATETIME       NOT NULL,
    [Note]         NVARCHAR (MAX) NOT NULL,
    [CustomerName] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Reservations] PRIMARY KEY CLUSTERED ([Id] ASC)
);

