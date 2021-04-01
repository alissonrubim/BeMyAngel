namespace BeMyAngel.Api.Presentations.ChatRoomEventController
{
    public class PostMessageRequest
    {
        public int ChatRoomId { get; set; }
        public string Message { get; set; }
    }
}
