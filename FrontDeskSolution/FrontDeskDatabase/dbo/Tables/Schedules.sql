CREATE TABLE [dbo].[Schedules] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [ClinicId] INT              NOT NULL,
    CONSTRAINT [PK_dbo.Schedules] PRIMARY KEY CLUSTERED ([Id] ASC)
);

