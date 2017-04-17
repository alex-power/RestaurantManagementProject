CREATE TABLE [dbo].[Images]
(
	[Id] INT NOT NULL, 
    [Image] VARBINARY(MAX) NULL
	CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Images_FoodItems] FOREIGN KEY ([Id]) REFERENCES [dbo].[FoodItems] ([Id])
)
