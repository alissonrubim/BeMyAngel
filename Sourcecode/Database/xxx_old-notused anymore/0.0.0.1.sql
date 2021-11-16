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
   [UserName] VARCHAR(64) NOT NULL,
   [Password] VARCHAR(256) NULL,
   [EncryptKey] VARCHAR(246) NOT NULL,
   [IsEnabled] BIT NOT NULL DEFAULT 1
);
ALTER TABLE [dbo].[User] ADD CONSTRAINT [User_PK] PRIMARY KEY ([UserId]);
ALTER TABLE [dbo].[User] ADD CONSTRAINT [User_UK_UserName] UNIQUE ([UserName]);

INSERT INTO [dbo].[User]([UserName], [EncryptKey]) VALUES('system', NEWID());

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