--Create user_manual, a regular patient 
INSERT INTO [dbo].[Person]([Name]) VALUES('Manuel');
INSERT INTO [dbo].[User]([Username], [PersonId]) VALUES('user_manuel', @@IDENTITY);
INSERT INTO [dbo].[UserRole]([UserId], [RoleId]) 
VALUES((SELECT [UserId] FROM [dbo].[User] WHERE [Username] = 'user_manuel'), (SELECT [RoleId] FROM [dbo].[Role] WHERE [Identifier] = 'PATIENT'));


--Create user psyc_fernanda, a regular psychiatrist
INSERT INTO [dbo].[Person]([Name]) VALUES('Fernanda');
INSERT INTO [dbo].[User]([Username], [PersonId]) VALUES('psyc_fernanda', @@IDENTITY);
INSERT INTO [dbo].[UserRole]([UserId], [RoleId]) 
VALUES((SELECT [UserId] FROM [dbo].[User] WHERE [Username] = 'psyc_fernanda'), (SELECT [RoleId] FROM [dbo].[Role] WHERE [Identifier] = 'PSYCHIATRIST'));




--------------------------------------------------------------------------------
/*
CREATE TABLE [dbo].[Session](	
   [SessionId] INT NOT NULL IDENTITY,
   [CreateDateTime] DATETIME NOT NULL,
   [ExpireAtDateTime] DATETIME NULL
   [UserId] INT NULL
);
ALTER TABLE [dbo].[Session] CREATE CONSTRAINT [Session_PK] PRIMARY KEY ([SessionId]);
ALTER TABLE [dbo].[Session] CREATE CONSTRIANT [Session_FK_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId]); */
