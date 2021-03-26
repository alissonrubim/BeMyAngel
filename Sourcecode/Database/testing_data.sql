USE [BeMyAngel];

--Create user_manual, a regular patient 
INSERT INTO [dbo].[User]([Username]) VALUES('user_manuel');
INSERT INTO [dbo].[Person]([Name], [UserId]) VALUES('Manuel', @@IDENTITY);
INSERT INTO [dbo].[UserRole]([UserId], [RoleId]) 
VALUES((SELECT [UserId] FROM [dbo].[User] WHERE [Username] = 'user_manuel'), (SELECT [RoleId] FROM [dbo].[Role] WHERE [Identifier] = 'PATIENT'));


--Create user psyc_fernanda, a regular psychiatrist
INSERT INTO [dbo].[User]([Username]) VALUES('psyc_fernanda');
INSERT INTO [dbo].[Person]([Name], [UserId]) VALUES('Fernanda', @@IDENTITY);
INSERT INTO [dbo].[UserRole]([UserId], [RoleId]) 
VALUES((SELECT [UserId] FROM [dbo].[User] WHERE [Username] = 'psyc_fernanda'), (SELECT [RoleId] FROM [dbo].[Role] WHERE [Identifier] = 'PSYCHIATRIST'));
