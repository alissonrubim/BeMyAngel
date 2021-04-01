ALTER TABLE [dbo].[ChatRoomSession] ADD [Token] VARCHAR(64) NULL;
GO
UPDATE [dbo].[ChatRoomSession] SET [Token] = NEWID() WHERE [Token] IS NULL;
GO
ALTER TABLE [dbo].[ChatRoomSession] ALTER COLUMN  [Token] VARCHAR(64) NOT NULL;
ALTER TABLE [dbo].[ChatRoomSession] ADD CONSTRAINT [ChatRoomSession_UK_Token] UNIQUE ([Token]);
