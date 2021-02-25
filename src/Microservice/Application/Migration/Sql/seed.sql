SET XACT_ABORT ON

/*
 * Declare values used in CreatedByName, CreatedByUserId, CreatedDate, ModifiedByName, ModifiedByUserId and ModifiedDate columns
 */
DECLARE @CurrentTime DATETIME = GETUTCDATE()
DECLARE @EmptyGuid UNIQUEIDENTIFIER = (SELECT CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER))
DECLARE @SystemUser VARCHAR(50) = 'SYSTEM'

/*
* DegreeType
*/
DECLARE @degreetype TABLE
	(
		Id INT,
		[Name] VARCHAR(100),
		[Description] VARCHAR(100),
		Credits INT,
		IsActive BIT
	)

INSERT INTO @degreetype
	(Id, [Name], [Description], Credits, IsActive)
VALUES 
	(1, 'High School Diploma', '', 16, 1),
	(2, 'Associates', '', 64, 1),
	(3, 'Bachelors', '', 120, 1),
	(4, 'Masters', '', 150, 1),
	(5, 'PhD', '', 48, 1)

SET IDENTITY_INSERT dbo.DegreeType ON;

MERGE dbo.DegreeType AS t
USING @degreetype AS s
ON t.Id = s.Id
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Id, [Name], [Description], Credits, IsActive, CreatedByName, CreatedByUserId, CreatedDate, ModifiedByName, ModifiedByUserId, ModifiedDate)
	VALUES (s.Id, s.[Name], s.[Description], s.Credits, s.IsActive, @systemUser, @EmptyGuid, @currentTime, @systemUser, @EmptyGuid, @currentTime);

SET IDENTITY_INSERT dbo.DegreeType OFF;
