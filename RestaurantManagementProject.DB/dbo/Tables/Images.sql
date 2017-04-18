CREATE TABLE [dbo].[Images] (
    [Id]     INT             NOT NULL,
    [Image1] VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FoodItems] FOREIGN KEY ([Id]) REFERENCES [dbo].[FoodItems] ([Id])
);

