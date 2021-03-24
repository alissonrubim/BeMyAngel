CREATE DATABASE [BeMyAngel];
GO
USE [BeMyAngel];
GO

CREATE TABLE [dbo].[Session](
  [SessionId] INT NOT NULL IDENTITY,
  [Token] VARCHAR(64) NOT NULL,
  [IpAddress] VARCHAR(16) NOT NULL,
  [UserAgent] VARCHAR(128) NULL
);
ALTER TABLE [dbo].[Session] ADD CONSTRAINT [Session_PK] PRIMARY KEY ([SessionId]);
ALTER TABLE [dbo].[Session] ADD CONSTRAINT [Session_UK_Token] UNIQUE ([Token]);


CREATE TABLE [dbo].[Role](
   [RoleId] INT NOT NULL IDENTITY,
   [Name] VARCHAR(64) NOT NULL,
   [Identifier] VARCHAR(32) NOT NULL
);
ALTER TABLE [dbo].[Role] ADD CONSTRAINT [Role_PK] PRIMARY KEY ([RoleId]);
ALTER TABLE [dbo].[Role] ADD CONSTRAINT [Role_UK_Identifier] UNIQUE ([Identifier]);
INSERT INTO [dbo].[Role]([Name], [Identifier])
VALUES ('Patient', 'PATIENT'), ('Psychiatrist', 'PSYCHIATRIST'), ('System', 'SYSTEM');


CREATE TABLE [dbo].[User](
   [UserId] INT NOT NULL IDENTITY,
   [Username] VARCHAR(64) NOT NULL,
);
ALTER TABLE [dbo].[User] ADD CONSTRAINT [User_PK] PRIMARY KEY ([UserId]);
ALTER TABLE [dbo].[User] ADD CONSTRAINT [User_UK_Username] UNIQUE ([Username]);

INSERT INTO [dbo].[User]([Username]) VALUES('system');


CREATE TABLE [dbo].[UserSession](
  [UserId] INT NOT NULL,
  [SessionId] INT NOT NULL
);
ALTER TABLE [dbo].[UserSession] ADD CONSTRAINT [UserSession_FK_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId]); 
ALTER TABLE [dbo].[UserSession] ADD CONSTRAINT [UserSession_FK_Session] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[Session]([SessionId]); 


CREATE TABLE [dbo].[Person](
   [PersonId] INT NOT NULL IDENTITY,
   [UserId] INT NULL,
   [Name] VARCHAR(64) NOT NULL
);
ALTER TABLE [dbo].[Person] ADD CONSTRAINT [Person_PK] PRIMARY KEY ([PersonId]);
ALTER TABLE [dbo].[Person] ADD CONSTRAINT [Person_FK_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId]); 
ALTER TABLE [dbo].[Person] ADD CONSTRAINT [Person_UK_UserId] UNIQUE ([UserId]);


CREATE TABLE [dbo].[UserRole](
   [UserId] INT NOT NULL,
   [RoleId] INT NOT NULL
);
ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [UserRole_FK_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId]); 
ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [UserRole_FK_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role]([RoleId]); 
ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [UserRole_UK_UserId_RoleId] UNIQUE ([UserId], [RoleId]);

INSERT INTO [dbo].[UserRole]([UserId], [RoleId]) 
VALUES((SELECT [UserId] FROM [dbo].[User] WHERE [Username] = 'system'), (SELECT [RoleId] FROM [dbo].[Role] WHERE [Identifier] = 'SYSTEM'));


CREATE TABLE [dbo].[ChatRoom](
   [ChatRoomId] INT NOT NULL IDENTITY,
   [CreatedAtDateTime] DATETIME NOT NULL,
   [TerminatedAtDateTime] DATETIME NULL
);
ALTER TABLE [dbo].[ChatRoom] ADD CONSTRAINT [ChatRoom_PK] PRIMARY KEY ([ChatRoomId]);
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