using BeMyAngel.Api.Presentations.ChatEventController;
using System.Threading.Tasks;

namespace BeMyAngel.Api.Hubs
{
    public interface IChatHub
    {
        void ReceiveMessage(HubEventResponse hubEvent);
    }
}
