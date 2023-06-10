CREATE DATABASE db;
GO
USE db;
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

IF OBJECT_ID(N'[Countries]') IS NULL
BEGIN
    BEGIN TRANSACTION;

    CREATE TABLE [Countries] (
        [Id] int NOT NULL IDENTITY,
        [CountryName] nvarchar(max) NOT NULL,
        [CountryCode] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Countries] PRIMARY KEY ([Id])
    );

    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [Username] nvarchar(32) NOT NULL,
        [Email] nvarchar(50) NOT NULL,
        [Password] nvarchar(100) NOT NULL,
        [Role] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );

    CREATE TABLE [Years] (
        [Id] int NOT NULL IDENTITY,
        [Year] int NOT NULL,
        CONSTRAINT [PK_Years] PRIMARY KEY ([Id])
    );

    CREATE TABLE [ECommerceData] (
        [Id] int NOT NULL IDENTITY,
        [CountryId] int NOT NULL,
        [YearId] int NOT NULL,
        [IndividualCriteria] nvarchar(max) NOT NULL,
        [UnitOfMeasure] nvarchar(max) NOT NULL,
        [Value] float NOT NULL,
        CONSTRAINT [PK_ECommerceData] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ECommerceData_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Countries] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ECommerceData_Years_YearId] FOREIGN KEY ([YearId]) REFERENCES [Years] ([Id]) ON DELETE CASCADE
    );

    CREATE TABLE [InternetUseData] (
        [Id] int NOT NULL IDENTITY,
        [CountryId] int NOT NULL,
        [YearId] int NOT NULL,
        [IndividualCriteria] nvarchar(max) NOT NULL,
        [UnitOfMeasure] nvarchar(max) NOT NULL,
        [Value] float NOT NULL,
        CONSTRAINT [PK_InternetUseData] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_InternetUseData_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Countries] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_InternetUseData_Years_YearId] FOREIGN KEY ([YearId]) REFERENCES [Years] ([Id]) ON DELETE CASCADE
    );

    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CountryCode', N'CountryName') AND [object_id] = OBJECT_ID(N'[Countries]'))
        SET IDENTITY_INSERT [Countries] ON;
    INSERT INTO [Countries] ([Id], [CountryCode], [CountryName])
    VALUES (1, N'BE', N'Belgium'),
    (2, N'BG', N'Bulgaria'),
    (3, N'CZ', N'Czechia'),
    (4, N'DK', N'Denmark'),
    (5, N'DE', N'Germany'),
    (6, N'EE', N'Estonia'),
    (7, N'IE', N'Ireland'),
    (8, N'EL', N'Greece'),
    (9, N'ES', N'Spain'),
    (10, N'FR', N'France'),
    (11, N'HR', N'Croatia'),
    (12, N'IT', N'Italy'),
    (13, N'CY', N'Cyprus'),
    (14, N'LV', N'Latvia'),
    (15, N'LT', N'Lithuania'),
    (16, N'LU', N'Luxembourg'),
    (17, N'HU', N'Hungary'),
    (18, N'MT', N'Malta'),
    (19, N'NL', N'Netherlands'),
    (20, N'AT', N'Austria'),
    (21, N'PL', N'Poland'),
    (22, N'PT', N'Portugal'),
    (23, N'RO', N'Romania'),
    (24, N'SI', N'Slovenia'),
    (25, N'SK', N'Slovakia'),
    (26, N'FI', N'Finland'),
    (27, N'SE', N'Sweden'),
    (28, N'IS', N'Iceland'),
    (29, N'LI', N'Liechtenstein'),
    (30, N'NO', N'Norway'),
    (31, N'CH', N'Switzerland'),
    (32, N'UK', N'United Kingdom'),
    (33, N'BA', N'Bosnia and Herzegovina'),
    (34, N'ME', N'Montenegro'),
    (35, N'MK', N'North Macedonia'),
    (36, N'AL', N'Albania'),
    (37, N'RS', N'Serbia'),
    (38, N'TR', N'Turkey'),
    (39, N'XK', N'Kosovo'),
    (40, N'EU27_2020', N'European Union');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CountryCode', N'CountryName') AND [object_id] = OBJECT_ID(N'[Countries]'))
        SET IDENTITY_INSERT [Countries] OFF;

    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Password', N'Role', N'Username') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] ON;
    INSERT INTO [Users] ([Id], [Email], [Password], [Role], [Username])
    VALUES (1, N'', N'AQAAAAIAAYagAAAAEI7YdLvq87Mab5oGkEyr5BDAxD8acnA7yB1nQhAjikJrL6vTxVNc4AZjCd/dsoEmdQ==', N'admin', N'admin');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Password', N'Role', N'Username') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] OFF;

    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Year') AND [object_id] = OBJECT_ID(N'[Years]'))
        SET IDENTITY_INSERT [Years] ON;
    INSERT INTO [Years] ([Id], [Year])
    VALUES (1, 2002),
    (2, 2003),
    (3, 2004),
    (4, 2005),
    (5, 2006),
    (6, 2007),
    (7, 2008),
    (8, 2009),
    (9, 2010),
    (10, 2011),
    (11, 2012),
    (12, 2013),
    (13, 2014),
    (14, 2015),
    (15, 2016),
    (16, 2017),
    (17, 2018),
    (18, 2019),
    (19, 2020),
    (20, 2021),
    (21, 2022);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Year') AND [object_id] = OBJECT_ID(N'[Years]'))
        SET IDENTITY_INSERT [Years] OFF;

    CREATE INDEX [IX_ECommerceData_CountryId] ON [ECommerceData] ([CountryId]);

    CREATE INDEX [IX_ECommerceData_YearId] ON [ECommerceData] ([YearId]);

    CREATE INDEX [IX_InternetUseData_CountryId] ON [InternetUseData] ([CountryId]);

    CREATE INDEX [IX_InternetUseData_YearId] ON [InternetUseData] ([YearId]);

    CREATE UNIQUE INDEX [IX_Users_Username] ON [Users] ([Username]);

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230609233251_InitialCreate', N'7.0.5');

    COMMIT;
END;
GO