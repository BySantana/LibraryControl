CREATE OR ALTER VIEW [vwGetBooksByCategory] AS
	select 
		Book.UserId,
		Category.Title AS Category,
		YEAR(Book.AddedAt) AS [Year],
		COUNT(Book.Id) AS Books
	from Book
	join Category 
		on Book.CategoryId = Category.Id
	where
		Book.AddedAt >= DATEADD(MONTH, -12, CAST(GETDATE() AS DATE))
	GROUP BY
		Book.UserId,
		Category.Title,
		YEAR(Book.AddedAt)