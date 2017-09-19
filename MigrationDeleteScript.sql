delete from [User]
GO
DBCC CHECKIDENT ('[User]', RESEED, 0);
GO

delete from [Partner]
GO
DBCC CHECKIDENT ('[Partner]', RESEED, 0);
GO

delete from [TournamentGame]
GO
DBCC CHECKIDENT ('[TournamentGame]', RESEED, 0);
GO

delete from [PartnerPack]
GO
DBCC CHECKIDENT ('[PartnerPack]', RESEED, 0);
GO

