using System;
using System.Collections.Generic;
using System.Linq;
using AppointmentScheduling.Core.Model.Events;
using FrontDesk.SharedKernel;
using FrontDesk.SharedKernel.Enums;

namespace AppointmentScheduling.Core.Model.ScheduleAggregate
{
    public class Schedule : Entity<Guid>
    {
        public int ClinicId { get; private set; }

        // not persisted
        public DateTimeRange DateRange { get; private set; }

        private List<Appointment> _appointments;
        public IEnumerable<Appointment> Appointments
        {
            get
            {
                return _appointments.AsEnumerable();
            }
            private set
            {
                _appointments = (List<Appointment>)value;
            }
        }

        public Schedule(Guid id, DateTimeRange dateRange, int clinicId, IEnumerable<Appointment> appointments)
            : base(id)
        {
            DateRange = dateRange;
            ClinicId = clinicId;
            _appointments = new List<Appointment>(appointments);
            MarkConflictingAppointments();

            DomainEvents.Register<AppointmentUpdatedEvent>(Handle);
        }

        private Schedule()
            : base(Guid.NewGuid()) // required for EF
        {
            _appointments = new List<Appointment>();
        }

        public Appointment AddNewAppointment(Appointment appointment)
        {
            if (_appointments.Any(a => a.Id == appointment.Id))
            {
                throw new ArgumentException("Cannot add duplicate appointment to schedule.", "appointment");
            }

            appointment.State = TrackingState.Added;
            _appointments.Add(appointment);

            MarkConflictingAppointments();

            var appointmentScheduledEvent = new AppointmentScheduledEvent(appointment);
            DomainEvents.Raise(appointmentScheduledEvent);

            return appointment;
        }

        public void DeleteAppointment(Appointment appointment)
        {
            // mark the appointment for deletion by the repository
            var appointmentToDelete = this.Appointments.Where(a => a.Id == appointment.Id).FirstOrDefault();
            if (appointmentToDelete != null)
            {
                appointmentToDelete.State = TrackingState.Deleted;
            }

            MarkConflictingAppointments();
        }

        private void MarkConflictingAppointments()
        {
            foreach (var appointment in _appointments)
            {
                var potentiallyConflictingAppointments = _appointments
                    .Where(a => a.PatientId == appointment.PatientId &&
                    a.TimeRange.Overlaps(appointment.TimeRange) &&
                    a.Id != appointment.Id &&
                    a.State != TrackingState.Deleted).ToList();

                potentiallyConflictingAppointments.ForEach(a => a.IsPotentiallyConflicting = true);

                appointment.IsPotentiallyConflicting = potentiallyConflictingAppointments.Any();
            }
        }

        public void Handle(AppointmentUpdatedEvent args)
        {
            MarkConflictingAppointments();
        }
    }
}