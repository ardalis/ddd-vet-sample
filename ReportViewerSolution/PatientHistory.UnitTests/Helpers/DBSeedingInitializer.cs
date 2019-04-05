using System;
using System.Data.Entity;
using PatientHistory.Domain.Entities;
using Repository;

namespace TestsAndSpecs.Helpers
{
  public class PatientHistoryDBSeedingInitializer : DropCreateDatabaseAlways<PatientHistoryDataContext>
  {
    protected override void Seed(PatientHistoryDataContext context)
    {
      context.Patients.Add
        (PatientInfo.CreatePatientInfoWithHistory(0, "Sampson", "Flynn", "Newfoundland", "Dog", 120, DateTime.Now, 5, "watch tremor in rear legs"));
      context.Visits.Add(new Visit(1, 126, "knocked the files off of the counter again", DateTime.Now));
      var checkedout = new Visit(1, 123, "knocked the files off of the counter again", DateTime.Now.AddDays(-7));
      checkedout.CheckOut();
      context.Visits.Add(checkedout);

      base.Seed(context);
    }


  }
}
