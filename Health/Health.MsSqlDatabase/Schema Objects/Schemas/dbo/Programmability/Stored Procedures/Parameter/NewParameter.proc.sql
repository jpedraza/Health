CREATE PROCEDURE [dbo].[NewParameter]
	@nameParameter nvarchar(max), 
	@defaultValue varbinary(1)
AS
	insert into Parameters(Name, DefaultValue) values(@nameParameter, @defaultValue)
	
	
RETURN 0