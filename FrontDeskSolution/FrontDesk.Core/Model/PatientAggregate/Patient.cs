using System;
using FrontDesk.Core.Enums;
using FrontDesk.Core.Interfaces;
using FrontDesk.Core.Model.ClientAggregate;

namespace FrontDesk.Core.Model.PatientAggregate
{
    public class Patient : IEntity
    {
        public Guid Id { get; private set; }
        public Client Owner { get; private set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }

        public Patient(Client owner)
        {
            this.Owner = owner;
        }
    }
}