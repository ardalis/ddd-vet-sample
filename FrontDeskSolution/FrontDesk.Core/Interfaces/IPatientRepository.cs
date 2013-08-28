using FrontDesk.Core.Model.PatientAggregate;

namespace FrontDesk.Core.Interfaces
{
    public interface IPatientRepository
    {
        void CreateOrUpdate(Patient patient);
    }
}