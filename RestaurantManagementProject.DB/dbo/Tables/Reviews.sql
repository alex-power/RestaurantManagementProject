CREATE TABLE [dbo].[Reviews] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Text]         NVARCHAR (MAX) NOT NULL,
    [DateOfVisit]  DATETIME       NOT NULL,
    [DateOfPost]   DATETIME       NOT NULL,
    [Rating]       INT            NOT NULL,
    [CustomerName] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED ([Id] ASC)
);

