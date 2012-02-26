CREATE PROCEDURE [dbo].[GetAllPatientsForDoctor]
	@doctorId int = 0
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	if not(exists(select * from Doctors where DoctorId = @doctorId))
	begin
		set @statusMessage = dbo.GSM(3001001)
		select @status as Status, @statusMessage as StatusMessage
		return
	end
	if not(exists(select * from PatientsToDoctors where DoctorId = @doctorId))
	begin
		set @StatusMessage = dbo.GSM(1001001)
		select @status as Status, @statusMessage as StatusMessage
		return
	end
	select u.UserId as Id,
		   u.FirstName,
		   u.LastName,
		   u.ThirdName,
		   p.Card,
		   p.Policy,
		   p.Mother as Mother,
		   @status as Status, @statusMessage as StatusMessage
		   from Users as u
		   join Patients as p on u.UserId = p.PatientId
		   join PatientsToDoctors as ptd on ptd.PatientId = p.PatientId
		   where ptd.DoctorId = @doctorId
RETURN 0