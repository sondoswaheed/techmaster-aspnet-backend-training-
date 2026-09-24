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

CREATE TABLE [Instructors] (
    [InstructorId] int NOT NULL IDENTITY,
    [FullName] nvarchar(max) NOT NULL,
    [Email] nvarchar(450) NOT NULL,
    [Specialization] nvarchar(max) NOT NULL,
    [Bio] nvarchar(max) NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Instructors] PRIMARY KEY ([InstructorId])
);
GO

CREATE TABLE [Students] (
    [StudentId] int NOT NULL IDENTITY,
    [FullName] nvarchar(max) NOT NULL,
    [Email] nvarchar(450) NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [IsActive] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedAt] datetime2 NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([StudentId])
);
GO

CREATE TABLE [TrainingTracks] (
    [TrainingTrackId] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Code] nvarchar(450) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Level] nvarchar(max) NOT NULL,
    [Capacity] int NOT NULL,
    [StartDate] date NOT NULL,
    [EndDate] date NOT NULL,
    [Status] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [InstructorId] int NOT NULL,
    CONSTRAINT [PK_TrainingTracks] PRIMARY KEY ([TrainingTrackId]),
    CONSTRAINT [FK_TrainingTracks_Instructors_InstructorId] FOREIGN KEY ([InstructorId]) REFERENCES [Instructors] ([InstructorId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Enrollments] (
    [EnrollmentId] int NOT NULL IDENTITY,
    [EnrollmentDate] datetime2 NOT NULL,
    [Status] int NOT NULL,
    [FinalResult] decimal(18,2) NULL,
    [ProgressPercentage] decimal(18,2) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [StudentId] int NOT NULL,
    [TrainingTrackId] int NOT NULL,
    CONSTRAINT [PK_Enrollments] PRIMARY KEY ([EnrollmentId]),
    CONSTRAINT [FK_Enrollments_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([StudentId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Enrollments_TrainingTracks_TrainingTrackId] FOREIGN KEY ([TrainingTrackId]) REFERENCES [TrainingTracks] ([TrainingTrackId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Payments] (
    [PaymentId] int NOT NULL IDENTITY,
    [Amount] decimal(18,2) NOT NULL,
    [PaymentMethod] int NOT NULL,
    [PaymentDate] date NOT NULL,
    [PaymentStatus] int NOT NULL,
    [ReferenceNumber] nvarchar(max) NOT NULL,
    [Notes] nvarchar(max) NULL,
    [EnrollmentId] int NOT NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY ([PaymentId]),
    CONSTRAINT [FK_Payments_Enrollments_EnrollmentId] FOREIGN KEY ([EnrollmentId]) REFERENCES [Enrollments] ([EnrollmentId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Enrollments_StudentId] ON [Enrollments] ([StudentId]);
GO

CREATE INDEX [IX_Enrollments_TrainingTrackId] ON [Enrollments] ([TrainingTrackId]);
GO

CREATE UNIQUE INDEX [IX_Instructors_Email] ON [Instructors] ([Email]);
GO

CREATE INDEX [IX_Payments_EnrollmentId] ON [Payments] ([EnrollmentId]);
GO

CREATE UNIQUE INDEX [IX_Students_Email] ON [Students] ([Email]);
GO

CREATE UNIQUE INDEX [IX_TrainingTracks_Code] ON [TrainingTracks] ([Code]);
GO

CREATE INDEX [IX_TrainingTracks_InstructorId] ON [TrainingTracks] ([InstructorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260915224159_firstMigration', N'8.0.31');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260921171527_changetablename', N'8.0.31');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Payments] ADD [TotalAmount] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260923144722_Addtotalamountpropertyandpendingstatus', N'8.0.31');
GO

COMMIT;
GO

