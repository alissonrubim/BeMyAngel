CREATE TABLE [dbo].[Role](
   [RoleId] INT NOT NULL IDENTITY,
   [Name] VARCHAR(64) NOT NULL,
   [Identifier] VARCHAR(32) NOT NULL
);
ALTER TABLE [dbo].[Role] CREATE CONSTRAINT [Role_PK] PRIMARY KEY ([RoleId]);
ALTER TABLE [dbo].[Role] CREATE CONSTRAINT [Role_UK_Identifier] UNIQUE KEY ([Identifier]);
INSERT INTO [dbo].[Role]([Name], [Identifier])
VALUES ('Patient', 'PATIENT'), ('Psychiatrist', 'PSYCHIATRIST'), ('System', 'SYSTEM');


CREATE TABLE [dbo].[User](
   [UserId] INT NOT NULL IDENTITY,
   [Username] VARCHAR(64) NOT NULL,
);
ALTER TABLE [dbo].[User] CREATE CONSTRAINT [User_PK] PRIMARY KEY ([UserId]);
ALTER TABLE [dbo].[User] CREATE CONSTRAINT [User_UK_Username] UNIQUE KEY ([Username]);

INSERT INTO [dbo].[User]([Username]) VALUES('system');


CREATE TABLE [dbo].[Person](
   [PersonId] INT NOT NULL IDENTITY,
   [UserId] INT NOT NULL,
   [Name] VARCHAR(64) NOT NULL

);
ALTER TABLE [dbo].[Person] CREATE CONSTRAINT [Person_PK] PRIMARY KEY ([PersonId]);
ALTER TABLE [dbo].[Person] CREATE CONSTRAINT [Person_FK_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Person]([UserId]); 
ALTER TABLE [dbo].[Person] CREATE CONSTRAINT [Person_UK_UserId] UNIQUE KEY ([UserId]);


CREATE TABLE [dbo].[UserRole](
   [UserId]
   [RoleId]
);
ALTER TABLE [dbo].[UserRole] CREATE CONSTRAINT [UserRole_FK_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId]); 
ALTER TABLE [dbo].[UserRole] CREATE CONSTRAINT [UserRole_FK_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role]([RoleId]); 
ALTER TABLE [dbo].[UserRole] CREATE CONSTRAINT [UserRole_UK_UserId_RoleId] UNIQUE KEY ([UserId], [RoleId]);

INSERT INTO [dbo].[UserRole]([UserId], [RoleId]) 
VALUES((SELECT [UserId] FROM [dbo].[User] WHERE [Username] = 'system'), (SELECT [RoleId] FROM [dbo].[Role] WHERE [Identifier] = 'SYSTEM'));


CREATE TABLE [dbo].[ChatRoom](
   [ChatRoomId] INT NOT NULL IDENTITY,
   [CreatedAtDateTime] DATETIME NOT NULL,
   [TerminatedAtDateTime] DATETIME NULL
);
ALTER TABLE [dbo].[ChatRoom] CREATE CONSTRAINT [ChatRoom_PK] PRIMARY KEY ([ChatRoomId]);

create table [dbo].[ChatRoomUser](
   [ChatRoomId]
   [UserId]
);
ALTER TABLE [dbo].[ChatRoomUser] CREATE CONSTRAINT [ChatRoomUser_FK_ChatRoom] FOREIGN KEY ([ChatRoomId]) REFERENCES [dbo].[ChatRoom]([ChatRoomId]); 
ALTER TABLE [dbo].[ChatRoomUser] CREATE CONSTRAINT [ChatRoomUser_FK_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId]); 
ALTER TABLE [dbo].[ChatRoomUser] CREATE CONSTRAINT [ChatRoomUser_UK_ChatRoomId_UserId] UNIQUE KEY ([ChatRoomId], [UserId]);

CREATE TABLE [dbo].[ChatRoomEventType](
   [ChatRoomEventType] INT NOT NULL IDENTITY,
   [Name] VARCHAR(64) NOT NULL,
   [Identifier] VARCHAR(32) NOT NULL
);
ALTER TABLE [dbo].[ChatRoomEvent] CREATE CONSTRAINT [ChatRoomEvent_PK] PRIMARY KEY ([ChatRoomEventType]);
ALTER TABLE [dbo].[ChatRoomEvent] CREATE CONSTRAINT [ChatRoomEvent_UK_Identifier] UNIQUE KEY ([Identifier]);

INSERT INTO [dbo].[ChatRoomEvent]([Name], [Identifier])
VALUES ('Create the chat room', 'CREATE_CHAT'), ('Send message to everyone at chat room', 'SEND_MESSAGE'), ('Terminate chat room', 'TERMINATE_CHAT');

CREATE TABLE [dbo].[ChatRoomEvent](
   [ChatRoomId] INT NOT NULL IDENTITY,
   [ChatRoomEventTypeId] INT NOT NULL,
   [UserId] DATETIME NOT NULL,
   [DataTime] DATETIME NOT NULL,
   [EventData] VARCHAR(MAX) NULL
);
ALTER TABLE [dbo].[ChatRoomEvent] CREATE CONSTRAINT [ChatRoomEvent_FK_ChatRoom] FOREIGN KEY ([ChatRoomId]) REFERENCES [dbo].[ChatRoom]([ChatRoomId]); 
ALTER TABLE [dbo].[ChatRoomEvent] CREATE CONSTRAINT [ChatRoomEvent_FK_ChatRoomEventType] FOREIGN KEY ([ChatRoomEventTypeId]) REFERENCES [dbo].[ChatRoomEventType]([ChatRoomEventTypeId]); 
ALTER TABLE [dbo].[ChatRoomEvent] CREATE CONSTRAINT [ChatRoomEvent_FK_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId]); 
ALTER TABLE [dbo].[ChatRoomEvent] ADD CONSTRAINT [ChatRoomEvent_CK_EventData] CHECK (ISJSON(EventData)=1);