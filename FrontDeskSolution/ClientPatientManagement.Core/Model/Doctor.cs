using System;
using System.Linq;
using ClientPatientManagement.Core.Interfaces;

namespace ClientPatientManagement.Core.Model
{
    public class Doctor : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
