using BeMyAngel.Api.Presentations.ChatRoomEventController;
using System.Threading.Tasks;

namespace BeMyAngel.Api.Hubs
{
    public interface IChatHub
    {
        Task ReceiveMessage(HubEventResponse hubEvent);
    }
}
