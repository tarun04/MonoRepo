SET XACT_ABORT ON

/*
 * Declare values used in CreatedByName, CreatedByUserId, CreatedDate, ModifiedByName, ModifiedByUserId and ModifiedDate columns
 */
DECLARE @CurrentTime DATETIME = GETUTCDATE()
DECLARE @EmptyGuid UNIQUEIDENTIFIER = (SELECT CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER))
DECLARE @SystemUser VARCHAR(50) = 'SYSTEM'

DECLARE @MonoRepoId NVARCHAR(100) = '8e0fa984-38d3-4a6d-aff2-ba4d3a7069ce'

DECLARE @Default NVARCHAR(100) = '5c3ffee9-7e1a-47bf-bf4f-42b1b6e868c6'
DECLARE @Localhost NVARCHAR(100) = '1794298e-5e0a-4213-8700-a46c3a906da6'

DECLARE @JaneB2bId NVARCHAR(100) = 'f5185fc4-8dbf-403b-531a-08d87fa4ec3f'
DECLARE @JohnB2bId NVARCHAR(100) = '84a9e1ed-6850-49d5-531b-08d87fa4ec3f'
DECLARE @JaneB2cId NVARCHAR(100) = '10ea76dd-a2f9-49b8-0e6a-08d87fb19516'
DECLARE @JohnB2cId NVARCHAR(100) = 'f20e3bd6-bfa5-417a-0e6b-08d87fb19516'

/*
 * Admin
 */
DECLARE @admin TABLE
	(
		Id INT,
		IdentityUserId UNIQUEIDENTIFIER,
		FirstName VARCHAR(100),
		LastName VARCHAR(100),
		Email VARCHAR(100),
		PhoneNumber VARCHAR(20),
		PhoneNumberTypeId INT,
		OtherPhoneNumber VARCHAR(20),
		[Address] VARCHAR(100),
		JoiningDate DATETIME
	)

INSERT INTO @admin
	(Id, IdentityUserId, FirstName, LastName, Email, PhoneNumber, PhoneNumberTypeId, OtherPhoneNumber, [Address], JoiningDate)
VALUES 
	(1, @JohnB2bId, 'John', 'Doe', 'john@localhost.com', '530-466-9163', 2, NULL, '2958 Hill Croft Farm Road, Sacramento, CA 95814', @currentTime)

SET IDENTITY_INSERT dbo.[Admin] ON;

MERGE dbo.[Admin] AS t
USING @admin AS s
ON t.Id = s.Id AND t.IdentityUserId = s.IdentityUserId
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Id, IdentityUserId, FirstName, LastName, Email, PhoneNumber, PhoneNumberTypeId, OtherPhoneNumber, [Address], JoiningDate, CreatedByName, CreatedByUserId, CreatedDate, ModifiedByName, ModifiedByUserId, ModifiedDate)
	VALUES (s.Id, s.IdentityUserId, s.FirstName, s.LastName, s.Email, s.PhoneNumber, s.PhoneNumberTypeId, s.OtherPhoneNumber, s.[Address], s.JoiningDate, @systemUser, @EmptyGuid, @currentTime, @systemUser, @EmptyGuid, @currentTime);

SET IDENTITY_INSERT dbo.[Admin] OFF;

/*
 * Course
 */
DECLARE @course TABLE
	(
		Id INT,
		[Name] VARCHAR(100),
		[Description] VARCHAR(100),
		Credits INT,
		IsActive BIT
	)

INSERT INTO @course
	(Id, [Name], [Description], Credits, IsActive)
VALUES 
	(1, 'English', '', 4, 1),
	(2, 'Algebra', '', 4, 1),
	(3, 'Geometry', '', 4, 1),
	(4, 'Statistics', '', 4, 1),
	(5, 'Biology', '', 4, 1),
	(6, 'Chemistry', '', 4, 1),
	(7, 'Physics', '', 4, 1),
	(8, 'Geography', '', 4, 1),
	(9, 'Sociology', '', 4, 1),
	(10, 'Spanish', '', 4, 1)

SET IDENTITY_INSERT dbo.Course ON;

MERGE dbo.Course AS t
USING @course AS s
ON t.Id = s.Id AND t.[Name] = s.[Name]
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Id, [Name], [Description], Credits, IsActive, CreatedByName, CreatedByUserId, CreatedDate, ModifiedByName, ModifiedByUserId, ModifiedDate)
	VALUES (s.Id, s.[Name], s.[Description], s.Credits, s.IsActive, @systemUser, @EmptyGuid, @currentTime, @systemUser, @EmptyGuid, @currentTime);

SET IDENTITY_INSERT dbo.Course OFF;

/*
 * Instructor
 */
DECLARE @instructor TABLE
	(
		Id INT,
		IdentityUserId UNIQUEIDENTIFIER,
		FirstName VARCHAR(100),
		LastName VARCHAR(100),
		Gender VARCHAR(20),
		Email VARCHAR(100),
		PhoneNumber VARCHAR(20),
		PhoneNumberTypeId INT,
		OtherPhoneNumber VARCHAR(20),
		[Address] VARCHAR(100),
		JoiningDate DATETIME
	)

INSERT INTO @instructor
	(Id, IdentityUserId, FirstName, LastName, Gender, Email, PhoneNumber, PhoneNumberTypeId, OtherPhoneNumber, [Address], JoiningDate)
VALUES 
	(1, @JaneB2bId, 'Jane', 'Doe', 'Female', 'jane@default.com', '310-253-6610', 2, NULL, '3347 Libby Street, Culver City, CA 90232', @currentTime),
	(2, @EmptyGuid, 'Mark', 'Alvarez', 'Male', 'mark@default.com', '573-220-9630', 2, NULL, '2040 Jett Lane, Los Angeles, CA 90017', @currentTime),
	(3, @EmptyGuid, 'Ronald', 'Watts', 'Male', 'ronald@default.com', '408-534-9569', 2, NULL, '1248 Ford Street, San Jose, CA 95113', @currentTime)

SET IDENTITY_INSERT dbo.Instructor ON;

MERGE dbo.Instructor AS t
USING @instructor AS s
ON t.Id = s.Id AND t.IdentityUserId = s.IdentityUserId
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Id, IdentityUserId, FirstName, LastName, Gender, Email, PhoneNumber, PhoneNumberTypeId, OtherPhoneNumber, [Address], JoiningDate, CreatedByName, CreatedByUserId, CreatedDate, ModifiedByName, ModifiedByUserId, ModifiedDate)
	VALUES (s.Id, s.IdentityUserId, s.FirstName, s.LastName, s.Gender, s.Email, s.PhoneNumber, s.PhoneNumberTypeId, s.OtherPhoneNumber, s.[Address], s.JoiningDate, @systemUser, @EmptyGuid, @currentTime, @systemUser, @EmptyGuid, @currentTime);

SET IDENTITY_INSERT dbo.Instructor OFF;

/*
 * InstructorCourse
 */
DECLARE @instructorcourse TABLE
	(
		InstructorId INT,
		CourseId INT
	)

INSERT INTO @instructorcourse
	(InstructorId, CourseId)
VALUES 
	(1, 1),
	(2, 2),
	(3, 3),
	(2, 4),
	(3, 5),
	(1, 6),
	(3, 7),
	(2, 8),
	(1, 9),
	(1, 10)

--SET IDENTITY_INSERT dbo.InstructorCourse ON;

MERGE dbo.InstructorCourse AS t
USING @instructorcourse AS s
ON t.InstructorId = s.InstructorId AND t.CourseId = s.CourseId
WHEN NOT MATCHED BY TARGET THEN
    INSERT (InstructorId, CourseId, CreatedByName, CreatedByUserId, CreatedDate, ModifiedByName, ModifiedByUserId, ModifiedDate)
	VALUES (s.InstructorId, s.CourseId, @systemUser, @EmptyGuid, @currentTime, @systemUser, @EmptyGuid, @currentTime);

--SET IDENTITY_INSERT dbo.InstructorCourse OFF;

/*
 * Parent
 */
DECLARE @parent TABLE
	(
		Id INT,
		IdentityUserId UNIQUEIDENTIFIER,
		FirstName VARCHAR(100),
		LastName VARCHAR(100),
		Gender VARCHAR(20),
		Email VARCHAR(100),
		PhoneNumber VARCHAR(20),
		PhoneNumberTypeId INT,
		OtherPhoneNumber VARCHAR(20),
		[Address] VARCHAR(100),
		RelationTypeId INT
	)

INSERT INTO @parent
	(Id, IdentityUserId, FirstName, LastName, Gender, Email, PhoneNumber, PhoneNumberTypeId, OtherPhoneNumber, [Address], RelationTypeId)
VALUES 
	(1, @JaneB2cId, 'Jane', 'Doe', 'Female', 'jane@default.com', '949-303-6206', 2, NULL, '4616 Oakwood Circle, Los Angeles, CA 90017', 2),
	(2, @EmptyGuid, 'Katherine', 'Ruzicka', 'Female', 'katherine@default.com', '310-974-3250', 2, NULL, '1439 Prospect Valley Road, Los Angeles, CA 90017', 2)

SET IDENTITY_INSERT dbo.Parent ON;

MERGE dbo.Parent AS t
USING @parent AS s
ON t.Id = s.Id AND t.IdentityUserId = s.IdentityUserId
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Id, IdentityUserId, FirstName, LastName, Gender, Email, PhoneNumber, PhoneNumberTypeId, OtherPhoneNumber, [Address], RelationTypeId, CreatedByName, CreatedByUserId, CreatedDate, ModifiedByName, ModifiedByUserId, ModifiedDate)
	VALUES (s.Id, s.IdentityUserId, s.FirstName, s.LastName, s.Gender, s.Email, s.PhoneNumber, s.PhoneNumberTypeId, s.OtherPhoneNumber, s.[Address], s.RelationTypeId, @systemUser, @EmptyGuid, @currentTime, @systemUser, @EmptyGuid, @currentTime);

SET IDENTITY_INSERT dbo.Parent OFF;

/*
 * Student
 */
DECLARE @student TABLE
	(
		Id INT,
		IdentityUserId UNIQUEIDENTIFIER,
		FirstName VARCHAR(100),
		LastName VARCHAR(100),
		Gender VARCHAR(20),
		Email VARCHAR(100),
		PhoneNumber VARCHAR(20),
		PhoneNumberTypeId INT,
		OtherPhoneNumber VARCHAR(20),
		[Address] VARCHAR(100),
		BirthDate DATETIME,
		AdmissionDate DATETIME,
		OtherName VARCHAR(100),
		MiddleName VARCHAR(100),
		NameSuffix VARCHAR(100),
		ParentId INT
	)

INSERT INTO @student
	(Id, IdentityUserId, FirstName, LastName, Gender, Email, PhoneNumber, PhoneNumberTypeId, OtherPhoneNumber, [Address], BirthDate, AdmissionDate, OtherName, MiddleName, NameSuffix, ParentId)
VALUES 
	(1, @JohnB2cId, 'John', 'Doe', 'Male', 'john@localhost.com', '215-961-3047', 2, NULL, '4616 Oakwood Circle, Los Angeles, CA 90017', @currentTime, @currentTime, NULL, NULL, NULL, 1),
	(2, @EmptyGuid, 'Oliva', 'Ruzicka', 'Female', 'paul@localhost.com', '909-948-5436', 2, NULL, '1439 Prospect Valley Road, Los Angeles, CA 90017', @currentTime, @currentTime, NULL, NULL, NULL, 2)

SET IDENTITY_INSERT dbo.Student ON;

MERGE dbo.Student AS t
USING @student AS s
ON t.Id = s.Id AND t.IdentityUserId = s.IdentityUserId
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Id, IdentityUserId, FirstName, LastName, Gender, Email, PhoneNumber, PhoneNumberTypeId, OtherPhoneNumber, [Address], BirthDate, AdmissionDate, OtherName, MiddleName, NameSuffix, ParentId, CreatedByName, CreatedByUserId, CreatedDate, ModifiedByName, ModifiedByUserId, ModifiedDate)
	VALUES (s.Id, s.IdentityUserId, s.FirstName, s.LastName, s.Gender, s.Email, s.PhoneNumber, s.PhoneNumberTypeId, s.OtherPhoneNumber, s.[Address], s.BirthDate, s.AdmissionDate, s.OtherName, s.MiddleName, s.NameSuffix, s.ParentId, @systemUser, @EmptyGuid, @currentTime, @systemUser, @EmptyGuid, @currentTime);

SET IDENTITY_INSERT dbo.Student OFF;

/*
 * StudentCourse
 */
DECLARE @studentcourse TABLE
	(
		StudentId INT,
		CourseId INT
	)

INSERT INTO @studentcourse
	(StudentId, CourseId)
VALUES 
	(1, 1),
	(2, 2),
	(1, 3),
	(2, 4),
	(1, 5),
	(1, 6),
	(2, 7),
	(2, 8),
	(2, 9),
	(1, 10)

--SET IDENTITY_INSERT dbo.StudentCourse ON;

MERGE dbo.StudentCourse AS t
USING @studentcourse AS s
ON t.StudentId = s.StudentId AND t.CourseId = s.CourseId
WHEN NOT MATCHED BY TARGET THEN
    INSERT (StudentId, CourseId, CreatedByName, CreatedByUserId, CreatedDate, ModifiedByName, ModifiedByUserId, ModifiedDate)
	VALUES (s.StudentId, s.CourseId, @systemUser, @EmptyGuid, @currentTime, @systemUser, @EmptyGuid, @currentTime);

--SET IDENTITY_INSERT dbo.StudentCourse OFF;
