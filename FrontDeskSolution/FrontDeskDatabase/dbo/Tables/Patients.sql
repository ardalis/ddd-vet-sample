CREATE TABLE [dbo].[Patients] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [ClientId]           INT            NOT NULL,
    [Name]               NVARCHAR (MAX) NULL,
    [Gender]             INT            NOT NULL,
    [AnimalType_Species] NVARCHAR (MAX) NULL,
    [AnimalType_Breed]   NVARCHAR (MAX) NULL,
    [PreferredDoctorId]  INT            NULL,
    CONSTRAINT [PK_dbo.Patients] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Patients_dbo.Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ClientId]
    ON [dbo].[Patients]([ClientId] ASC);

