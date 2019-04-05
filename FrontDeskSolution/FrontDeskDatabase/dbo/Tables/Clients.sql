CREATE TABLE [dbo].[Clients] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [FullName]          NVARCHAR (MAX) NULL,
    [Salutation]        NVARCHAR (MAX) NULL,
    [EmailAddress]      NVARCHAR (MAX) NULL,
    [PreferredName]     NVARCHAR (MAX) NULL,
    [PreferredDoctorId] INT            NULL,
    CONSTRAINT [PK_dbo.Clients] PRIMARY KEY CLUSTERED ([Id] ASC)
);

