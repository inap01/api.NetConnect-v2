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
	[Newsletter] bit,
	[LastChange] timestamp
);

ALTER TABLE [dbo].[User] ADD CONSTRAINT [Registered_Default] DEFAULT GETDATE() FOR [Registered]
ALTER TABLE [dbo].[User] ADD CONSTRAINT [IsTeam_Default] DEFAULT 0 FOR [IsTeam]
ALTER TABLE [dbo].[User] ADD CONSTRAINT [IsAdmin_Default] DEFAULT 0 FOR [IsAdmin]
ALTER TABLE [dbo].[User] ADD CONSTRAINT [IsVorstand_Default] DEFAULT 0 FOR [IsVorstand]
ALTER TABLE [dbo].[User] ADD CONSTRAINT [Newsletter_Default] DEFAULT 1 FOR [Newsletter]
GO