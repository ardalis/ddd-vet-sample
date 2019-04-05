CREATE TABLE [dbo].[AppointmentTypes] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (MAX) NULL,
    [Code]     NVARCHAR (MAX) NULL,
    [Duration] INT            NOT NULL,
    CONSTRAINT [PK_dbo.AppointmentTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

