IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [Prenom] nvarchar(max) NULL,
        [Nom] nvarchar(max) NULL,
        [Role] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [Competence] (
        [Id] int NOT NULL IDENTITY,
        [Intitule] nvarchar(max) NULL,
        CONSTRAINT [PK_Competence] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [Domaine] (
        [Id] int NOT NULL IDENTITY,
        [Intitule] nvarchar(max) NULL,
        CONSTRAINT [PK_Domaine] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [Niveau] (
        [Id] int NOT NULL IDENTITY,
        [Intitule] nvarchar(max) NULL,
        CONSTRAINT [PK_Niveau] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [Patient] (
        [Id] int NOT NULL IDENTITY,
        [Numero] int NOT NULL,
        CONSTRAINT [PK_Patient] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [Prerequis] (
        [Id] int NOT NULL IDENTITY,
        [Intitule] nvarchar(max) NULL,
        CONSTRAINT [PK_Prerequis] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [DomaineCompetence] (
        [Id] int NOT NULL IDENTITY,
        [DomaineId] int NULL,
        [CompetenceId] int NULL,
        CONSTRAINT [PK_DomaineCompetence] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DomaineCompetence_Competence_CompetenceId] FOREIGN KEY ([CompetenceId]) REFERENCES [Competence] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_DomaineCompetence_Domaine_DomaineId] FOREIGN KEY ([DomaineId]) REFERENCES [Domaine] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [DomaineUser] (
        [Id] int NOT NULL IDENTITY,
        [DomaineId] int NULL,
        [ApplicationUserId] nvarchar(450) NULL,
        CONSTRAINT [PK_DomaineUser] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DomaineUser_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_DomaineUser_Domaine_DomaineId] FOREIGN KEY ([DomaineId]) REFERENCES [Domaine] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [PatientUser] (
        [Id] int NOT NULL IDENTITY,
        [PatientId] int NULL,
        [ApplicationUserId] nvarchar(450) NULL,
        CONSTRAINT [PK_PatientUser] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PatientUser_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PatientUser_Patient_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patient] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [Suivi] (
        [Id] int NOT NULL IDENTITY,
        [PatientId] int NULL,
        [DomaineId] int NULL,
        [Etat] int NOT NULL,
        [DateValide] datetime2 NOT NULL,
        CONSTRAINT [PK_Suivi] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Suivi_Domaine_DomaineId] FOREIGN KEY ([DomaineId]) REFERENCES [Domaine] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Suivi_Patient_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patient] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [CompetencePrerequis] (
        [Id] int NOT NULL IDENTITY,
        [CompetenceId] int NULL,
        [PrerequisId] int NULL,
        CONSTRAINT [PK_CompetencePrerequis] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CompetencePrerequis_Competence_CompetenceId] FOREIGN KEY ([CompetenceId]) REFERENCES [Competence] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CompetencePrerequis_Prerequis_PrerequisId] FOREIGN KEY ([PrerequisId]) REFERENCES [Prerequis] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [PrerequisNiveau] (
        [Id] int NOT NULL IDENTITY,
        [NiveauId] int NULL,
        [PrerequisId] int NULL,
        CONSTRAINT [PK_PrerequisNiveau] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PrerequisNiveau_Niveau_NiveauId] FOREIGN KEY ([NiveauId]) REFERENCES [Niveau] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PrerequisNiveau_Prerequis_PrerequisId] FOREIGN KEY ([PrerequisId]) REFERENCES [Prerequis] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [SuiviApplicationUser] (
        [Id] int NOT NULL IDENTITY,
        [SuiviId] int NULL,
        [ApplicationUserId] nvarchar(450) NULL,
        CONSTRAINT [PK_SuiviApplicationUser] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SuiviApplicationUser_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SuiviApplicationUser_Suivi_SuiviId] FOREIGN KEY ([SuiviId]) REFERENCES [Suivi] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [SuiviCompetence] (
        [Id] int NOT NULL IDENTITY,
        [SuiviId] int NULL,
        [CompetenceId] int NULL,
        [Etat] int NOT NULL,
        [DateValide] datetime2 NOT NULL,
        CONSTRAINT [PK_SuiviCompetence] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SuiviCompetence_Competence_CompetenceId] FOREIGN KEY ([CompetenceId]) REFERENCES [Competence] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SuiviCompetence_Suivi_SuiviId] FOREIGN KEY ([SuiviId]) REFERENCES [Suivi] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [SuiviPrerequis] (
        [Id] int NOT NULL IDENTITY,
        [SuiviCompetenceId] int NULL,
        [PrerequisId] int NULL,
        [Etat] int NOT NULL,
        [DateValide] datetime2 NOT NULL,
        CONSTRAINT [PK_SuiviPrerequis] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SuiviPrerequis_Prerequis_PrerequisId] FOREIGN KEY ([PrerequisId]) REFERENCES [Prerequis] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SuiviPrerequis_SuiviCompetence_SuiviCompetenceId] FOREIGN KEY ([SuiviCompetenceId]) REFERENCES [SuiviCompetence] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [SuiviNiveau] (
        [Id] int NOT NULL IDENTITY,
        [SuiviPrerequisId] int NULL,
        [NiveauId] int NULL,
        [Etat] int NOT NULL,
        [DateValide] datetime2 NOT NULL,
        CONSTRAINT [PK_SuiviNiveau] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SuiviNiveau_Niveau_NiveauId] FOREIGN KEY ([NiveauId]) REFERENCES [Niveau] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SuiviNiveau_SuiviPrerequis_SuiviPrerequisId] FOREIGN KEY ([SuiviPrerequisId]) REFERENCES [SuiviPrerequis] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [Note] (
        [Id] int NOT NULL IDENTITY,
        [ApplicationUserId] nvarchar(450) NULL,
        [SuiviNiveauId] int NULL,
        [Date] datetime2 NOT NULL,
        [Texte] nvarchar(max) NULL,
        CONSTRAINT [PK_Note] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Note_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Note_SuiviNiveau_SuiviNiveauId] FOREIGN KEY ([SuiviNiveauId]) REFERENCES [SuiviNiveau] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE TABLE [SuiviExercice] (
        [Id] int NOT NULL IDENTITY,
        [Valide] bit NOT NULL,
        [DateFait] datetime2 NOT NULL,
        [DateValide] datetime2 NOT NULL,
        [ValideurId] nvarchar(450) NULL,
        [SuiviNiveauId] int NULL,
        CONSTRAINT [PK_SuiviExercice] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SuiviExercice_SuiviNiveau_SuiviNiveauId] FOREIGN KEY ([SuiviNiveauId]) REFERENCES [SuiviNiveau] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SuiviExercice_AspNetUsers_ValideurId] FOREIGN KEY ([ValideurId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_CompetencePrerequis_CompetenceId] ON [CompetencePrerequis] ([CompetenceId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_CompetencePrerequis_PrerequisId] ON [CompetencePrerequis] ([PrerequisId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_DomaineCompetence_CompetenceId] ON [DomaineCompetence] ([CompetenceId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_DomaineCompetence_DomaineId] ON [DomaineCompetence] ([DomaineId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_DomaineUser_ApplicationUserId] ON [DomaineUser] ([ApplicationUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_DomaineUser_DomaineId] ON [DomaineUser] ([DomaineId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_Note_ApplicationUserId] ON [Note] ([ApplicationUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_Note_SuiviNiveauId] ON [Note] ([SuiviNiveauId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_PatientUser_ApplicationUserId] ON [PatientUser] ([ApplicationUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_PatientUser_PatientId] ON [PatientUser] ([PatientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_PrerequisNiveau_NiveauId] ON [PrerequisNiveau] ([NiveauId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_PrerequisNiveau_PrerequisId] ON [PrerequisNiveau] ([PrerequisId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_Suivi_DomaineId] ON [Suivi] ([DomaineId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_Suivi_PatientId] ON [Suivi] ([PatientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_SuiviApplicationUser_ApplicationUserId] ON [SuiviApplicationUser] ([ApplicationUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_SuiviApplicationUser_SuiviId] ON [SuiviApplicationUser] ([SuiviId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_SuiviCompetence_CompetenceId] ON [SuiviCompetence] ([CompetenceId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_SuiviCompetence_SuiviId] ON [SuiviCompetence] ([SuiviId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_SuiviExercice_SuiviNiveauId] ON [SuiviExercice] ([SuiviNiveauId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_SuiviExercice_ValideurId] ON [SuiviExercice] ([ValideurId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_SuiviNiveau_NiveauId] ON [SuiviNiveau] ([NiveauId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_SuiviNiveau_SuiviPrerequisId] ON [SuiviNiveau] ([SuiviPrerequisId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_SuiviPrerequis_PrerequisId] ON [SuiviPrerequis] ([PrerequisId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    CREATE INDEX [IX_SuiviPrerequis_SuiviCompetenceId] ON [SuiviPrerequis] ([SuiviCompetenceId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200416103655_InitialPerso')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200416103655_InitialPerso', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200417161322_suppressionSuiviuser')
BEGIN
    DROP TABLE [SuiviApplicationUser];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200417161322_suppressionSuiviuser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200417161322_suppressionSuiviuser', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    ALTER TABLE [CompetencePrerequis] DROP CONSTRAINT [FK_CompetencePrerequis_Competence_CompetenceId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    ALTER TABLE [CompetencePrerequis] DROP CONSTRAINT [FK_CompetencePrerequis_Prerequis_PrerequisId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    ALTER TABLE [DomaineCompetence] DROP CONSTRAINT [FK_DomaineCompetence_Competence_CompetenceId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    ALTER TABLE [DomaineCompetence] DROP CONSTRAINT [FK_DomaineCompetence_Domaine_DomaineId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    ALTER TABLE [DomaineUser] DROP CONSTRAINT [FK_DomaineUser_Domaine_DomaineId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    ALTER TABLE [PatientUser] DROP CONSTRAINT [FK_PatientUser_AspNetUsers_ApplicationUserId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    ALTER TABLE [PrerequisNiveau] DROP CONSTRAINT [FK_PrerequisNiveau_Niveau_NiveauId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    ALTER TABLE [PrerequisNiveau] DROP CONSTRAINT [FK_PrerequisNiveau_Prerequis_PrerequisId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    DROP INDEX [IX_PrerequisNiveau_PrerequisId] ON [PrerequisNiveau];
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PrerequisNiveau]') AND [c].[name] = N'PrerequisId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [PrerequisNiveau] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [PrerequisNiveau] ALTER COLUMN [PrerequisId] int NOT NULL;
    CREATE INDEX [IX_PrerequisNiveau_PrerequisId] ON [PrerequisNiveau] ([PrerequisId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    DROP INDEX [IX_PrerequisNiveau_NiveauId] ON [PrerequisNiveau];
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PrerequisNiveau]') AND [c].[name] = N'NiveauId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [PrerequisNiveau] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [PrerequisNiveau] ALTER COLUMN [NiveauId] int NOT NULL;
    CREATE INDEX [IX_PrerequisNiveau_NiveauId] ON [PrerequisNiveau] ([NiveauId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Prerequis]') AND [c].[name] = N'Intitule');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Prerequis] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Prerequis] ALTER COLUMN [Intitule] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    DROP INDEX [IX_PatientUser_ApplicationUserId] ON [PatientUser];
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PatientUser]') AND [c].[name] = N'ApplicationUserId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [PatientUser] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [PatientUser] ALTER COLUMN [ApplicationUserId] nvarchar(450) NOT NULL;
    CREATE INDEX [IX_PatientUser_ApplicationUserId] ON [PatientUser] ([ApplicationUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Note]') AND [c].[name] = N'Texte');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Note] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Note] ALTER COLUMN [Texte] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Niveau]') AND [c].[name] = N'Intitule');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Niveau] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Niveau] ALTER COLUMN [Intitule] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    DROP INDEX [IX_DomaineUser_DomaineId] ON [DomaineUser];
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DomaineUser]') AND [c].[name] = N'DomaineId');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [DomaineUser] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [DomaineUser] ALTER COLUMN [DomaineId] int NOT NULL;
    CREATE INDEX [IX_DomaineUser_DomaineId] ON [DomaineUser] ([DomaineId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    DROP INDEX [IX_DomaineCompetence_DomaineId] ON [DomaineCompetence];
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DomaineCompetence]') AND [c].[name] = N'DomaineId');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [DomaineCompetence] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [DomaineCompetence] ALTER COLUMN [DomaineId] int NOT NULL;
    CREATE INDEX [IX_DomaineCompetence_DomaineId] ON [DomaineCompetence] ([DomaineId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    DROP INDEX [IX_DomaineCompetence_CompetenceId] ON [DomaineCompetence];
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DomaineCompetence]') AND [c].[name] = N'CompetenceId');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [DomaineCompetence] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [DomaineCompetence] ALTER COLUMN [CompetenceId] int NOT NULL;
    CREATE INDEX [IX_DomaineCompetence_CompetenceId] ON [DomaineCompetence] ([CompetenceId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Domaine]') AND [c].[name] = N'Intitule');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Domaine] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Domaine] ALTER COLUMN [Intitule] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    DROP INDEX [IX_CompetencePrerequis_PrerequisId] ON [CompetencePrerequis];
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CompetencePrerequis]') AND [c].[name] = N'PrerequisId');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [CompetencePrerequis] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [CompetencePrerequis] ALTER COLUMN [PrerequisId] int NOT NULL;
    CREATE INDEX [IX_CompetencePrerequis_PrerequisId] ON [CompetencePrerequis] ([PrerequisId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    DROP INDEX [IX_CompetencePrerequis_CompetenceId] ON [CompetencePrerequis];
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CompetencePrerequis]') AND [c].[name] = N'CompetenceId');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [CompetencePrerequis] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [CompetencePrerequis] ALTER COLUMN [CompetenceId] int NOT NULL;
    CREATE INDEX [IX_CompetencePrerequis_CompetenceId] ON [CompetencePrerequis] ([CompetenceId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Competence]') AND [c].[name] = N'Intitule');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Competence] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [Competence] ALTER COLUMN [Intitule] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Prenom');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Prenom] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Nom');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Nom] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    ALTER TABLE [CompetencePrerequis] ADD CONSTRAINT [FK_CompetencePrerequis_Competence_CompetenceId] FOREIGN KEY ([CompetenceId]) REFERENCES [Competence] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    ALTER TABLE [CompetencePrerequis] ADD CONSTRAINT [FK_CompetencePrerequis_Prerequis_PrerequisId] FOREIGN KEY ([PrerequisId]) REFERENCES [Prerequis] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    ALTER TABLE [DomaineCompetence] ADD CONSTRAINT [FK_DomaineCompetence_Competence_CompetenceId] FOREIGN KEY ([CompetenceId]) REFERENCES [Competence] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    ALTER TABLE [DomaineCompetence] ADD CONSTRAINT [FK_DomaineCompetence_Domaine_DomaineId] FOREIGN KEY ([DomaineId]) REFERENCES [Domaine] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    ALTER TABLE [DomaineUser] ADD CONSTRAINT [FK_DomaineUser_Domaine_DomaineId] FOREIGN KEY ([DomaineId]) REFERENCES [Domaine] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    ALTER TABLE [PatientUser] ADD CONSTRAINT [FK_PatientUser_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    ALTER TABLE [PrerequisNiveau] ADD CONSTRAINT [FK_PrerequisNiveau_Niveau_NiveauId] FOREIGN KEY ([NiveauId]) REFERENCES [Niveau] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    ALTER TABLE [PrerequisNiveau] ADD CONSTRAINT [FK_PrerequisNiveau_Prerequis_PrerequisId] FOREIGN KEY ([PrerequisId]) REFERENCES [Prerequis] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200511110840_essaiResoPatient')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200511110840_essaiResoPatient', N'3.1.3');
END;

GO

