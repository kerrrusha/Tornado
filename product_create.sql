IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Product' and xtype='U')
BEGIN
    CREATE TABLE Product (
        Id INT PRIMARY KEY IDENTITY (1, 1),
        [Name] NVARCHAR(250),
		category_id INT FOREIGN KEY REFERENCES Category(Id),
		cost money,
		old_cost money,
		image_url text,
		remains int,
		[description] text,
		creating_date date default current_timestamp
    )
END
