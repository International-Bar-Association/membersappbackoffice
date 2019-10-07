ALTER procedure [dbo].[New_Profile_Search_Conference]
(
	@CountryID int
	,@AreaOfPracticeID int
	,@CommitteeID int
	,@GivenName nvarchar(64)
	,@FamilyName nvarchar(64)
	,@FirmName nvarchar(255)
	,@City nvarchar(255)
	,@ConferenceId int
)
AS
SET NOCOUNT ON
---

CREATE TABLE
	#Temp
	(
		[ID] int
		,[FullName] nvarchar(128)
		,[LawFirm] nvarchar(255)
		,[Country] nvarchar(255)
		,[ConferenceId] int
		,[RowNumber] int
	)
---

;WITH Search
AS (
	SELECT
		[ID]
		,[FullName]
		,[LawFirm]
		,[Country]
		,[ConferenceId]
		,ROW_NUMBER()
		
	OVER (
		ORDER BY
			NEWID()
		) AS [RowNumber]
		
	FROM (
		SELECT DISTINCT
			[R].[id]
			,CONCAT([R].[family_name] + ', ', [R].[given_name]) AS [FullName]
			,[R].[lawfirm] AS [LawFirm]
			,[GC].[description] AS [Country]
			,[CONF_DEL].[conference_id] as [ConferenceId]
		FROM
			[dbo].[_records] AS [R]
		LEFT OUTER JOIN
			[dbo].[gen_countries] AS [GC]
		ON
			[GC].[id] = [R].[country]
		LEFT OUTER JOIN
			[dbo].[gen_addresses] AS [GA]
		ON
			[GA].[record] = [R].[id]
		AND
			[GA].[type] = 4
		LEFT OUTER JOIN
			[dbo].[gen_cities] AS [GCity]
		ON
			[GCity].[id] = [GA].[city]
		LEFT OUTER JOIN
			[dbo].[mem_members_committees] AS [MMC]
		ON
			[MMC].[record] = [R].[id]
		LEFT OUTER JOIN
			[dbo].[mem_members_areasofpractice] AS [MMA]
		ON
			[MMA].[record] = [R].[id]
		LEFT OUTER JOIN
			[dbo].[conf_delegate] as [CONF_DEL]
		ON
			[CONF_DEL].[record_id] = [R].[id]
		WHERE
			([R].[given_name] LIKE [dbo].[GetStringSearchFormat](@GivenName, 0) OR @GivenName IS NULL)
		AND
			([R].[family_name] LIKE [dbo].[GetStringSearchFormat](@FamilyName, 0) OR @FamilyName IS NULL)
		AND
			([R].[lawfirm] LIKE [dbo].[GetStringSearchFormat](@FirmName, 0) OR @FirmName IS NULL)
		AND
			([GCity].[description] LIKE [dbo].[GetStringSearchFormat](@City, 0) OR @City IS NULL)
		AND
			([R].[country] = @CountryID OR @CountryID IS NULL)
		AND
			([MMC].[committee] = @CommitteeID OR @CommitteeID IS NULL)
		AND
			([MMA].[areasofpractice] = @AreaOfPracticeID OR @AreaOfPracticeID IS NULL)
		AND 
			([CONF_DEL].[conference_id] = @ConferenceId OR @ConferenceId IS NULL)
		AND
			[R].[ProfileMakePublic] = 1
		AND
			[R].[status] = 1
		AND
			(
				[R].[class] = 1
			OR
				(
					[R].[class] IN (4, 13) AND [R].[membershiptype] = 8
				)
			)
		) AS X
	)
---

INSERT INTO
	#Temp
SELECT DISTINCT TOP 200
	[ID]
	,[FullName]
	,[LawFirm]
	,[Country]
	,[ConferenceId]
	,[RowNumber]
FROM
	[Search]
ORDER BY
	[RowNumber]
---

SELECT
	[R].[id]
	,[R].[Uid]
	,[Search].[FullName]
	,[Search].[LawFirm]
	,[Search].[Country]
FROM
	#Temp AS [Search]
INNER JOIN
	[dbo].[_records] AS [R]
ON
	[Search].[ID] = [R].[id]
ORDER BY
	[Search].[FullName]
---

DROP TABLE #Temp
---

SET NOCOUNT OFF