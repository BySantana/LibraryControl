CREATE OR ALTER VIEW [vwGetAvgBooksByMonth] AS
	select
		[UserId],
		MONTH([AddedAt]) AS [Month],
		YEAR([AddedAt]) AS [Year],
		CONVERT(DECIMAL(15,2), AVG([Nota])) AS [AverageNotas]
	from
		[Book]
	WHERE
		[Book].[AddedAt]
			>= DATEADD(MONTH, -11, CAST(GETDATE() AS DATE))
		AND [Book].[AddedAt]
			< DATEADD(MONTH, 1, CAST(GETDATE() AS DATE))
	GROUP BY
		[Book].[UserId],
		MONTH([Book].[AddedAt]),
		YEAR([Book].[AddedAt])