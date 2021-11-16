ALTER TABLE [dbo].[ChatRoomSession] ADD [Token] VARCHAR(64) NULL;
GO
UPDATE [dbo].[ChatRoomSession] SET [Token] = NEWID() WHERE [Token] IS NULL;
GO
ALTER TABLE [dbo].[ChatRoomSession] ALTER COLUMN  [Token] VARCHAR(64) NOT NULL;
ALTER TABLE [dbo].[ChatRoomSession] ADD CONSTRAINT [ChatRoomSession_UK_Token] UNIQUE ([Token]);

ALTER TABLE [dbo].[Session] DROP COLUMN [IpAddress];
ALTER TABLE [dbo].[Session] ADD [LocalIpAddress] VARCHAR(16) NULL; 
GO
UPDATE [dbo].[Session] SET [LocalIpAddress] = '::1';
ALTER TABLE [dbo].[Session] ALTER COLUMN [LocalIpAddress] VARCHAR(16) NOT NULL;

ALTER TABLE [dbo].[Session] ADD [LocalPort] INT NULL; 
GO
UPDATE [dbo].[Session] SET [LocalPort] = 80;
ALTER TABLE [dbo].[Session] ALTER COLUMN [LocalPort] INT NOT NULL;

ALTER TABLE [dbo].[Session] ADD [RemoteIpAddress] VARCHAR(16) NULL; 
GO
UPDATE [dbo].[Session] SET [RemoteIpAddress] = '::1';
ALTER TABLE [dbo].[Session] ALTER COLUMN [RemoteIpAddress] VARCHAR(16) NOT NULL;

ALTER TABLE [dbo].[Session] ADD [RemotePort] INT NULL; 
GO
UPDATE [dbo].[Session] SET [RemotePort] = 80;
ALTER TABLE [dbo].[Session] ALTER COLUMN [RemotePort] INT NOT NULL;

ALTER TABLE [dbo].[Session] ADD [ConnectionIdentifier] VARCHAR(32) NULL; 
GO
UPDATE [dbo].[Session] SET [ConnectionIdentifier] = '0000000000000';
ALTER TABLE [dbo].[Session] ALTER COLUMN [ConnectionIdentifier] VARCHAR(32) NOT NULL;

ALTER TABLE [dbo].[Session] ALTER COLUMN [UserAgent] VARCHAR(256) NOT NULL;
