CREATE PROCEDURE InsertRegion @Id uniqueidentifier, @Code VARCHAR(60), @Name VARCHAR(60), @RegionImageUrl VARCHAR(60)
AS
BEGIN
	INSERT INTO Regions values(@Id, @Code, @Name, @RegionImageUrl)
END;