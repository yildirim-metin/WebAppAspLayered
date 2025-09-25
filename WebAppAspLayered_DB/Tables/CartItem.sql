CREATE TABLE [dbo].[CartItem]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CartId] INT NOT NULL, 
    [BookId] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    CONSTRAINT [FK_CartItem_Cart] FOREIGN KEY ([CartId]) REFERENCES [Cart]([Id]), 
    CONSTRAINT [FK_CartItem_Book] FOREIGN KEY ([BookId]) REFERENCES [Book]([Id])
)
