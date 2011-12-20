CREATE PROCEDURE [dbo].[FullUpdateDoctor]
	@Id int, 
	@FirstName nvarchar(MAX), 
	@LastName nvarchar(MAX), 
	@ThirdName nvarchar(MAX), 
	@Login nvarchar(MAX), 
	@Password nvarchar(MAX),
	@Birthday datetime,
	@Token nvarchar(MAX),
	@SpecialtyId int
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		update Doctors
			set SpecialtyId=@SpecialtyId
			where DoctorId=@Id
		update Users
			set FirstName=@FirstName,
				LastName=@LastName,
				ThirdName=@ThirdName,
				Login=@Login,
				Password=@Password,
				Birthday=@Birthday,
				Token=@Token
			where UserId=@Id
			set @status = 1
	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(3001001)
	end catch
	select @status as Status, @statusMessage as StatusMessage	
RETURN 0