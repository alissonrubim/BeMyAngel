using BeMyAngel.Service.Models;
using System.Collections.Generic;

namespace BeMyAngel.Api.Presentations.ChatController
{
    public class GetCurrentResponse
    {
        public Chat Chat { get; set; }
        public string MyChatSessionToken { get; set; }
    }
}
