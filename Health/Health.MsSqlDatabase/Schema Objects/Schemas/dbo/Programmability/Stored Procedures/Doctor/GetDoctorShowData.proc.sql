CREATE PROCEDURE [dbo].[GetDoctorShowData]
	@doctorId int = 0
AS
	declare @status int
	declare @statusMessage nvarchar(MAX)
	if exists(select * from Doctors where DoctorId = @doctorId)
	begin
		set @status = 1	
		set @statusMessage = dbo.GSM(0000001)		
	end
	else 
	begin
		set @status = 0
		set @statusMessage = dbo.GSM(3001001)
	end	
	select  us.UserId as Id, 
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
			WHERE do.DoctorId = @doctorId		
RETURN 0