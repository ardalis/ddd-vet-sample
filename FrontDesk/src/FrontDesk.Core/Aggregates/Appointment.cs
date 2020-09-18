using System;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using FrontDesk.Core.Events;
using FrontDesk.SharedKernel;
using FrontDesk.SharedKernel.Enums;
using FrontDesk.SharedKernel.Interfaces;

namespace FrontDesk.Core.Aggregates
{
    public class Appointment : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid ScheduleId { get; private set; }
        public int ClientId { get; private set; }
        public int PatientId { get; private set; }
        public int RoomId { get; private set; }
        public int? DoctorId { get; private set; }
        public int AppointmentTypeId { get; private set; }

        [NotMapped]
        public DateTimeRange TimeRange { get; private set; }

        public string Title { get; private set; }

        #region More Properties
        public DateTime? DateTimeConfirmed { get; set; }

        // not persisted
        public TrackingState State { get; set; }
        public bool IsPotentiallyConflicting { get; set; }
        #endregion

        public Appointment(int appointmentTypeId, Guid scheduleId, int clientId, int doctorId, int patientId, int roomId, DateTimeRange timeRange, string title, DateTime? dateTimeConfirmed = null)
        {
            Id = Guid.NewGuid();
            AppointmentTypeId = appointmentTypeId;
            ScheduleId = scheduleId;
            ClientId = clientId;
            DoctorId = doctorId;
            PatientId = patientId;
            RoomId = roomId;
            TimeRange = timeRange;            
            Title = title;
            DateTimeConfirmed = dateTimeConfirmed;
        }        

        public Appointment(Guid id)
        {
            Id = id;
        }

        private Appointment() // required for EF
        {
            Id = Guid.NewGuid();
        }

        public void UpdateRoom(int newRoomId)
        {
            if (newRoomId == RoomId) return;

            RoomId = newRoomId;

            var appointmentUpdatedEvent = new AppointmentUpdatedEvent(this);
            BaseDomainEvent.Raise(appointmentUpdatedEvent);
        }

        public void UpdateTime(DateTimeRange newStartEnd)
        {
            if (newStartEnd == TimeRange) return;

            TimeRange = newStartEnd;

            var appointmentUpdatedEvent = new AppointmentUpdatedEvent(this);
            BaseDomainEvent.Raise(appointmentUpdatedEvent);
        }

        public void Confirm(DateTime dateConfirmed)
        {
            if (DateTimeConfirmed.HasValue) return; // no need to reconfirm

            DateTimeConfirmed = dateConfirmed;

            var appointmentConfirmedEvent = new AppointmentConfirmedEvent(this);
            BaseDomainEvent.Raise(appointmentConfirmedEvent);
        }

        // Factory method for creation
        public static Appointment Create(Guid scheduleId, 
            int clientId, int patientId, 
            int roomId, DateTime startTime, DateTime endTime, 
            int appointmentTypeId, int? doctorId, string title)
        {
            Guard.Against.NegativeOrZero(clientId, "clientId");
            Guard.Against.NegativeOrZero(patientId, "patientId");
            Guard.Against.NegativeOrZero(roomId, "roomId");
            Guard.Against.NegativeOrZero(appointmentTypeId, "appointmentTypeId");
            Guard.Against.NullOrEmpty(title, "title");
            var appointment = new Appointment(Guid.NewGuid());
            appointment.ScheduleId = scheduleId;
            appointment.PatientId = patientId;
            appointment.ClientId = clientId;
            appointment.RoomId = roomId;
            appointment.TimeRange = new DateTimeRange(startTime, endTime);
            appointment.AppointmentTypeId = appointmentTypeId;
            appointment.DoctorId = doctorId ?? 1;
            appointment.Title = title;
            return appointment;
        }
    }
}