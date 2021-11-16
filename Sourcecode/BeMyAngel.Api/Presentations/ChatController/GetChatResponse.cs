using BeMyAngel.Service.Models;
using System.Collections.Generic;

namespace BeMyAngel.Api.Presentations.ChatController
{
    public class GetChatResponse
    {
        public Chat Chat { get; set; }
        public string MyChatSessionToken { get; set; }
    }
}
