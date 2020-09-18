namespace BlazorShared.Models.Patient
{
    public class UpdatePatientRequest : BaseRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
