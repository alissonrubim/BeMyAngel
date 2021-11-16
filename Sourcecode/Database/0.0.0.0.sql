/****** Object:  Table [dbo].[Chat]    Script Date: 09/04/2021 23:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chat](
	[ChatId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[TerminatedAt] [datetimeoffset](7) NULL,
	[Identifier] [varchar](64) NULL,
 CONSTRAINT [Chat_PK] PRIMARY KEY CLUSTERED 
(
	[ChatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Chat_UK_Identifier] UNIQUE NONCLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChatEvent]    Script Date: 09/04/2021 23:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatEvent](
	[ChatEventId] [int] IDENTITY(1,1) NOT NULL,
	[ChatId] [int] NOT NULL,
	[ChatEventTypeId] [int] NOT NULL,
	[ChatSessionId] [int] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[Data] [varchar](max) NULL,
 CONSTRAINT [ChatEvent_PK] PRIMARY KEY CLUSTERED 
(
	[ChatEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChatEventType]    Script Date: 09/04/2021 23:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatEventType](
	[ChatEventTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Identifier] [varchar](32) NOT NULL,
	[Description] [varchar](128) NOT NULL,
 CONSTRAINT [ChatEventType_PK] PRIMARY KEY CLUSTERED 
(
	[ChatEventTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [ChatEventType_UK_Identifier] UNIQUE NONCLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChatSession]    Script Date: 09/04/2021 23:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatSession](
	[ChatSessionId] [int] IDENTITY(1,1) NOT NULL,
	[ChatId] [int] NOT NULL,
	[SessionId] [int] NOT NULL,
	[Token] [varchar](64) NOT NULL,
 CONSTRAINT [ChatSession_PK] PRIMARY KEY CLUSTERED 
(
	[ChatSessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [ChatSession_UK_ChatId_SessionId] UNIQUE NONCLUSTERED 
(
	[ChatId] ASC,
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [ChatSession_UK_Token] UNIQUE NONCLUSTERED 
(
	[Token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 09/04/2021 23:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[PersonId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Name] [varchar](64) NOT NULL,
 CONSTRAINT [Person_PK] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Person_UK_UserId] UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 09/04/2021 23:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](64) NOT NULL,
	[Identifier] [varchar](32) NOT NULL,
 CONSTRAINT [Role_PK] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Role_UK_Identifier] UNIQUE NONCLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 09/04/2021 23:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[SessionId] [int] IDENTITY(1,1) NOT NULL,
	[Token] [varchar](64) NOT NULL,
	[UserAgent] [varchar](256) NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[LastAccessAt] [datetimeoffset](7) NOT NULL,
	[UserId] [int] NULL,
	[LocalIpAddress] [varchar](16) NOT NULL,
	[LocalPort] [int] NOT NULL,
	[RemoteIpAddress] [varchar](16) NOT NULL,
	[RemotePort] [int] NOT NULL,
 CONSTRAINT [Session_PK] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Session_UK_Token] UNIQUE NONCLUSTERED 
(
	[Token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Setting]    Script Date: 09/04/2021 23:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Setting](
	[SettingId] [int] IDENTITY(1,1) NOT NULL,
	[Identifier] [varchar](128) NOT NULL,
	[Value] [varchar](128) NULL,
 CONSTRAINT [Setting_PK] PRIMARY KEY CLUSTERED 
(
	[SettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Setting_UK_Identifier] UNIQUE NONCLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 09/04/2021 23:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](64) NOT NULL,
	[Password] [varchar](256) NULL,
	[EncryptKey] [varchar](246) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
 CONSTRAINT [User_PK] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [User_UK_UserName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 09/04/2021 23:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [UserRole_UK_UserId_RoleId] UNIQUE NONCLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Chat] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Session] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Session] ADD  DEFAULT (getdate()) FOR [LastAccessAt]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((1)) FOR [IsEnabled]
GO
ALTER TABLE [dbo].[ChatEvent]  WITH CHECK ADD  CONSTRAINT [ChatEvent_FK_Chat] FOREIGN KEY([ChatId])
REFERENCES [dbo].[Chat] ([ChatId])
GO
ALTER TABLE [dbo].[ChatEvent] CHECK CONSTRAINT [ChatEvent_FK_Chat]
GO
ALTER TABLE [dbo].[ChatEvent]  WITH CHECK ADD  CONSTRAINT [ChatEvent_FK_ChatEventType] FOREIGN KEY([ChatEventTypeId])
REFERENCES [dbo].[ChatEventType] ([ChatEventTypeId])
GO
ALTER TABLE [dbo].[ChatEvent] CHECK CONSTRAINT [ChatEvent_FK_ChatEventType]
GO
ALTER TABLE [dbo].[ChatEvent]  WITH CHECK ADD  CONSTRAINT [ChatEvent_FK_ChatSession] FOREIGN KEY([ChatSessionId])
REFERENCES [dbo].[ChatSession] ([ChatSessionId])
GO
ALTER TABLE [dbo].[ChatEvent] CHECK CONSTRAINT [ChatEvent_FK_ChatSession]
GO
ALTER TABLE [dbo].[ChatSession]  WITH CHECK ADD  CONSTRAINT [ChatSession_FK_Chat] FOREIGN KEY([ChatId])
REFERENCES [dbo].[Chat] ([ChatId])
GO
ALTER TABLE [dbo].[ChatSession] CHECK CONSTRAINT [ChatSession_FK_Chat]
GO
ALTER TABLE [dbo].[ChatSession]  WITH CHECK ADD  CONSTRAINT [ChatSession_FK_Session] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([SessionId])
GO
ALTER TABLE [dbo].[ChatSession] CHECK CONSTRAINT [ChatSession_FK_Session]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [Person_FK_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [Person_FK_User]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [Session_FK_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [Session_FK_User]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [UserRole_FK_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [UserRole_FK_Role]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [UserRole_FK_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [UserRole_FK_User]
GO
ALTER TABLE [dbo].[ChatEvent]  WITH CHECK ADD  CONSTRAINT [ChatEvent_CK_Data] CHECK  ((isjson([Data])=(1)))
GO
ALTER TABLE [dbo].[ChatEvent] CHECK CONSTRAINT [ChatEvent_CK_Data]
GO
