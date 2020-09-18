namespace BlazorShared.Models.Room
{
    public class UpdateRoomRequest : BaseRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}