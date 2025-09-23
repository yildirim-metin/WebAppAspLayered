CREATE TABLE [dbo].[Book] (
    [Id]     INT            NOT NULL,
    [ISBN]   NVARCHAR (13)  NOT NULL,
    [Name]   NVARCHAR (100) NULL,
    [Author] NVARCHAR (100) NULL,
    [IsFav] BIT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);