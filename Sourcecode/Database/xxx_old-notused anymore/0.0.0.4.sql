DROP TABLE [dbo].[UserSession];
ALTER TABLE [dbo].[Session] DROP COLUMN [ConnectionIdentifier];


ALTER TABLE [dbo].[ChatRoom] ADD [Identifier] VARCHAR(64) NULL;
GO
UPDATE [dbo].[ChatRoom] SET [Identifier] = NEWID();
GO
ALTER TABLE [dbo].[ChatRoom] ALTER COLUMN [Identifier] VARCHAR(64) NULL;
GO
ALTER TABLE [dbo].[ChatRoom] ADD CONSTRAINT [ChatRoom_UK_Identifier] UNIQUE ([Identifier]);