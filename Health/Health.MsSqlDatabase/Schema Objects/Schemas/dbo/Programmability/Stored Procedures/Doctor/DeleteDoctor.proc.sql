CREATE PROCEDURE [dbo].[DeleteDoctor]
	@doctorId int = 0
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	if exists(select * from Doctors where DoctorId = @doctorId)
	begin		
		if exists(select * from PatientsToDoctors where DoctorId = @doctorId)
		begin
			set @status = 0
			set @statusMessage = dbo.GSM(2001001)
		end
		else 
		begin		
			delete from Doctors where DoctorId = @doctorId
		end
	end
	else 
	begin
		set @status = 0
		set @statusMessage = dbo.GSM(3001001)
	end	
	select @status as Status, @statusMessage as StatusMessage
RETURN 0