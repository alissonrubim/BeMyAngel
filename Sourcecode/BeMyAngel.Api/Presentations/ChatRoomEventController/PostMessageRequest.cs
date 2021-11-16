namespace BeMyAngel.Api.Presentations.ChatEventController
{
    public class PostMessageRequest
    {
        public int ChatId { get; set; }
        public string Message { get; set; }
    }
}
