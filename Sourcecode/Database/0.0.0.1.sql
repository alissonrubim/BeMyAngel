ALTER TABLE [dbo].[ChatSession] ADD [ConnectionId] VARCHAR(64) NULL;

GO

ALTER TABLE [dbo].[ChatSession] ADD CONSTRAINT [ChatSession_UK_ConnectionId] UNIQUE ([ConnectionId]);