USE master
GO

ALTER DATABASE [NetConnect] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

ALTER DATABASE [NetConnect] SET OFFLINE;
GO

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

CREATE TABLE [dbo].[User] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[FirstName] varchar(255) NOT NULL,
	[LastName] varchar(255) NOT NULL,
	[Nickname] varchar(255) NOT NULL,
	[Email] varchar(255) NOT NULL UNIQUE,
	[Password] varchar(64) NOT NULL,
	[PasswordReset] varchar(64),
	[Registered] datetime NOT NULL DEFAULT GETDATE(),
	[IsTeam] bit NOT NULL DEFAULT 0,
	[IsAdmin] bit NOT NULL DEFAULT 0,
	[CEO] int CHECK (CEO in (1, 2, 3)),
	[IsActive] bit NOT NULL DEFAULT 1,
	[Image] varchar(255),
	[SteamID] varchar(25),
	[BattleTag] varchar(25),
	[Newsletter] bit NOT NULL DEFAULT 1,
	[RowVersion] timestamp NOT NULL,
);

CREATE TABLE [dbo].[Event] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Start] datetime NOT NULL,
	[End] datetime NOT NULL,
	[ReservationCost] FLOAT NOT NULL,
	[IsActiveReservation] INT NOT NULL DEFAULT 0,
	[IsActiveCatering] bit NOT NULL,
	[IsActiveFeedback] bit NOT NULL,
	[FeedbackLink] VARCHAR(255) NOT NULL,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[CateringOrder] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[EventID] int NOT NULL,
	[UserID] int NOT NULL,
	[SeatID] int NOT NULL,
	[CompletionState] int NOT NULL DEFAULT 0 CHECK (CompletionState in (0, 1, 2)),
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[CateringOrderDetail] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[CateringOrderID] int NOT NULL,
	[CateringProductID] int NOT NULL,
	[Attributes] text,
	[RowVersion] timestamp NOT NULL
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
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[PartnerDetails] (
	[PartnerID] int NOT NULL,
	[DisplayStage] bit NOT NULL DEFAULT 0,
	[DisplaySidebar] bit NOT NULL DEFAULT 0,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[PartnerPack] (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(50) NOT NULL,
	[IsActive] bit NOT NULL DEFAULT 1,
	[RowVersion] timestamp NOT NULL
);

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
	[TeamSize] INT NOT NULL DEFAULT 1,
	[ChallongeLink] text NOT NULL,
	[Mode] VARCHAR(10) NOT NULL,
	[Start] datetime NOT NULL,
	[End] datetime,
	[IsPauseGame] bit NOT NULL DEFAULT 0,
	[PartnerID] INT DEFAULT 0,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[TournamentGame] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL,
	[Icon] VARCHAR(50) NOT NULL,
	[Rules] text NOT NULL,
	[IsActive] bit NOT NULL DEFAULT 1,
	[BattleTag] bit NOT NULL DEFAULT 0,
	[SteamID] bit NOT NULL DEFAULT 0,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[TournamentParticipant] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[UserID] INT NOT NULL,
	[TournamentID] int NOT NULL,
	[TournamentTeamID] INT DEFAULT NULL,
	[Registered] datetime NOT NULL,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[TournamentTeam] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(250) NOT NULL,
	[TournamentID] INT NOT NULL,
	[Password] VARCHAR(100) NOT NULL,
	[RowVersion] timestamp NOT NULL
);

CREATE TABLE [dbo].[ChangeSet] (
	[ID] INT IDENTITY(1,1) PRIMARY KEY,
	[Event] DateTime,
	[CateringOrder] DateTime,
	[CateringProduct] DateTime,
	[CateringOrderDetail] DateTime,
	[Logs] DateTime,
	[Partner] DateTime,
	[PartnerDetails] DateTime,
	[PartnerPack] DateTime,
	[Seat] DateTime,
	[Tournament] DateTime,
	[TournamentGame] DateTime,
	[TournamentTeam] DateTime,	
	[TournamentParticipant] DateTime,
	[User] DateTime,
	[RowVersion] rowversion
);
GO

INSERT INTO ChangeSet(CateringProduct)VALUES(NULL)

-- [dbo].[CateringOrder]
ALTER TABLE [dbo].[CateringOrder] ADD CONSTRAINT [FK_CateringOrder_EventID] FOREIGN KEY (EventID) REFERENCES [dbo].[Event](ID);
ALTER TABLE [dbo].[CateringOrder] ADD CONSTRAINT [FK_CateringOrder_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User](ID);
ALTER TABLE [dbo].[CateringOrder] ADD CONSTRAINT [FK_CateringOrder_SeatID] FOREIGN KEY (SeatID) REFERENCES [dbo].[Seat](ID);

-- [dbo].[CateringOrderDetail]
ALTER TABLE [dbo].[CateringOrderDetail] ADD CONSTRAINT [FK_CateringOrderDetail_CateringOrderID] FOREIGN KEY (CateringOrderID) REFERENCES [dbo].[CateringOrder](ID);
ALTER TABLE [dbo].[CateringOrderDetail] ADD CONSTRAINT [FK_CateringOrderDetail_CateringProductID] FOREIGN KEY (CateringProductID) REFERENCES [dbo].[CateringProduct](ID);

-- [dbo].[Logs]
ALTER TABLE [dbo].[Logs] ADD CONSTRAINT [FK_Logs_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User](ID);

-- [dbo].[Partner]
ALTER TABLE [dbo].[Partner] ADD CONSTRAINT [FK_Partner_PartnerPackID] FOREIGN KEY (PartnerPackID) REFERENCES [dbo].[PartnerPack](ID);

-- [dbo].[PartnerDetails]
ALTER TABLE [dbo].[PartnerDetails] ADD CONSTRAINT [FK_PartnerDetails_PartnerID] FOREIGN KEY (PartnerID) REFERENCES [dbo].[Partner](ID);

-- [dbo].[Seat]
ALTER TABLE [dbo].[Seat] ADD CONSTRAINT [FK_Seat_EventID] FOREIGN KEY (EventID) REFERENCES [dbo].[Event](ID);
ALTER TABLE [dbo].[Seat] ADD CONSTRAINT [FK_Seat_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User](ID);

-- [dbo].[Tournament]
ALTER TABLE [dbo].[Tournament] ADD CONSTRAINT [FK_Tournament_EventID] FOREIGN KEY (EventID) REFERENCES [dbo].[Event](ID);
ALTER TABLE [dbo].[Tournament] ADD CONSTRAINT [FK_Tournament_TournamentGameID] FOREIGN KEY (TournamentGameID) REFERENCES [dbo].[TournamentGame](ID);
ALTER TABLE [dbo].[Tournament] ADD CONSTRAINT [FK_Tournament_PartnerID] FOREIGN KEY (PartnerID) REFERENCES [dbo].[Partner](ID);

-- [dbo].[TournamentParticipant]
ALTER TABLE [dbo].[TournamentParticipant] ADD CONSTRAINT [FK_Tournament_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User](ID);
ALTER TABLE [dbo].[TournamentParticipant] ADD CONSTRAINT [FK_Tournament_TournamentID] FOREIGN KEY (TournamentID) REFERENCES [dbo].[Tournament](ID);
ALTER TABLE [dbo].[TournamentParticipant] ADD CONSTRAINT [FK_Tournament_TournamentTeamID] FOREIGN KEY (TournamentTeamID) REFERENCES [dbo].[TournamentTeam](ID);

-- [dbo].[TournamentTeam]
ALTER TABLE [dbo].[TournamentTeam] ADD CONSTRAINT [FK_TournamentTeam_TournamentID] FOREIGN KEY (TournamentID) REFERENCES [dbo].[Tournament](ID);

GO

-- Trigger

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

IF (OBJECT_ID(N'[dbo].[UpdateCaterinOrdergDetail]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateCaterinOrdergDetail];
END;
go

create trigger [dbo].[UpdateCaterinOrdergDetail]
on [dbo].[CateringOrderDetail]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET CateringOrderDetail = @date
go

IF (OBJECT_ID(N'[dbo].[UpdateChat]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateChat];
END;
go

create trigger [dbo].[UpdateChat]
on [dbo].[Chat]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET Chat = @date
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

IF (OBJECT_ID(N'[dbo].[UpdateSettings]') IS NOT NULL)
BEGIN
      DROP TRIGGER [dbo].[UpdateSettings];
END;
go

create trigger [dbo].[UpdateSettings]
on [dbo].[Settings]
after update, insert, delete
as	
	declare @date DateTime
	Select @date = GETDATE()
	UPDATE [dbo].[ChangeSet]
	SET Settings = @date
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