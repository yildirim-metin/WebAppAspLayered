CREATE TABLE [dbo].[CartItem] (
    [Id]       INT NOT NULL IDENTITY,
    [CartId]   INT NOT NULL,
    [BookId]   INT NOT NULL,
    [Quantity] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CartItem_Book] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Book] ([Id]),
    CONSTRAINT [FK_CartItem_Cart] FOREIGN KEY ([CartId]) REFERENCES [dbo].[Cart] ([Id])
);