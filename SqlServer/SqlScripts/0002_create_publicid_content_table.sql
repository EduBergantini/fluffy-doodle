BEGIN TRANSACTION;
GO

ALTER TABLE [contents].[TBL_CONTENTS] ADD [CTNT_PUBID] nvarchar(150) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210815213439_CreatePublicIdForContents', N'5.0.8');
GO

COMMIT;
GO

