CREATE TABLE [dbo].[Appointments] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [ScheduleId]        UNIQUEIDENTIFIER NOT NULL,
    [ClientId]          INT              NOT NULL,
    [PatientId]         INT              NOT NULL,
    [RoomId]            INT              NOT NULL,
    [DoctorId]          INT              NULL,
    [TimeRange_Start]   DATETIME         NOT NULL,
    [TimeRange_End]     DATETIME         NOT NULL,
    [AppointmentTypeId] INT              NOT NULL,
    [Title]             NVARCHAR (MAX)   NULL,
    [DateTimeConfirmed] DATETIME         NULL,
    CONSTRAINT [PK_dbo.Appointments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Appointments_dbo.Schedules_ScheduleId] FOREIGN KEY ([ScheduleId]) REFERENCES [dbo].[Schedules] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ScheduleId]
    ON [dbo].[Appointments]([ScheduleId] ASC);

