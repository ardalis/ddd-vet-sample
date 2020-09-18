namespace BlazorShared.Models.Patient
{
    public class PatientDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public int? PreferredDoctorId { get; set; }
    }
}
