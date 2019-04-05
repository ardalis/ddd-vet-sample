CREATE TABLE [dbo].[Rooms] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Rooms] PRIMARY KEY CLUSTERED ([Id] ASC)
);

