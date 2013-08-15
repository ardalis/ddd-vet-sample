using System.Collections.Generic;
using PatientHistory.Domain;
using System.Linq;
using TestsAndSpecs.Features.Search;

namespace Repository
{
  public class PatientRepository
  {
    private readonly DataContext _context;

    public PatientRepository(DataContext context)
    {
      _context = context;
    }

    public List<PatientResultItem> Find(string patientFirstName, string clientLastName)
    {

      var results =
        _context.PatientList.Where(p => p.FirstName.Contains(patientFirstName)
                                        && p.LastName.Contains(clientLastName)).ToList();
      return results;
    }


    public PatientInfo Find(int patientId)
    {
      var results =
        _context.Patients.Where(p => p.Id == patientId)
        .Select(p=>new {Patient=p,VisitNotes=p.VisitNotes.OrderByDescending(v=>v.VisitDateTime)}).SingleOrDefault();
      var patient = results.Patient;
      patient.SetVisitHistory(results.VisitNotes);
      return results;
    }
  }
}
