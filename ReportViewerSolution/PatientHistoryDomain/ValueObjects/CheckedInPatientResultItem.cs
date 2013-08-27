using System;

namespace PatientHistory.Domain.ValueObjects
{
  public class CheckedInPatientResultItem:PatientResultItem
  {
    public DateTime CheckedIn { get; set; }
  }
}
