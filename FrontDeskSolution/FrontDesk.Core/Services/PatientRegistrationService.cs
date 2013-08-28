using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontDesk.Core.Model.PatientAggregate;

namespace FrontDesk.Core.Services
{
    public delegate void NewPatientCreatedEventHandler(object sender, NewPatientCreatedEventArgs e);

    public class NewPatientCreatedEventArgs : EventArgs
    {
        public NewPatientCreatedEventArgs (Patient patient)
        {
            this.Patient = patient;
        }

        public Patient Patient { get; private set; }
    }

    public class PatientRegistrationService
    {
        private readonly IPatientRepository _patientRepository;
        public event NewPatientCreatedEventHandler NewPatientCreated;

        public PatientRegistrationService(IPatientRepository patientRepository)
        {
            this._patientRepository = patientRepository;
        }

        public void RegisterPatient(Patient patient)
        {
            _patientRepository.CreateOrUpdate(patient);

            OnNewPatientCreated(new NewPatientCreatedEventArgs(patient));
        }

        private void OnNewPatientCreated(NewPatientCreatedEventArgs e)
        {
            if (NewPatientCreated != null)
            {
                NewPatientCreated(this, e);
            }
        }
    }

    public interface IPatientRepository
    {
        void CreateOrUpdate(Patient patient);
    }
}
