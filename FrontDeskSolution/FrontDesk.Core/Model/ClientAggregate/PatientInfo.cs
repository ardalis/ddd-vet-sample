using System;
using FrontDesk.Core.Enums;
using FrontDesk.Core.Interfaces;

namespace FrontDesk.Core.Model.ClientAggregate
{
    public class PatientInfo : IEntity
    {
        public Guid Id { get; private set; }
        public Client Owner { get; private set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public PatientInfo(Client owner)
        {
            this.Owner = owner;
        }
    }
}