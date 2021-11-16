using BeMyAngel.Api.Presentations.ChatEventController;
using System.Threading.Tasks;

namespace BeMyAngel.Api.Hubs
{
    public interface IChatHub
    {
        Task SendMessage(string chatIdentifier, string chatSessionToken, HubEventResponse eventResponse);
    }
}
