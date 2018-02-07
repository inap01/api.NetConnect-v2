--USE master
--GO
--
--ALTER DATABASE [NetConnect] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--GO
--
--ALTER DATABASE [NetConnect] SET OFFLINE;
--GO

USE master
GO

DROP DATABASE NetConnect
GO

USE master
GO

CREATE DATABASE NetConnect
GO

USE NetConnect
GO

--CateringOrder.OrderState => -1: Storniert, 0: Neu, 1: Bezahlt, 2: Fertig
CREATE TABLE [dbo].[CateringOrder] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[EventID] int NOT NULL,
	[UserID] int NOT NULL,
	[SeatID] int NOT NULL,
	[Registered] datetime NOT NULL DEFAULT GETDATE(),
	[OrderState] int NOT NULL DEFAULT 0 CHECK (OrderState in (-1, 0, 1, 2)),
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[CateringOrderDetail] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[CateringOrderID] int NOT NULL,
	[CateringProductID] int NOT NULL,
	[Attributes] text,
	[Amount] int NOT NULL DEFAULT 1,
	[RowVersion] timestamp NOT NULL
);
  
CREATE TABLE [dbo].[CateringProduct] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] text NOT NULL,
	[Image] varchar(MAX) NOT NULL,
	[Price] decimal(10,2) NOT NULL,
	[SingleChoice] bit NOT NULL DEFAULT 0,
	[IsActive] bit NOT NULL DEFAULT 1,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[CateringProductAttribute] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(250) NOT NULL unique,
	[IsActive] bit NOT NULL DEFAULT 1,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[CateringProductAttributeRelation] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[CateringProductID] int NOT NULL,
	[CateringProductAttributeID] int NOT NULL,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[Event] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[EventTypeID] int NOT NULL,
	[Volume] int NOT NULL,
	[Image] varchar(MAX),
	[Start] datetime NOT NULL,
	[End] datetime NOT NULL,
	[ReservationCost] FLOAT NOT NULL,
	[IsActiveReservation] bit NOT NULL DEFAULT 0,
	[IsActiveCatering] bit NOT NULL DEFAULT 0,
	[IsActiveFeedback] bit NOT NULL DEFAULT 0,
	[IsPrivate] bit NOT NULL DEFAULT 0,
	[FeedbackLink] VARCHAR(255) NOT NULL,
	[District] varchar(80) NOT NULL,
	[Street] varchar(80) NOT NULL,
	[Housenumber] varchar(5) NOT NULL,
	[Postcode] varchar(10) NOT NULL,
	[City] varchar(80) NOT NULL,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[EventType] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(255) NOT NULL,
	[PublicAccess] bit NOT NULL DEFAULT 1,
	[Description] text NOT NULL,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[FaqQuestion] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[FaqCategoryID] int NOT NULL,
	[Question] text NOT NULL,
	[Answer] text NOT NULL,
	[Position] int NOT NULL,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[FaqCategory] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(250) NOT NULL,
	[Position] int NOT NULL,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[Logs] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[UserID] int NOT NULL,
	[SQLTable] varchar(50) NOT NULL,
	[SQLActionType] varchar(50) NOT NULL,
	[SQLQuery] text NOT NULL,
	[ModelBefore] text NOT NULL,
	[ModelAfter] text NOT NULL,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[News] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[NewsCategoryID] int NOT NULL,
	[Title] varchar(100) NOT NULL,
	[Image] varchar(MAX),
	[Date] datetime NOT NULL DEFAULT GETDATE(),
	[Text] text NOT NULL,
	[IsFeatured] bit NOT NULL DEFAULT 0,
	[IsActive] bit NOT NULL DEFAULT 1,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[NewsCategory] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(100) NOT NULL,
	[Image] varchar(MAX) NOT NULL,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[Partner] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(50) NOT NULL,
	[Link] varchar(MAX) NOT NULL,
	[RefLink] varchar(MAX),
	[Content] text,
	[ImageOriginal] varchar(MAX) NOT NULL,
	[ImagePassive] varchar(MAX) NOT NULL,
	[PartnerPackID] int NOT NULL DEFAULT 1,
	[IsActive] bit NOT NULL DEFAULT 0,
	[Position] int NOT NULL DEFAULT 0,
	[ClickCount] int NOT NULL DEFAULT 0,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[PartnerDisplay] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(MAX) NOT NULL,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[PartnerDisplayRelation] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[PartnerID] int NOT NULL,
	[PartnerDisplayID] int NOT NULL,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[PartnerPack] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(50) NOT NULL,
	[IsActive] bit NOT NULL DEFAULT 1,
	[RowVersion] timestamp NOT NULL
);

-- Seat.State => -1: Gesperrt, 1: Vorgemerkt, 2: Reserviert, 3: NetConnect
CREATE TABLE [dbo].[Seat] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[SeatNumber] int NOT NULL,
	[EventID] int NOT NULL,
	[UserID] INT NOT NULL DEFAULT 0,
	[State] INT NOT NULL CHECK ([State] in (-1, 1, 2, 3)),
	[Description] text NOT NULL,
	[ReservationDate] datetime NOT NULL DEFAULT GETDATE(),
	[Payed] bit NOT NULL DEFAULT 0,
	[IsActive] bit NOT NULL DEFAULT 1,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[SeatTransferLog] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[SeatID] int NOT NULL,
	[SourceUserID] int NOT NULL,
	[DestinationUserID] int NOT NULL,
	[TransferDate] datetime NOT NULL DEFAULT GETDATE(),
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[Tournament] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[EventID] INT NOT NULL,
	[TournamentGameID] INT NOT NULL,
	[PartnerID] INT DEFAULT 0,
	[TeamSize] INT NOT NULL DEFAULT 1,
	[ChallongeLink] varchar(MAX),
	[Mode] VARCHAR(10) NOT NULL,
	[Start] datetime NOT NULL,
	[End] datetime,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[TournamentGame] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL,
	[Image] varchar(MAX) NOT NULL,
	[Rules] text NOT NULL,
	[RequireBattleTag] bit NOT NULL DEFAULT 0,
	[RequireSteamID] bit NOT NULL DEFAULT 0,
	[IsActive] bit NOT NULL DEFAULT 1,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[TournamentParticipant] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[UserID] INT NOT NULL,
	[TournamentID] int NOT NULL,
	[Registered] datetime NOT NULL,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[TournamentTeam] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(250) NOT NULL,
	[TournamentID] INT NOT NULL,
	[Password] VARCHAR(100),
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[TournamentTeamParticipant] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[UserID] INT NOT NULL,
	[TournamentTeamID] INT NOT NULL,
	[Registered] datetime NOT NULL,
	[RowVersion] timestamp NOT NULL
);

-- TournamentWinner.Placement => "Siegertreppchen"
CREATE TABLE [dbo].[TournamentWinner] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[TournamentID] int NOT NULL,
	[Image] varchar(MAX) NOT NULL,
	[Placement] int NOT NULL CHECK ([Placement] in (1, 2, 3)),
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[TournamentWinnerPlayer] (
	[TournamentWinnerID] INT PRIMARY KEY,
	[UserID] int NOT NULL,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[TournamentWinnerTeam] (
	[TournamentWinnerID] INT PRIMARY KEY,
	[TournamentTeamID] int NOT NULL,
	[RowVersion] timestamp NOT NULL
);

-- User.CEO => "Vorstand"
CREATE TABLE [dbo].[User] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[FirstName] varchar(255) NOT NULL,
	[LastName] varchar(255) NOT NULL,
	[Nickname] varchar(255) NOT NULL,
	[Email] varchar(255) NOT NULL UNIQUE,
	[Password] varchar(MAX) NOT NULL,
	[PasswordSalt] varchar(64) NOT NULL,
	[PasswordReset] varchar(64),
	[Registered] datetime NOT NULL DEFAULT GETDATE(),
	[Image] varchar(MAX),
	[IsTeam] bit NOT NULL DEFAULT 0,
	[IsAdmin] bit NOT NULL DEFAULT 0,
	[CEO] int CHECK (CEO in (1, 2, 3, 4, 5)),
	[IsActive] bit NOT NULL DEFAULT 1,
	[SteamID] varchar(25),
	[BattleTag] varchar(25),
	[Newsletter] bit NOT NULL DEFAULT 1,
	[RowVersion] timestamp NOT NULL,
);

CREATE TABLE [dbo].[UserTask] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Description] varchar(50) NOT NULL UNIQUE,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[UserTaskRelation] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[UserID] int NOT NULL,
	[UserTaskID] int NOT NULL,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[UserRole] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(250) NOT NULL UNIQUE,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[UserPrivilege] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(250) NOT NULL UNIQUE,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[UserPrivilegeRelation] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[UserID] int NOT NULL,
	[UserPrivilegeID] int NOT NULL,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[UserRolePrivilegeRelation] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[UserRoleID] int NOT NULL,
	[UserPrivilegeID] int NOT NULL,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[ChangeSet] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[CateringProduct] DateTime,
	[CateringOrder] DateTime,
	[CateringOrderDetail] DateTime,
	[CateringProductAttribute] DateTime,
	[CateringProductAttributeRelation] DateTime,
	[Event] DateTime,
	[EventType] DateTime,
	[FaqQuestion] DateTime,
	[FaqCategory] DateTime,
	[Logs] DateTime,
	[Partner] DateTime,
	[PartnerDisplay] DateTime,
	[PartnerDisplayRelation] DateTime,
	[PartnerPack] DateTime,
	[Seat] DateTime,
	[SeatTransferLog] DateTime,
	[Tournament] DateTime,
	[TournamentGame] DateTime,
	[TournamentTeam] DateTime,	
	[TournamentParticipant] DateTime,
	[TournamentWinner] DateTime,
	[TournamentWinnerPlayer] DateTime,
	[TournamentWinnerTeam] DateTime,
	[User] DateTime,
	[UserTask] DateTime,
	[UserTaskRelation] DateTime,
	[UserRole] DateTime,
	[UserPrivilege] DateTime,
	[UserPrivilegeRelation] DateTime,
	[UserRolePrivilegeRelation] DateTime,
	[RowVersion] rowversion
);
GO


-- [dbo].[CateringProductAttributeRelation]
ALTER TABLE [dbo].[CateringProductAttributeRelation] ADD CONSTRAINT [FK_CateringProductAttributeRelation_CateringProductID] FOREIGN KEY (CateringProductID) REFERENCES [dbo].[CateringProduct](ID);
ALTER TABLE [dbo].[CateringProductAttributeRelation] ADD CONSTRAINT [FK_CateringProductAttributeRelation_CateringProductAttributeID] FOREIGN KEY (CateringProductAttributeID) REFERENCES [dbo].[CateringProductAttribute](ID);

-- [dbo].[CateringOrder]
ALTER TABLE [dbo].[CateringOrder] ADD CONSTRAINT [FK_CateringOrder_EventID] FOREIGN KEY (EventID) REFERENCES [dbo].[Event](ID);
ALTER TABLE [dbo].[CateringOrder] ADD CONSTRAINT [FK_CateringOrder_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User](ID);
ALTER TABLE [dbo].[CateringOrder] ADD CONSTRAINT [FK_CateringOrder_SeatID] FOREIGN KEY (SeatID) REFERENCES [dbo].[Seat](ID);

-- [dbo].[CateringOrderDetail]
ALTER TABLE [dbo].[CateringOrderDetail] ADD CONSTRAINT [FK_CateringOrderDetail_CateringOrderID] FOREIGN KEY (CateringOrderID) REFERENCES [dbo].[CateringOrder](ID);
ALTER TABLE [dbo].[CateringOrderDetail] ADD CONSTRAINT [FK_CateringOrderDetail_CateringProductID] FOREIGN KEY (CateringProductID) REFERENCES [dbo].[CateringProduct](ID);

-- [dbo].[Event]
ALTER TABLE [dbo].[Event] ADD CONSTRAINT [FK_Event_EventTypeID] FOREIGN KEY (EventTypeID) REFERENCES [dbo].[EventType](ID);

-- [dbo].[FaqQuestion]
ALTER TABLE [dbo].[FaqQuestion] ADD CONSTRAINT [FK_FaqQuestion_FaqCategoryID] FOREIGN KEY (FaqCategoryID) REFERENCES [dbo].[FaqCategory](ID) ON DELETE CASCADE;

-- [dbo].[Logs]
ALTER TABLE [dbo].[Logs] ADD CONSTRAINT [FK_Logs_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User](ID);

-- [dbo].[News]
ALTER TABLE [dbo].[News] ADD CONSTRAINT [FK_News_NewsCategoryID] FOREIGN KEY (NewsCategoryID) REFERENCES [dbo].[NewsCategory](ID);

-- [dbo].[Partner]
ALTER TABLE [dbo].[Partner] ADD CONSTRAINT [FK_Partner_PartnerPackID] FOREIGN KEY (PartnerPackID) REFERENCES [dbo].[PartnerPack](ID);

-- [dbo].[PartnerDetails]
ALTER TABLE [dbo].[PartnerDisplayRelation] ADD CONSTRAINT [FK_PartnerDisplayRelation_PartnerID] FOREIGN KEY (PartnerID) REFERENCES [dbo].[Partner](ID) ON DELETE CASCADE;
ALTER TABLE [dbo].[PartnerDisplayRelation] ADD CONSTRAINT [FK_PartnerDisplayRelation_PartnerDisplayID] FOREIGN KEY (PartnerDisplayID) REFERENCES [dbo].[PartnerDisplay](ID) ON DELETE CASCADE;

-- [dbo].[Seat]
ALTER TABLE [dbo].[Seat] ADD CONSTRAINT [FK_Seat_EventID] FOREIGN KEY (EventID) REFERENCES [dbo].[Event](ID);
ALTER TABLE [dbo].[Seat] ADD CONSTRAINT [FK_Seat_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User](ID);

-- [dbo].[SeatTransferLog]
ALTER TABLE [dbo].[SeatTransferLog] ADD CONSTRAINT [FK_SeatTransferLog_SeatID] FOREIGN KEY (SeatID) REFERENCES [dbo].[Seat](ID) ON DELETE CASCADE;
ALTER TABLE [dbo].[SeatTransferLog] ADD CONSTRAINT [FK_SeatTransferLog_SourceUserID] FOREIGN KEY (SourceUserID) REFERENCES [dbo].[User](ID);
ALTER TABLE [dbo].[SeatTransferLog] ADD CONSTRAINT [FK_SeatTransferLog_DestinationUserID] FOREIGN KEY (DestinationUserID) REFERENCES [dbo].[User](ID);

-- [dbo].[Tournament]
ALTER TABLE [dbo].[Tournament] ADD CONSTRAINT [FK_Tournament_EventID] FOREIGN KEY (EventID) REFERENCES [dbo].[Event](ID);
ALTER TABLE [dbo].[Tournament] ADD CONSTRAINT [FK_Tournament_TournamentGameID] FOREIGN KEY (TournamentGameID) REFERENCES [dbo].[TournamentGame](ID);
ALTER TABLE [dbo].[Tournament] ADD CONSTRAINT [FK_Tournament_PartnerID] FOREIGN KEY (PartnerID) REFERENCES [dbo].[Partner](ID);

-- [dbo].[TournamentParticipant]
ALTER TABLE [dbo].[TournamentParticipant] ADD CONSTRAINT [FK_TournamentParticipant_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User](ID);
ALTER TABLE [dbo].[TournamentParticipant] ADD CONSTRAINT [FK_TournamentParticipant_TournamentID] FOREIGN KEY (TournamentID) REFERENCES [dbo].[Tournament](ID) ON DELETE CASCADE;

-- [dbo].[TournamentTeam]
ALTER TABLE [dbo].[TournamentTeam] ADD CONSTRAINT [FK_TournamentTeam_TournamentID] FOREIGN KEY (TournamentID) REFERENCES [dbo].[Tournament](ID) ON DELETE CASCADE;

-- [dbo].[TournamentTeamParticipant]
ALTER TABLE [dbo].[TournamentTeamParticipant] ADD CONSTRAINT [FK_TournamentTeamParticipant_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User](ID);
ALTER TABLE [dbo].[TournamentTeamParticipant] ADD CONSTRAINT [FK_TournamentTeamParticipant_TournamentTeamID] FOREIGN KEY (TournamentTeamID) REFERENCES [dbo].[TournamentTeam](ID) ON DELETE CASCADE;

-- [dbo].[TournamentWinner]
ALTER TABLE [dbo].[TournamentWinner] ADD CONSTRAINT [FK_TournamentWinner_TournamentID] FOREIGN KEY (TournamentID) REFERENCES [dbo].[Tournament](ID) ON DELETE CASCADE;

-- [dbo].[TournamentWinnerPlayer]
ALTER TABLE [dbo].[TournamentWinnerPlayer] ADD CONSTRAINT [FK_TournamentWinnerPlayer_TournamentWinnerID] FOREIGN KEY (TournamentWinnerID) REFERENCES [dbo].[TournamentWinner](ID) ON DELETE CASCADE;
ALTER TABLE [dbo].[TournamentWinnerPlayer] ADD CONSTRAINT [FK_TournamentWinnerPlayer_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User](ID) ON DELETE CASCADE;

-- [dbo].[TournamentWinnerTeam]
ALTER TABLE [dbo].[TournamentWinnerTeam] ADD CONSTRAINT [FK_TournamentWinnerTeam_TournamentWinnerID] FOREIGN KEY (TournamentWinnerID) REFERENCES [dbo].[TournamentWinner](ID) ON DELETE CASCADE;
ALTER TABLE [dbo].[TournamentWinnerTeam] ADD CONSTRAINT [FK_TournamentWinnerTeam_TournamentTeamID] FOREIGN KEY (TournamentTeamID) REFERENCES [dbo].[TournamentTeam](ID);

-- [dbo].[UserTaskRelation]
ALTER TABLE [dbo].[UserTaskRelation] ADD CONSTRAINT [FK_UserTaskRelation_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User](ID);
ALTER TABLE [dbo].[UserTaskRelation] ADD CONSTRAINT [FK_UserTaskRelation_UserTaskID] FOREIGN KEY (UserTaskID) REFERENCES [dbo].[UserTask](ID);

-- [dbo].[UserPrivilegeRelation]
ALTER TABLE [dbo].[UserPrivilegeRelation] ADD CONSTRAINT [FK_UserPrivilegeRelation_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User](ID);
ALTER TABLE [dbo].[UserPrivilegeRelation] ADD CONSTRAINT [FK_UserPrivilegeRelation_UserPrivilegeID] FOREIGN KEY (UserID) REFERENCES [dbo].[UserPrivilege](ID);

-- [dbo].[UserRolePrivilegeRelation]
ALTER TABLE [dbo].[UserRolePrivilegeRelation] ADD CONSTRAINT [FK_UserRolePrivilegeRelation_UserID] FOREIGN KEY (UserRoleID) REFERENCES [dbo].[UserRole](ID);
ALTER TABLE [dbo].[UserRolePrivilegeRelation] ADD CONSTRAINT [FK_UserRolePrivilegeRelation_UserPrivilegeID] FOREIGN KEY (UserPrivilegeID) REFERENCES [dbo].[UserPrivilege](ID);

GO


-- TRIGGER fix
INSERT INTO ChangeSet(CateringProduct)VALUES(NULL)

-- DEFAULT USER
INSERT INTO dbo.[User] (FirstName, LastName, Nickname, Email, [Password], PasswordSalt, Newsletter)
VALUES ('Bestellung', 'Theke', 'BestellungTheke', 'bestellung.theke@lan-netconnect.de', 'qweasd', '123456', 0)
GO

-- EVENTS erstellen
INSERT INTO dbo.EventType ([Name], [PublicAccess], [Description])
VALUES ('Playground', 1, ''), 
       ('NetConnect & Friends', 0, '')
GO

INSERT INTO dbo.[Event] ([EventTypeID], [Volume], [Start], [End], [ReservationCost], [IsActiveReservation], [IsActiveCatering], [IsActiveFeedback], [FeedbackLink], [District], [Street], [Housenumber], [Postcode], [City])
VALUES (1, 1, '20140704', '20140706', 15, 1, 0, 0, 'http://lan-netconnect.de', 'Koerrenzig', 'Hauptstrasse', '91', '52441', 'Linnich'),
       (1, 2, '20141107', '20141109', 15, 1, 0, 0, 'http://lan-netconnect.de', 'Koerrenzig', 'Hauptstrasse', '91', '52441', 'Linnich'),
       (1, 3, '20150417', '20150419', 15, 1, 0, 0, 'http://lan-netconnect.de', 'Koerrenzig', 'Hauptstrasse', '91', '52441', 'Linnich'),
       (1, 4, '20150918', '20150920', 15, 1, 0, 0, 'http://lan-netconnect.de', 'Koerrenzig', 'Hauptstrasse', '91', '52441', 'Linnich'),
       (1, 5, '20151211', '20151213', 15, 1, 0, 0, 'http://lan-netconnect.de', 'Koerrenzig', 'Hauptstrasse', '91', '52441', 'Linnich'),
       (1, 6, '20160930', '20160930', 15, 1, 0, 0, 'http://lan-netconnect.de', 'Koerrenzig', 'Hauptstrasse', '91', '52441', 'Linnich'),
       (1, 7, '20170317', '20170319', 15, 1, 0, 0, 'http://lan-netconnect.de', 'Koerrenzig', 'Hauptstrasse', '91', '52441', 'Linnich'),
       (1, 8, '20170908', '20170910', 15, 1, 0, 0, 'http://lan-netconnect.de', 'Koerrenzig', 'Hauptstrasse', '91', '52441', 'Linnich'),
	   (2, 1, '20171215', '20171217', 10, 1, 0, 0, 'http://lan-netconnect.de', 'Koerrenzig', 'Hauptstrasse', '91', '52441', 'Linnich'),
	   (1, 9, '20180309', '20180311', 15, 1, 0, 0, 'http://lan-netconnect.de', 'Koerrenzig', 'Hauptstrasse', '91', '52441', 'Linnich')
GO

INSERT INTO dbo.NewsCategory ([Name], [Image])
VALUES ('NetConnect', 'newscategory/default.png'),
	   ('Playground', 'newscategory/default.png'),
	   ('Sponsoren', 'newscategory/default.png')
GO

INSERT INTO dbo.News ([NewsCategoryID], [Title], [Text])
VALUES (1, 'Beispiel News Titel', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.'),
	   (1, 'Beispiel News Titel', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.'),
	   (1, 'Beispiel News Titel', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.'),
	   (1, 'Beispiel News Titel', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.'),
	   (1, 'Beispiel News Titel', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.'),
	   (1, 'Beispiel News Titel', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.')
GO

INSERT INTO dbo.PartnerDisplay([Name])
VALUES ('Header'), ('Footer')
GO


-- Trigger ChangeSet
IF (OBJECT_ID(N'[dbo].[LimitChangeSet]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[LimitChangeSet];
END;
go

create trigger [LimitChangeSet]
on [dbo].[ChangeSet]
after insert
as
    declare @tableCount int
    select @tableCount = Count(*)
    from [dbo].[ChangeSet]

    if @tableCount > 1
    begin
        rollback
    end
go

IF (OBJECT_ID(N'[dbo].[UpdateCateringOrder]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateCateringOrder];
END;
go

create trigger [dbo].[UpdateCateringOrder]
on [dbo].[CateringOrder]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET CateringOrder = @date
go

IF (OBJECT_ID(N'[dbo].[UpdateCateringOrderDetail]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateCateringOrderDetail];
END;
go

create trigger [dbo].[UpdateCateringOrderDetail]
on [dbo].[CateringOrderDetail]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET CateringOrderDetail = @date
go

IF (OBJECT_ID(N'[dbo].[UpdateCateringProduct]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateCateringProduct];
END;
go

create trigger [dbo].[UpdateCateringProduct]
on [dbo].[CateringProduct]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET CateringProduct = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateCateringProductAttribute]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateCateringProductAttribute];
END;
go

create trigger [dbo].[UpdateCateringProductAttribute]
on [dbo].[CateringProductAttribute]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET CateringProductAttribute = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateCateringProductAttributeRelation]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateCateringProductAttributeRelation];
END;
go

create trigger [dbo].[UpdateCateringProductAttributeRelation]
on [dbo].[CateringProductAttributeRelation]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET CateringProductAttributeRelation = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateEventType]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateEventType];
END;
go

create trigger [dbo].[UpdateEventType]
on [dbo].[EventType]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET [EventType] = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateFaqQuestion]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateFaqQuestion];
END;
go

create trigger [dbo].[UpdateFaqQuestion]
on [dbo].[FaqQuestion]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET [FaqQuestion] = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateFaqCategory]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateFaqCategory];
END;
go

create trigger [dbo].[UpdateFaqCategory]
on [dbo].[FaqCategory]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET [FaqCategory] = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateLogs]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateLogs];
END;
go


create trigger [dbo].[UpdateLogs]
on [dbo].[Logs]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET Logs = @date
go


IF (OBJECT_ID(N'[dbo].[UpdatePartner]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdatePartner];
END;
go


create trigger [dbo].[UpdatePartner]
on [dbo].[Partner]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET [Partner] = @date
go


IF (OBJECT_ID(N'[dbo].[UpdatePartnerDisplay]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdatePartnerDisplay];
END;
go


create trigger [dbo].[UpdatePartnerDisplay]
on [dbo].[PartnerDisplay]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET [PartnerDisplay] = @date
go


IF (OBJECT_ID(N'[dbo].[UpdatePartnerDisplayRelation]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdatePartnerDisplayRelation];
END;
go


create trigger [dbo].[UpdatePartnerDisplayRelation]
on [dbo].[PartnerDisplayRelation]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET [PartnerDisplayRelation] = @date
go


IF (OBJECT_ID(N'[dbo].[UpdatePartnerPack]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdatePartnerPack];
END;
go

create trigger [dbo].[UpdatePartnerPack]
on [dbo].[PartnerPack]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET [PartnerPack] = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateSeat]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateSeat];
END;
go

create trigger [dbo].[UpdateSeat]
on [dbo].[Seat]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET Seat = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateSeatTransferLog]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateSeatTransferLog];
END;
go

create trigger [dbo].[UpdateSeatTransferLog]
on [dbo].[SeatTransferLog]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET SeatTransferLog = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateTournament]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateTournament];
END;
go

create trigger [dbo].[UpdateTournament]
on [dbo].[Tournament]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET Tournament = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateTournamentGame]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateTournamentGame];
END;
go

create trigger [dbo].[UpdateTournamentGame]
on [dbo].[TournamentGame]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET TournamentGame = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateTournamentParticipant]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateTournamentParticipant];
END;
go

create trigger [dbo].[UpdateTournamentParticipant]
on [dbo].[TournamentParticipant]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET TournamentParticipant = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateTournamentTeam]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateTournamentTeam];
END;
go


create trigger [dbo].[UpdateTournamentTeam]
on [dbo].[TournamentTeam]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET TournamentTeam = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateTournamentWinner]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateTournamentWinner];
END;
go

create trigger [dbo].[UpdateTournamentWinner]
on [dbo].[TournamentWinner]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET TournamentWinner = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateTournamentWinnerPlayer]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateTournamentWinnerPlayer];
END;
go

create trigger [dbo].[UpdateTournamentWinnerPlayer]
on [dbo].[TournamentWinnerPlayer]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET TournamentWinnerPlayer = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateTournamentWinnerTeam]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateTournamentWinnerTeam];
END;
go

create trigger [dbo].[UpdateTournamentWinnerTeam]
on [dbo].[TournamentWinnerTeam]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET TournamentWinnerTeam = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateUser]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateUser];
END;
go

create trigger [dbo].[UpdateUser]
on [dbo].[User]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET [User] = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateUserTask]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateUserTask];
END;
go

create trigger [dbo].[UpdateUserTask]
on [dbo].[UserTask]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET [UserTask] = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateUserTaskRelation]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateUserTaskRelation];
END;
go

create trigger [dbo].[UpdateUserTaskRelation]
on [dbo].[UserTaskRelation]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET [UserTaskRelation] = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateUserRole]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateUserRole];
END;
go

create trigger [dbo].[UpdateUserRole]
on [dbo].[UserRole]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET [UserRole] = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateUserPrivilege]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateUserPrivilege];
END;
go

create trigger [dbo].[UpdateUserPrivilege]
on [dbo].[UserPrivilege]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET [UserPrivilege] = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateUserPrivilegeRelation]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateUserPrivilegeRelation];
END;
go

create trigger [dbo].[UpdateUserPrivilegeRelation]
on [dbo].[UserPrivilegeRelation]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET [UserPrivilegeRelation] = @date
go


IF (OBJECT_ID(N'[dbo].[UpdateUserRolePrivilegeRelation]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateUserRolePrivilegeRelation];
END;
go

create trigger [dbo].[UpdateUserRolePrivilegeRelation]
on [dbo].[UserRolePrivilegeRelation]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET [UserRolePrivilegeRelation] = @date
go