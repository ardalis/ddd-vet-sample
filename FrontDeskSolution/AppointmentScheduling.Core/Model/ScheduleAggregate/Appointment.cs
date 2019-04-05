using System;
using FrontDesk.SharedKernel;
using FrontDesk.SharedKernel.Enums;
using AppointmentScheduling.Core.Model.Events;

namespace AppointmentScheduling.Core.Model.ScheduleAggregate
{
    public class Appointment : Entity<Guid>
    {
        public Guid ScheduleId { get; private set; }
        public int ClientId { get; private set; }
        public int PatientId { get; private set; }
        public int RoomId { get; private set; }
        public int? DoctorId { get; private set; }
        public int AppointmentTypeId { get; private set; }
        public DateTimeRange TimeRange { get; private set; }

        public string Title { get; set; }

        #region More Properties
        public DateTime? DateTimeConfirmed { get; set; }

        // not persisted
        public TrackingState State { get; set; }
        public bool IsPotentiallyConflicting { get; set; }
        #endregion

        public Appointment(Guid id)
            : base(id)
        {
        }

        private Appointment()
            : base(Guid.NewGuid()) // required for EF
        {
        }

        public void UpdateRoom(int newRoomId)
        {
            if (newRoomId == RoomId) return;

            RoomId = newRoomId;

            var appointmentUpdatedEvent = new AppointmentUpdatedEvent(this);
            DomainEvents.Raise(appointmentUpdatedEvent);
        }

        public void UpdateTime(DateTimeRange newStartEnd)
        {
            if (newStartEnd == TimeRange) return;

            TimeRange = newStartEnd;

            var appointmentUpdatedEvent = new AppointmentUpdatedEvent(this);
            DomainEvents.Raise(appointmentUpdatedEvent);
        }

        public void Confirm(DateTime dateConfirmed)
        {
            if (DateTimeConfirmed.HasValue) return; // no need to reconfirm

            DateTimeConfirmed = dateConfirmed;

            var appointmentConfirmedEvent = new AppointmentConfirmedEvent(this);
            DomainEvents.Raise(appointmentConfirmedEvent);
        }

        // Factory method for creation
        public static Appointment Create(Guid scheduleId, 
            int clientId, int patientId, 
            int roomId, DateTime startTime, DateTime endTime, 
            int appointmentTypeId, int? doctorId, string title)
        {
            Guard.ForLessEqualZero(clientId, "clientId");
            Guard.ForLessEqualZero(patientId, "patientId");
            Guard.ForLessEqualZero(roomId, "roomId");
            Guard.ForLessEqualZero(appointmentTypeId, "appointmentTypeId");
            Guard.ForNullOrEmpty(title, "title");
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