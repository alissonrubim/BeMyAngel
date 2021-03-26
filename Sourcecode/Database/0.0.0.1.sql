
/*
create table [dbo].[ChatRoomPerson](
   [ChatRoomId] INT NOT NULL,
   [PersonId] INT NOT NULL
);
ALTER TABLE [dbo].[ChatRoomPerson] ADD CONSTRAINT [ChatRoomPerson_FK_ChatRoom] FOREIGN KEY ([ChatRoomId]) REFERENCES [dbo].[ChatRoom]([ChatRoomId]); 
ALTER TABLE [dbo].[ChatRoomPerson] ADD CONSTRAINT [ChatRoomPerson_FK_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person]([PersonId]); 
ALTER TABLE [dbo].[ChatRoomPerson] ADD CONSTRAINT [ChatRoomPerson_UK_ChatRoomId_PersonId] UNIQUE ([ChatRoomId], [PersonId]);

CREATE TABLE [dbo].[ChatRoomEventType](
   [ChatRoomEventTypeId] INT NOT NULL IDENTITY,
   [Identifier] VARCHAR(32) NOT NULL,
   [Description] VARCHAR(128) NOT NULL
);
ALTER TABLE [dbo].[ChatRoomEventType] ADD CONSTRAINT [ChatRoomEventType_PK] PRIMARY KEY ([ChatRoomEventTypeId]);
ALTER TABLE [dbo].[ChatRoomEventType] ADD CONSTRAINT [ChatRoomEventType_UK_Identifier] UNIQUE ([Identifier]);

INSERT INTO [dbo].[ChatRoomEventType]([Description], [Identifier])
VALUES ('Create the chat room', 'CREATE_CHAT'), 
       ('Send message to everyone at chat room', 'SEND_MESSAGE'), 
       ('Terminate chat room', 'TERMINATE_CHAT');

CREATE TABLE [dbo].[ChatRoomEvent](
   [ChatRoomId] INT NOT NULL IDENTITY,
   [ChatRoomEventTypeId] INT NOT NULL,
   [PersonId] INT NOT NULL,
   [DataTime] DATETIME NOT NULL,
   [EventData] VARCHAR(MAX) NULL
);
ALTER TABLE [dbo].[ChatRoomEvent] ADD CONSTRAINT [ChatRoomEvent_FK_ChatRoom] FOREIGN KEY ([ChatRoomId]) REFERENCES [dbo].[ChatRoom]([ChatRoomId]); 
ALTER TABLE [dbo].[ChatRoomEvent] ADD CONSTRAINT [ChatRoomEvent_FK_ChatRoomEventType] FOREIGN KEY ([ChatRoomEventTypeId]) REFERENCES [dbo].[ChatRoomEventType]([ChatRoomEventTypeId]); 
ALTER TABLE [dbo].[ChatRoomEvent] ADD CONSTRAINT [ChatRoomEvent_FK_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person]([PersonId]); 
ALTER TABLE [dbo].[ChatRoomEvent] ADD CONSTRAINT [ChatRoomEvent_CK_EventData] CHECK (ISJSON(EventData)=1);*/