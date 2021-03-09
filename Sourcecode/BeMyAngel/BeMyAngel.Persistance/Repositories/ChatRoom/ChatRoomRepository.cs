using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Persistance.Repositories.ChatRoom
{
    internal class ChatRoomRepository : IChatRoomRepository
    {
        private readonly IDatabase _database;
        public ChatRoomRepository(IDatabase database)
        {
            _database = database;
        }
        public ChatRoomDto Get(int userId, int chatRoomId)
        {
            return _database.Fetch<ChatRoomDto>(@"SELECT 
                                                    CR.[ChatRoomId], 
                                                    CR.[CreatedAtDateTime], 
                                                    CR.[TerminatedAtDateTime] 
                                                FROM [ChatRoom] CR
                                                INNER JOIN [ChatRoomUser] CRU on CRU.[ChatRoomId] = CR.[ChatRoomId]
                                                WHERE 
                                                    CR.[ChatRoomId] = @ChatRoomId
                                                    and CRU.[UserId] = @UserId", new { ChatRoomId = chatRoomId, UserId = userId });
        }

        public ChatRoomDto GetCurrent(int userId)
        {
            return _database.Fetch<ChatRoomDto>(@"SELECT 
                                                    CR.[ChatRoomId], 
                                                    CR.[CreatedAtDateTime], 
                                                    CR.[TerminatedAtDateTime] 
                                                FROM [ChatRoom] CR
                                                INNER JOIN [ChatRoomUser] CRU on CRU.[ChatRoomId] = CR.[ChatRoomId]
                                                WHERE 
                                                    CRU.[UserId] = @UserId
                                                    and [TerminateAtDateTime] IS NULL", new { UserId = userId });
        }

        public int Insert(ChatRoomDto dto)
        {
            throw new NotImplementedException();
        }

        public void Update(ChatRoomDto dto)
        {
            _database.Execute(@"UPDATE [ChatRoom] SET [TerminateAtDateTime] = @TerminateAtDateTime WHERE [ChatRoomId] = @ChatRoomId", dto);
        }
    }
}
