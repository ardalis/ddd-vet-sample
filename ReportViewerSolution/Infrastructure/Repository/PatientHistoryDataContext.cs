using System.Collections.Generic;
using System.Data.Entity;
using PatientHistory.Domain;



namespace Repository
{
  public class PatientHistoryDataContext:DbContext
  {
    public IDbSet<PatientInfo> Patients { get; set; }
  
  }
}
