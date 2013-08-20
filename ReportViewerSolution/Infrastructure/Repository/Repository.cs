using System.Collections.Generic;
using System.Linq;
using PatientHistory.Domain.Entities;
using PatientHistory.Domain.ValueObjects;

namespace Repository
{
  public class PatientRepository
  {
    private readonly PatientHistoryDataContext _context;

    public PatientRepository(PatientHistoryDataContext context)
    {
      _context = context;
    }

    public List<PatientResultItem> Find(string patientFirstName, string clientLastName)
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
        .Select(p=>new {Patient=p,VisitNotes=p.VisitNotes.OrderByDescending(v=>v.VisitDateTime)}).SingleOrDefault();
      var patient = results.Patient;
      patient.SetVisitHistory(results.VisitNotes);
      return patient;
    }
  }
}
