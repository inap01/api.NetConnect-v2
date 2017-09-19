Delete  From CateringOrder
DBCC CHECKIDENT ('[CateringOrder]', RESEED, 0);
GO

USE master
GO

ALTER DATABASE [NetConnect] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

ALTER DATABASE [NetConnect] SET OFFLINE;
GO

DROP DATABASE NetConnect
GO

USE master
GO

CREATE DATABASE NetConnect
GO

USE NetConnect
GO

CREATE TABLE [dbo].[User] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[FirstName] varchar(255) NOT NULL,
	[LastName] varchar(255) NOT NULL,
	[Nickname] varchar(255) NOT NULL,
	[Email] varchar(255) NOT NULL,
	[Password] varchar(64) NOT NULL,
	[PasswordReset] varchar(64),
	[Registered] datetime NOT NULL DEFAULT GETDATE(),
	[IsTeam] bit NOT NULL DEFAULT 0,
	[IsAdmin] bit NOT NULL DEFAULT 0,
	[IsCEO] bit NOT NULL DEFAULT 0,
	[IsActive] bit NOT NULL DEFAULT 1,
	[Image] varchar(255),
	[SteamID] varchar(25),
	[BattleTag] varchar(25),
	[Newsletter] bit NOT NULL DEFAULT 1,
	[LastChange] timestamp NOT NULL
);

CREATE TABLE [dbo].[CateringOrder] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Volume] int NOT NULL,
	[UserID] int NOT NULL,
	[SeatID] int NOT NULL,
	[CompletionState] int NOT NULL DEFAULT 0,
	[LastChange] timestamp NOT NULL
);

CREATE TABLE [dbo].[CateringOrderDetail] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[CateringOrderID] int NOT NULL,
	[CateringProductID] int NOT NULL,
	[Attributes] text,
	[LastChange] timestamp NOT NULL
);
  
CREATE TABLE [dbo].[CateringProduct] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] text NOT NULL,
	[Description] text,
	[Image] varchar(100) NOT NULL,
	[Price] decimal(10,2) NOT NULL,
	[IsActive] bit NOT NULL DEFAULT 1,
	[Attributes] text,
	[SingleChoice] bit NOT NULL DEFAULT 0,
	[LastChange] timestamp NOT NULL
);
  
CREATE TABLE [dbo].[Chat] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[UserID] int NOT NULL,
	[Message] text NOT NULL,
	[GameFlag] bit NOT NULL DEFAULT 0,
	[GameTitle] varchar(50) DEFAULT NULL,
	[LastChange] timestamp NOT NULL
);


CREATE TABLE [dbo].[Logs] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[UserID] int NOT NULL,
	[SQLTable] varchar(50) NOT NULL,
	[SQLActionType] varchar(50) NOT NULL,
	[SQLQuery] text NOT NULL,
	[ModelBefore] text NOT NULL,
	[ModelAfter] text NOT NULL,
	[LastChange] timestamp NOT NULL
);


CREATE TABLE [dbo].[Partner] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(50) NOT NULL,
	[Link] varchar(100) NOT NULL,
	[Content] text,
	[Image] varchar(50) NOT NULL,
	[ImageAlt] text NOT NULL,
	[PartnerPackID] int NOT NULL DEFAULT 1,
	[IsActive] bit NOT NULL DEFAULT 0,
	[Position] int NOT NULL DEFAULT 0,
	[ClickCount] int NOT NULL DEFAULT 0,
	[LastChange] timestamp NOT NULL
);

CREATE TABLE [dbo].[PartnerPack] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(50) NOT NULL,
	[IsActive] bit NOT NULL DEFAULT 1,
	[LastChange] timestamp NOT NULL
);

CREATE TABLE [dbo].[Seat] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[UserID] INT NOT NULL DEFAULT 0,
	[State] INT NOT NULL,
	[Description] text NOT NULL,
	[ReservationDate] datetime NOT NULL,
	[Payed] bit NOT NULL,
	[IsTeam] bit NOT NULL,
	[IsActive] bit NOT NULL DEFAULT 1,
	[LastChange] timestamp NOT NULL
);

CREATE TABLE [dbo].[Settings] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[Volume] INT NOT NULL,
	[ReservationCost] FLOAT NOT NULL,
	[Start] datetime NOT NULL,
	[End] datetime NOT NULL,
	[IsActiveReservation] INT NOT NULL DEFAULT 0,
	[BankAccountCheck] DATE NOT NULL,
	[ReservedDays] INT NOT NULL,
	[IsActiveCatering] bit NOT NULL,
	[IsActiveFeedback] bit NOT NULL,
	[FeedbackLink] VARCHAR(255) NOT NULL,
	[IsActiveChat] bit NOT NULL,
	[BankAccountOwner] VARCHAR(50) NOT NULL,
	[IBAN] VARCHAR(50) NOT NULL,
	[BLZ] VARCHAR(50) NOT NULL,
	[BankAccountNumber] VARCHAR(50) NOT NULL,
	[BIC] VARCHAR(50) NOT NULL,
	[LastChange] timestamp NOT NULL
);

CREATE TABLE [dbo].[Tournament] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[Volume] INT NOT NULL,
	[TournamentGameID] INT NOT NULL,
	[TeamSize] INT NOT NULL DEFAULT 1,
	[ChallongeLink] text NOT NULL,
	[Mode] VARCHAR(10) NOT NULL,
	[Start] datetime NOT NULL,
	[End] datetime,
	[IsPauseGame] bit NOT NULL DEFAULT 0,
	[PartnerID] INT DEFAULT 0,
	[LastChange] timestamp NOT NULL
);

CREATE TABLE [dbo].[TournamentGame] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL,
	[Icon] VARCHAR(50) NOT NULL,
	[Rules] text NOT NULL,
	[IsActive] bit NOT NULL DEFAULT 1,
	[BattleTag] bit NOT NULL DEFAULT 0,
	[SteamID] bit NOT NULL DEFAULT 0,
	[LastChange] timestamp NOT NULL
);

CREATE TABLE [dbo].[TournamentParticipant] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[UserID] INT NOT NULL,
	[TournamentID] int NOT NULL,
	[TournamentTeamID] INT NOT NULL DEFAULT 0,
	[Registered] datetime NOT NULL,
	[LastChange] timestamp NOT NULL
);

CREATE TABLE [dbo].[TournamentTeam] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(250) NOT NULL,
	[TournamentID] INT NOT NULL,
	[Password] VARCHAR(100) NOT NULL,
	[LastChange] timestamp NOT NULL
);

GO


-- [dbo].[CateringOrder]
ALTER TABLE [dbo].[CateringOrder] ADD CONSTRAINT [FK_CateringOrder_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User](ID);
ALTER TABLE [dbo].[CateringOrder] ADD CONSTRAINT [FK_CateringOrder_SeatID] FOREIGN KEY (SeatID) REFERENCES [dbo].[Seat](ID);

-- [dbo].[CateringOrderDetail]
ALTER TABLE [dbo].[CateringOrderDetail] ADD CONSTRAINT [FK_CateringOrderDetail_CateringOrderID] FOREIGN KEY (CateringOrderID) REFERENCES [dbo].[CateringOrder](ID);
ALTER TABLE [dbo].[CateringOrderDetail] ADD CONSTRAINT [FK_CateringOrderDetail_CateringProductID] FOREIGN KEY (CateringProductID) REFERENCES [dbo].[CateringProduct](ID);

-- [dbo].[Chat]
ALTER TABLE [dbo].[Chat] ADD CONSTRAINT [FK_Chat_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User](ID);

-- [dbo].[Logs]
ALTER TABLE [dbo].[Logs] ADD CONSTRAINT [FK_Logs_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User](ID);

-- [dbo].[Partner]
ALTER TABLE [dbo].[Partner] ADD CONSTRAINT [FK_Partner_PartnerPackID] FOREIGN KEY (PartnerPackID) REFERENCES [dbo].[PartnerPack](ID);

-- [dbo].[Seat]
ALTER TABLE [dbo].[Seat] ADD CONSTRAINT [FK_Seat_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User](ID);

-- [dbo].[Tournament]
ALTER TABLE [dbo].[Tournament] ADD CONSTRAINT [FK_Tournament_TournamentGameID] FOREIGN KEY (TournamentGameID) REFERENCES [dbo].[TournamentGame](ID);
ALTER TABLE [dbo].[Tournament] ADD CONSTRAINT [FK_Tournament_PartnerID] FOREIGN KEY (PartnerID) REFERENCES [dbo].[Partner](ID);

-- [dbo].[TournamentParticipant]
ALTER TABLE [dbo].[TournamentParticipant] ADD CONSTRAINT [FK_Tournament_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User](ID);
ALTER TABLE [dbo].[TournamentParticipant] ADD CONSTRAINT [FK_Tournament_TournamentID] FOREIGN KEY (TournamentID) REFERENCES [dbo].[Tournament](ID);
ALTER TABLE [dbo].[TournamentParticipant] ADD CONSTRAINT [FK_Tournament_TournamentTeamID] FOREIGN KEY (TournamentTeamID) REFERENCES [dbo].[TournamentTeam](ID);

-- [dbo].[TournamentTeam]
ALTER TABLE [dbo].[TournamentTeam] ADD CONSTRAINT [FK_TournamentTeam_TournamentID] FOREIGN KEY (TournamentID) REFERENCES [dbo].[Tournament](ID);

GO