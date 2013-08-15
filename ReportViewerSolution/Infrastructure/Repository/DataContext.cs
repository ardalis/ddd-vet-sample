using System.Collections.Generic;
using System.Data.Entity;
using PatientHistory.Domain;
using TestsAndSpecs.Features.Search;


namespace Repository
{
  public class DataContext
  {
    public IList<PatientResultItem> PatientList { get; set; }
    public IDbSet<PatientInfo> Patients { get; set; }
  
  }
}
