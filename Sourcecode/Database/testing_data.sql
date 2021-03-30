USE [BeMyAngel];

--Create user_manual, a regular patient 
INSERT INTO [dbo].[User]([Username], [EncryptKey]) VALUES('pat1@teste.com', NEWID());
INSERT INTO [dbo].[Person]([Name], [UserId]) VALUES('Manuel', @@IDENTITY);
INSERT INTO [dbo].[UserRole]([UserId], [RoleId]) 
VALUES((SELECT [UserId] FROM [dbo].[User] WHERE [Username] = 'pat1@teste.com'), (SELECT [RoleId] FROM [dbo].[Role] WHERE [Identifier] = 'PATIENT'));

--Create user psyc_fernanda, a regular psychiatrist
INSERT INTO [dbo].[User]([Username], [EncryptKey]) VALUES('psyc1@test.com', NEWID());
INSERT INTO [dbo].[Person]([Name], [UserId]) VALUES('Fernanda', @@IDENTITY);
INSERT INTO [dbo].[UserRole]([UserId], [RoleId]) 
VALUES((SELECT [UserId] FROM [dbo].[User] WHERE [Username] = 'psyc1@test.com'), (SELECT [RoleId] FROM [dbo].[Role] WHERE [Identifier] = 'PSYCHIATRIST'));

UPDATE [dbo].[User] SET [Password] = CONVERT(VARCHAR(MAX), HashBytes('SHA2_256', CONCAT('1234', [EncryptKey])), 1) WHERE [Password] IS NULL;