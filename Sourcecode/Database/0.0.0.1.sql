ALTER TABLE [dbo].[Session] ADD [CreatedAt] DATETIMEOFFSET NOT NULL DEFAULT GETDATE();
ALTER TABLE [dbo].[Session] ADD [LastAccessAt] DATETIMEOFFSET NOT NULL DEFAULT GETDATE();

ALTER TABLE [dbo].[Session] ADD [UserId] INTEGER NULL;
ALTER TABLE [dbo].[Session] ADD CONSTRAINT [Session_FK_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId])

ALTER TABLE [dbo].[ChatRoom] DROP COLUMN [CreatedAtDateTime]
ALTER TABLE [dbo].[ChatRoom] DROP COLUMN [TerminatedAtDateTime]

ALTER TABLE [dbo].[ChatRoom] ADD [CreatedAt] DATETIMEOFFSET NOT NULL DEFAULT GETDATE();
ALTER TABLE [dbo].[ChatRoom] ADD [TerminatedAt] DATETIMEOFFSET NULL;

create table [dbo].[ChatRoomSession](
   [ChatRoomId] INT NOT NULL,
   [SessionId] INT NOT NULL
);
ALTER TABLE [dbo].[ChatRoomSession] ADD CONSTRAINT [ChatRoomSession_FK_ChatRoom] FOREIGN KEY ([ChatRoomId]) REFERENCES [dbo].[ChatRoom]([ChatRoomId]); 
ALTER TABLE [dbo].[ChatRoomSession] ADD CONSTRAINT [ChatRoomSession_FK_Session] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[Session]([SessionId]); 
ALTER TABLE [dbo].[ChatRoomSession] ADD CONSTRAINT [ChatRoomSession_UK_ChatRoomId_SessionId] UNIQUE ([ChatRoomId], [SessionId]);

CREATE TABLE [dbo].[ChatRoomEventType](
   [ChatRoomEventTypeId] INT NOT NULL IDENTITY,
   [Identifier] VARCHAR(32) NOT NULL,
   [Description] VARCHAR(128) NOT NULL
);
ALTER TABLE [dbo].[ChatRoomEventType] ADD CONSTRAINT [ChatRoomEventType_PK] PRIMARY KEY ([ChatRoomEventTypeId]);
ALTER TABLE [dbo].[ChatRoomEventType] ADD CONSTRAINT [ChatRoomEventType_UK_Identifier] UNIQUE ([Identifier]);

INSERT INTO [dbo].[ChatRoomEventType]([Description], [Identifier])
VALUES ('Chat was created', 'CreateChat'), 
       ('Message was sent', 'PostMessage'), 
       ('Chat was terminated', 'TerminateChat');

CREATE TABLE [dbo].[ChatRoomEvent](
   [ChatRoomEventId] INT NOT NULL IDENTITY,
   [ChatRoomId] INT NOT NULL,
   [ChatRoomEventTypeId] INT NOT NULL,
   [SessionId] INT NOT NULL,
   [CreatedAt] DATETIMEOFFSET NOT NULL,
   [Data] VARCHAR(MAX) NULL
);
ALTER TABLE [dbo].[ChatRoomEvent] ADD CONSTRAINT [ChatRoomEvent_PK] PRIMARY KEY ([ChatRoomEventId]);
ALTER TABLE [dbo].[ChatRoomEvent] ADD CONSTRAINT [ChatRoomEvent_FK_ChatRoom] FOREIGN KEY ([ChatRoomId]) REFERENCES [dbo].[ChatRoom]([ChatRoomId]); 
ALTER TABLE [dbo].[ChatRoomEvent] ADD CONSTRAINT [ChatRoomEvent_FK_ChatRoomEventType] FOREIGN KEY ([ChatRoomEventTypeId]) REFERENCES [dbo].[ChatRoomEventType]([ChatRoomEventTypeId]); 
ALTER TABLE [dbo].[ChatRoomEvent] ADD CONSTRAINT [ChatRoomEvent_FK_Session] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[Session]([SessionId]); 
ALTER TABLE [dbo].[ChatRoomEvent] ADD CONSTRAINT [ChatRoomEvent_CK_Data] CHECK (ISJSON([Data])=1);