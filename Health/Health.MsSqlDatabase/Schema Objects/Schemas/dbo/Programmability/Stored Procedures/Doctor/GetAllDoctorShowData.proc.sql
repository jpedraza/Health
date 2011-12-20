CREATE PROCEDURE [dbo].[GetAllDoctorShowData]
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
		   sp.Name as Specialty,
		   @status as Status, @statusMessage as StatusMessage
		   from Users as us 
		   JOIN Doctors as do ON us.UserId = do.DoctorId
		   JOIN Roles as ro ON us.RoleId = ro.RoleId
		   JOIN Specialties as sp ON do.SpecialtyId = sp.SpecialtyId			   
RETURN 0