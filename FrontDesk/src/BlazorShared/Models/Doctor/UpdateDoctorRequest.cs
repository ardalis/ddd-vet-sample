namespace BlazorShared.Models.Doctor
{
    public class UpdateDoctorRequest : BaseRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
