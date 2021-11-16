/** Create a psychiatrist (psyc1)  **/
IF (SELECT COUNT([UserName]) FROM [dbo].[User] WHERE [UserName] = 'psyc1@test.com') = 0
BEGIN
	INSERT INTO [dbo].[User]([UserName], [EncryptKey]) VALUES('psyc1@test.com', NEWID());
	INSERT INTO [dbo].[Person]([Name], [UserId]) VALUES('Fernanda', (SELECT [UserId] FROM [dbo].[User] WHERE [UserName] = 'psyc1@test.com'));
	INSERT INTO [dbo].[UserRole]([UserId], [RoleId]) VALUES(
		(SELECT [UserId] FROM [dbo].[User] WHERE [UserName] = 'psyc1@test.com'), 
		(SELECT [RoleId] FROM [dbo].[Role] WHERE [Identifier] = 'PSYCHIATRIST')
	);
END

/** Update all passwords in the User table  **/
UPDATE [dbo].[User] SET [Password] = CONVERT(VARCHAR(MAX), HashBytes('SHA2_256', CONCAT('1234', [EncryptKey])), 1) WHERE [Password] IS NULL;