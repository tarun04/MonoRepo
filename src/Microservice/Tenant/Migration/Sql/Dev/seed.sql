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

/*
* Products
*/
DECLARE @products TABLE
	(
		Id UNIQUEIDENTIFIER,
		[Name] VARCHAR(250)
	)

INSERT INTO @products
	(Id, [Name])
VALUES 
	(@MonoRepoId, 'MonoRepo')

MERGE dbo.Products AS t
USING @products AS s
ON t.Id = s.Id
WHEN MATCHED AND (t.[Name] <> s.[Name])
    THEN UPDATE SET t.[Name] = s.[Name], t.Id = s.Id
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Id, [Name], CreatedByName, CreatedByUserId, CreatedDate, ModifiedByName, ModifiedByUserId, ModifiedDate)
	VALUES (s.Id, s.[Name], @systemUser, @EmptyGuid, @currentTime, @systemUser, @EmptyGuid, @currentTime);

/*
* Tenants
*/
DECLARE @tenants TABLE
	(
		Id UNIQUEIDENTIFIER,
		[Name] VARCHAR(100),
		DisplayName VARCHAR(100)
	)

INSERT INTO @tenants
	(Id, [Name], DisplayName)
VALUES
	(@Default, 'default', 'MonoRepo'),
	(@Localhost, 'localhost', 'MonoRepo')

MERGE dbo.Tenants AS t
USING @tenants AS s
ON t.Id = s.Id
WHEN MATCHED AND (t.[Name] <> s.[Name] OR t.DisplayName <> s.DisplayName)
	THEN UPDATE SET t.[Name] = s.[Name], t.Id = s.Id, t.DisplayName = s.DisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (Id, [Name], DisplayName, CreatedByName, CreatedByUserId, CreatedDate, ModifiedByName, ModifiedByUserId, ModifiedDate)
	VALUES (s.Id, s.[Name], s.DisplayName, @systemUser, @EmptyGuid, @currentTime, @systemUser, @EmptyGuid, @currentTime);


/*
* TenantProducts
*/
DECLARE @tenantProducts TABLE
	(
		TenantId UNIQUEIDENTIFIER,
		ProductId UNIQUEIDENTIFIER,
		PurchasedDate DATE,
		GoLiveDate DATE,
		IsActive BIT,
		TenantProductUrl NVARCHAR(100)
	)

INSERT INTO @tenantProducts
	(TenantId, ProductId, PurchasedDate, GoLiveDate, IsActive, TenantProductUrl)
VALUES
	(@Localhost, @MonoRepoId, GETUTCDATE(), GETUTCDATE(), 1, 'http://localhost:4200/monorepo')
		
MERGE dbo.TenantProducts AS t
USING @tenantProducts AS s
ON t.TenantId = s.TenantId AND t.ProductId = s.ProductId
WHEN MATCHED AND (t.ProductId <> s.ProductId)
	THEN UPDATE SET t.ProductId = s.ProductId, t.PurchasedDate = s.PurchasedDate, t.GoLiveDate = s.GoLiveDate, t.IsActive = s.IsActive, t.TenantProductUrl = s.TenantProductUrl
WHEN NOT MATCHED BY TARGET THEN
	INSERT (TenantId, ProductId, PurchasedDate, GoLiveDate, IsActive, TenantProductUrl, CreatedByName, CreatedByUserId, CreatedDate, ModifiedByName, ModifiedByUserId, ModifiedDate)
	VALUES (s.TenantId, s.ProductId, s.PurchasedDate, s.GoLiveDate, s.IsActive, s.TenantProductUrl, @systemUser, @EmptyGuid, @currentTime, @systemUser, @EmptyGuid, @currentTime);