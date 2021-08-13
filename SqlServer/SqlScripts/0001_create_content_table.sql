IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'contents') IS NULL EXEC(N'CREATE SCHEMA [contents];');
GO

CREATE TABLE [contents].[TBL_CONTENTS] (
    [CTNT_ID] int NOT NULL IDENTITY,
    [CTNT_TITLE] nvarchar(120) NOT NULL,
    [CTNT_SUBTITLE] nvarchar(270) NOT NULL,
    [CTNT_FEATIMG] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_CONTENTS] PRIMARY KEY ([CTNT_ID])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210813203037_CreateContentsMigration', N'5.0.8');
GO

COMMIT;
GO

