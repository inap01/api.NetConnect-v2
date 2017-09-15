CREATE TABLE [dbo].[User] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[FirstName] varchar(255) NOT NULL,
	[LastName] varchar(255) NOT NULL,
	[Nickname] varchar(255) NOT NULL,
	[Email] varchar(255) NOT NULL,
	[Password] varchar(64) NOT NULL,
	[PasswordReset] varchar(64),
	[Registered] datetime NOT NULL,
	[IsTeam] bit NOT NULL,
	[IsAdmin] bit NOT NULL,
	[IsVorstand] bit NOT NULL,
	[Image] varchar(255),
	[SteamID] varchar(25),
	[BattleTag] varchar(25),
	[Newsletter] bit NOT NULL,
	[LastChange] DateTime NOT NULL DEFAULT GETDATE(),
);

ALTER TABLE [dbo].[User] ADD CONSTRAINT [Registered_Default] DEFAULT GETDATE() FOR [Registered]
ALTER TABLE [dbo].[User] ADD CONSTRAINT [IsTeam_Default] DEFAULT 0 FOR [IsTeam]
ALTER TABLE [dbo].[User] ADD CONSTRAINT [IsAdmin_Default] DEFAULT 0 FOR [IsAdmin]
ALTER TABLE [dbo].[User] ADD CONSTRAINT [IsVorstand_Default] DEFAULT 0 FOR [IsVorstand]
ALTER TABLE [dbo].[User] ADD CONSTRAINT [Newsletter_Default] DEFAULT 1 FOR [Newsletter]
GO

CREATE TABLE [dbo].[CateringOrders] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[LanID] int NOT NULL,
	[UserID] int NOT NULL,
	[SeatID] int NOT NULL,
	[Details] text NOT NULL,
	[Price] decimal(10,2) NOT NULL,
	[CompletionState] bit NOT NULL DEFAULT 0,
	[LastChange] DateTime NOT NULL DEFAULT GETDATE(),
);
  
  CREATE TABLE [CateringProducts] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] text NOT NULL,
	[Description] text,
	[Image] varchar(100) NOT NULL,
	[Price] decimal(10,2) NOT NULL,
	[Attributes] text,
	[SingleChoice] bit NOT NULL DEFAULT 0,
	[LastChange] datetime NOT NULL DEFAULT GETDATE(),
  );
  
  CREATE TABLE [Chat] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[UserID] int NOT NULL,
	[Message] text NOT NULL,
	[GameFlag] bit NOT NULL DEFAULT 0,
	[GameTitle] varchar(50) DEFAULT NULL,
	[LastChange] datetime NOT NULL DEFAULT GETDATE(),
);


CREATE TABLE [Logs] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[UserID] int NOT NULL,
	[SQLTable] varchar(50) NOT NULL,
	[SQLActionType] varchar(50) NOT NULL,
	[SQLQuery] text NOT NULL,
	[ModelBefore] text NOT NULL,
	[ModelAfter] text NOT NULL,
	[LastChange] datetime NOT NULL DEFAULT GETDATE(),
);


CREATE TABLE [Partner] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(50) NOT NULL,
	[Link] varchar(100) NOT NULL,
	[Content] text NOT NULL,
	[Image] varchar(50) NOT NULL,
	[ImageAlt] text NOT NULL,
	[State] int NOT NULL,
	[Active] bit NOT NULL DEFAULT 0,
	[Position] int NOT NULL DEFAULT 0,
	[ClickCount] int NOT NULL DEFAULT 0,
	[LastChange] datetime NOT NULL DEFAULT GETDATE(),
);

CREATE TABLE [PartnerPacks] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(50) NOT NULL,
	[ShowPartner] bit NOT NULL,
	[ShowFrontsite] bit NOT NULL,
	[ShowShirt] bit NOT NULL,
	[LastChange] datetime NOT NULL DEFAULT GETDATE(),
);

CREATE TABLE [Seating] (
  [ID] INT IDENTITY(1,1) PRIMARY KEY,
  [UserID] INT NOT NULL DEFAULT 0,
  [State] INT NOT NULL,
  [Description] text NOT NULL,
  [ReservationDate] datetime NOT NULL,
  [Payed] bit NOT NULL,
  [LastChange] datetime NOT NULL DEFAULT GETDATE()
);

CREATE TABLE [Settings] (
  [ID] INT IDENTITY(1,1) PRIMARY KEY,
  [Volume] INT NOT NULL,
  [PrePayment] FLOAT NOT NULL,
  [BoxOffice] FLOAT NOT NULL,
  [Start] datetime NOT NULL,
  [End] datetime NOT NULL,
  [ActiveReservation] INT NOT NULL DEFAULT 0,
  [BankAccountCheck] DATE NOT NULL,
  [ReservedDays] INT NOT NULL,
  [Catering] bit NOT NULL,
  [Feedback] bit NOT NULL,
  [FeedbackLink] VARCHAR(255) NOT NULL,
  [Chat] bit NOT NULL,
  [BankAccountOwner] VARCHAR(50) NOT NULL,
  [IBAN] VARCHAR(50) NOT NULL,
  [BLZ] VARCHAR(50) NOT NULL,
  [BankAccountNumber] VARCHAR(50) NOT NULL,
  [BIC] VARCHAR(50) NOT NULL,
  [LastChange] datetime NOT NULL DEFAULT GETDATE()
);

CREATE TABLE [Tournaments] (
  [ID] INT IDENTITY(1,1) PRIMARY KEY,
  [LanID] INT NOT NULL,
  [GameID] INT NOT NULL,
  [Team] INT NOT NULL DEFAULT 1,
  [Link] text NOT NULL,
  [Mode] VARCHAR(10) NOT NULL,
  [Start] datetime NOT NULL,
  [End] datetime NOT NULL,
  [PauseGame] bit NOT NULL DEFAULT 0,
  [PowerdBy] INT NOT NULL DEFAULT 0,
  [LastChange] datetime NOT NULL DEFAULT GETDATE()
);

CREATE TABLE [TournamentGames] (
  [ID] INT IDENTITY(1,1) PRIMARY KEY,
  [Name] VARCHAR(50) NOT NULL,
  [Icon] VARCHAR(50) NOT NULL,
  [Rules] text NOT NULL,
  [BattleTag] bit NOT NULL DEFAULT 0,
  [SteamID] bit NOT NULL DEFAULT 0,
  [LastChange] datetime NOT NULL DEFAULT GETDATE()
);

CREATE TABLE [TournamentParticipants] (
  [ID] INT IDENTITY(1,1) PRIMARY KEY,
  [UserID] INT NOT NULL,
  [TournamentID] int NOT NULL,
  [TeamID] INT NOT NULL DEFAULT 0,
  [Registered] datetime NOT NULL,
  [LastChange] datetime NOT NULL DEFAULT GETDATE()
);

CREATE TABLE TournamentTeams (
  [ID] INT IDENTITY(1,1) PRIMARY KEY,
  [Name] VARCHAR(250) NOT NULL,
  [TournamentID] INT NOT NULL,
  [Password] VARCHAR(500) NOT NULL,
  [LastChange] datetime NOT NULL DEFAULT GETDATE()
);