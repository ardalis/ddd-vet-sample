CREATE TABLE [dbo].[Doctors] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Doctors] PRIMARY KEY CLUSTERED ([Id] ASC)
);

