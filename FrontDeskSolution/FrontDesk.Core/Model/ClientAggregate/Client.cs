using System;
using System.Collections.Generic;
using System.Linq;
using FrontDesk.Core.Interfaces;

namespace FrontDesk.Core.Model.ClientAggregate
{
    public class Client : IEntity
    {
        public Guid Id { get; private set; }
        public Common.FullName FullName { get; set; }
        public string Salutation { get; set; }
        public string PreferredName { get; set; }

        public IList<PatientInfo> Patients { get; private set; }

        public Client()
        {
            Patients = new List<PatientInfo>();
        }

        public override string ToString()
        {
            string result = String.Empty;
            if (!String.IsNullOrWhiteSpace(this.Salutation))
            {
                result += this.Salutation;
            }
            result = string.Format("{0} {1}", result, this.FullName.AsFormattedName()).Trim();

            if (!String.IsNullOrWhiteSpace(this.PreferredName))
            {
                result = string.Format("{0} ({1})", result, this.PreferredName).Trim();
            }

            return result;
        }
    }
}