CREATE TABLE [dbo].[Setting](
  [SettingId] INT NOT NULL IDENTITY,
  [Identifier] VARCHAR(128) NOT NULL,
  [Value] VARCHAR(128) NULL
);
ALTER TABLE [dbo].[Setting] ADD CONSTRAINT [Setting_PK] PRIMARY KEY ([SettingId]);
ALTER TABLE [dbo].[Setting] ADD CONSTRAINT [Setting_UK_Identifier] UNIQUE ([Identifier]);