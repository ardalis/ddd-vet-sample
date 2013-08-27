using System;

namespace PatientHistory.Domain.Entities
{
  public class Visit
  {
    //have to remember, this will be read only in this context. So far, 
    //there is no need for a ctor or factory..dang execpt for integration test :(
    public int Id { get; private set; }
    public DateTime CheckedIn { get; private set; }
    public DateTime? CheckedOut { get; private set; }
    public int PatientId { get; private set; }
    public decimal WeightAtCheckIn { get; private set; }
    public string CheckInNotes { get; private set; }
    public bool Cancelled{ get; private set; }

    public bool CurrentlyCheckedIn
    {
      get { return CheckedOut.HasValue==false && Cancelled == false; }
    }

    public  Visit(int patientid, decimal weight,string notes,DateTime datetime)
    {
      PatientId = patientid;
      WeightAtCheckIn = weight;
      CheckInNotes = notes;
      CheckedIn = datetime;
    }
    public void CheckOut()
    {
      CheckedOut = DateTime.Now;
    }

    //can our sample benefit from knowing who brought the patient? who checked the patient in?
  }
}