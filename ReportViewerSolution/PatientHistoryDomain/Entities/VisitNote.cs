using System;
using PatientHistory.Domain.Enums;

namespace PatientHistory.Domain
{
  public class VisitNote
  {
    public int Id { get; set; }
    public int DoctorId { get; private set; }
    public DateTime  VisitDateTime { get; private set; }
    public OverallHealthStatus OverallStatus { get; private set; }
    public string Notes { get; private set; }

  }
}