/*****************************************************************************************************************
	HOW TO USE: This script will run everytime that the database is updated with a new script.
	            So, make sure to CHECK IF THE DATA ALREADY EXISTS before trying to insert it.
*****************************************************************************************************************/

/** Insert Users **/
IF (SELECT COUNT([UserName]) FROM [dbo].[User] WHERE [UserName] = 'system') = 0
BEGIN
	INSERT INTO [dbo].[User]([UserName], [EncryptKey]) VALUES('system', NEWID());
END

/** Insert Roles **/
IF (SELECT COUNT([Identifier]) FROM [dbo].[Role] WHERE [Identifier] = 'PATIENT') = 0
BEGIN
	INSERT INTO [dbo].[Role]([Name], [Identifier]) VALUES('Patient', 'PATIENT');
END

IF (SELECT COUNT([Identifier]) FROM [dbo].[Role] WHERE [Identifier] = 'PATIENT') = 0
BEGIN
	INSERT INTO [dbo].[Role]([Name], [Identifier]) VALUES('Patient', 'PATIENT');
END

IF (SELECT COUNT([Identifier]) FROM [dbo].[Role] WHERE [Identifier] = 'PSYCHIATRIST') = 0
BEGIN
	INSERT INTO [dbo].[Role]([Name], [Identifier]) VALUES('Psychiatrist', 'PSYCHIATRIST');
END


/** Insert ChatEventTypes **/
IF (SELECT COUNT([Identifier]) FROM [dbo].[ChatEventType] WHERE [Identifier] = 'CREATECHAT') = 0
BEGIN
	INSERT INTO [dbo].[ChatEventType]([Description], [Identifier]) VALUES('Chat was created', 'CREATECHAT');
END

IF (SELECT COUNT([Identifier]) FROM [dbo].[ChatEventType] WHERE [Identifier] = 'POSTMESSAGE') = 0
BEGIN
	INSERT INTO [dbo].[ChatEventType]([Description], [Identifier]) VALUES('Message was sent', 'POSTMESSAGE');
END

IF (SELECT COUNT([Identifier]) FROM [dbo].[ChatEventType] WHERE [Identifier] = 'TERMINATECHAT') = 0
BEGIN
	INSERT INTO [dbo].[ChatEventType]([Description], [Identifier]) VALUES('Chat was terminated', 'TERMINATECHAT');
END


/*****************************************************************************************************************
	HOW TO USE: This script will run everytime that the database is updated with a new script.
	            So, make sure to CHECK IF THE DATA ALREADY EXISTS before trying to insert it.
*****************************************************************************************************************/