CREATE PROCEDURE [dbo].[ParameterForPatientByPatientId]
	@patientId int
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	if not(exists(select * from Patients where PatientId = @patientId))
	begin
		set @statusMessage = dbo.GSM(3001002)
		select @status as Status, @statusMessage as StatusMessage
		return
	end
	select p.PatientId,
		   @status as Status, @statusMessage as StatusMessage		   	   
		   from Patients as p
		   JOIN ParametersForPatients as pfp ON pfp.PatientId=@patientId
		   where p.PatientId = @patientId
RETURN 0