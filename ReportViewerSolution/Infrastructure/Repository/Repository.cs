using System;
using System.Collections.Generic;
using System.Linq;
using PatientHistory.Domain.Entities;
using PatientHistory.Domain.ValueObjects;

namespace Repository
{
  public interface IPatientRepository
  {
    IEnumerable<PatientResultItem> Find(string patientFirstName, string clientLastName);
    PatientInfo Find(int patientId);
  }

  public class PatientRepository : IPatientRepository
  {
    private readonly PatientHistoryDataContext _context;

  
    public PatientRepository(PatientHistoryDataContext context)
    {
      _context = context;
    }

    public PatientRepository()
    {
      _context = new PatientHistoryDataContext();
    }

    public int CurrentCheckedInPatientCount()
    {
      return _context.Visits.Count(v => v.CheckedOut == null && v.Cancelled == false);
    }

    public IEnumerable<PatientResultItem> Find(string patientFirstName, string clientLastName)
    {
      var results =
        _context.Patients.Where(p => p.PatientFirstName.Contains(patientFirstName)
                                     && p.ClientLastName.Contains(clientLastName))
          .Select(p => new PatientResultItem
                         {
                           PatientId = p.Id,
                           FirstName = p.PatientFirstName,
                           LastName = p.ClientLastName,
                           Breed = p.Breed,
                           Species = p.Species,
                           Age = p.CurrentAge
                         })
                         .ToList();
      return results;
    }


    public PatientInfo Find(int patientId)
    {
      var results =
        _context.Patients.Where(p => p.Id == patientId)
        .Select(p=>new {Patient=p,VisitNotes=p.VisitNotes.OrderByDescending(v=>v.VisitDateTime)})
        .SingleOrDefault();

      if (results != null)
      {
        var patient = results.Patient;
        patient.SetVisitHistory(results.VisitNotes);
        return patient;
      }
      return null;
    }

    public IList<CheckedInPatientResultItem> GetCurrentCheckedInPatients()
    {
      var visits = _context.Visits.Where(v => v.CheckedOut == null && v.Cancelled==false ).Select(v => new {v.PatientId,v.CheckedIn});
      var patients=
      _context.Patients
      .Where(p=>visits.Any() || visits.Select(v=>v.PatientId).Contains(p.Id) )
         .Select(p => new CheckedInPatientResultItem
         {
           PatientId = p.Id,
           FirstName = p.PatientFirstName,
           LastName = p.ClientLastName,
           Breed = p.Breed,
           Species = p.Species,
           Age = p.CurrentAge
         })
                        .ToList();
      foreach (var patient in patients)
      {
        patient.CheckedIn = visits.FirstOrDefault(v => v.PatientId == patient.PatientId).CheckedIn;
      }
      return patients;
    }
  }
}
