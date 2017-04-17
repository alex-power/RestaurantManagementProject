CREATE TABLE [dbo].[OrderFoodItem] (
    [Orders_Id]    INT NOT NULL,
    [FoodItems_Id] INT NOT NULL,
    CONSTRAINT [PK_OrderFoodItem] PRIMARY KEY CLUSTERED ([Orders_Id] ASC, [FoodItems_Id] ASC),
    CONSTRAINT [FK_OrderFoodItem_FoodItem] FOREIGN KEY ([FoodItems_Id]) REFERENCES [dbo].[FoodItems] ([Id]),
    CONSTRAINT [FK_OrderFoodItem_Order] FOREIGN KEY ([Orders_Id]) REFERENCES [dbo].[Orders] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_OrderFoodItem_FoodItem]
    ON [dbo].[OrderFoodItem]([FoodItems_Id] ASC);

