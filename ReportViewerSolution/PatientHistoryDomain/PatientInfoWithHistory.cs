using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientHistory.Domain
{
 
  public class PatientInfo
  {
    //considering data coming via EF...
    
    public static PatientInfo CreatePatientInfoWithHistory(int id, int patientFirstName, int clientLastName, string breed, string species, int latestWeight, DateTime latestWeightTaken, decimal currentAge, string watchItems)
    {
      return new PatientInfo(id, patientFirstName, clientLastName, breed, species, latestWeight, latestWeightTaken, currentAge, watchItems);
    }

    private PatientInfo(int id, int patientFirstName, int clientLastName, string breed, string species, int latestWeight, DateTime latestWeightTaken, decimal currentAge, string watchItems)
    {
      Id = id;
      PatientFirstName = patientFirstName;
      ClientLastName = clientLastName;
      Breed = breed;
      Species = species;
      LatestWeight = latestWeight;
      LatestWeightTaken = latestWeightTaken;
      CurrentAge = currentAge;
      WatchItems = watchItems;
    }

    public int Id { get; set; }
    public int PatientFirstName { get; private set; }
    public int ClientLastName { get; private set; }
    public string Breed { get; private set; }
    public string Species { get; private set; }
    public int LatestWeight { get; private set; }
    public DateTime LatestWeightTaken { get; private set; }
    public decimal CurrentAge { get; private set; }
    public string WatchItems { get; private set; }
    public IList<VisitNote> VisitNotes {get; private set; }

    public void SetVisitHistory(IOrderedEnumerable<VisitNote> visitNotes)
    {
      visitNotes.ToList().ForEach(note => VisitNotes.Add(note));
    }
  }
}