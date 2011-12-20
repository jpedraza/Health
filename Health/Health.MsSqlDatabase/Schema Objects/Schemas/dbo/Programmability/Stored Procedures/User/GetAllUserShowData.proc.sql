CREATE PROCEDURE [dbo].[GetAllUserShowData]
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	select us.UserId as Id, 
		   FirstName, 
		   LastName, 
		   ThirdName, 
		   Login, 
		   Password, 
		   ro.Name as Role, 
		   Birthday,
		   Token,
		   @status as Status, @statusMessage as StatusMessage
		   from Users as us 
		   JOIN Roles as ro ON us.RoleId = ro.RoleId
RETURN 0