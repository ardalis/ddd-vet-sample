namespace AppointmentScheduling.Core.Model.ScheduleAggregate
{
  public class AppointmentType
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int Duration { get; set; }
  }
}