using BlazorShared.Models.AppointmentType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorShared.Models.Appointment
{
    public class AppointmentDto
    {
        public Guid AppointmentId { get; set; }
        public int RoomId { get; set; }
        public int? DoctorId { get; set; }
        public int ClientId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }

        [Required(ErrorMessage = "The Start field is required")]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "The End field is required")]
        public DateTime End { get; set; }
        public object StartTimezone { get; set; }
        public object EndTimezone { get; set; }
        public string RecurrenceRule { get; set; }
        public List<DateTime> RecurrenceExceptions { get; set; }
        public Guid? RecurrenceId { get; set; }

        [Required(ErrorMessage = "The Title is required")]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsAllDay { get; set; }
        public bool IsPotentiallyConflicting { get; set; }
        public bool IsConfirmed { get; set; }

        [Required(ErrorMessage = "The Appointment Type field is required")]
        public int AppointmentTypeId { get; set; }

        public AppointmentTypeDto AppointmentType { get; set; }

        public AppointmentDto ShallowCopy()
        {
            return (AppointmentDto)this.MemberwiseClone();            
        }
    }
}
