﻿CREATE TABLE [dbo].[Cart]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    CONSTRAINT [FK_Cart_User] FOREIGN KEY (Id) REFERENCES [User]([Id]),
)
