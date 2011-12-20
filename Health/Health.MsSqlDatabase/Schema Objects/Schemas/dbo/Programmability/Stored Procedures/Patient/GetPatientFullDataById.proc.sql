CREATE PROCEDURE [dbo].[GetPatientFullDataById]
	@patientId int = 0
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	if not(exists(select * from Patients where PatientId = @patientId))
	begin
		set @statusMessage = dbo.GSM(3001002)
		select @status as Status, @statusMessage as StatusMessage
		return
	end
	select p.PatientId as Id,
		   p.Card,
		   p.Policy,
		   p.Mother,
		   p.Phone1,
		   p.Phone2,
		   p.StartDateOfObservation,
		   fc.Code as FunctionalClassCode,
		   fc.Description as FunctionalClassDescription,
		   u.FirstName,
		   u.LastName,
		   u.ThirdName,
		   u.Login,
		   u.Password,
		   u.Token,
		   r.Name as Role,
		   @status as Status, @statusMessage as StatusMessage		   	   
		   from Patients as p
		   join FunctionalClasses as fc on p.FunctionalClassesId = fc.FunctionalClassesId
		   join Users as u on p.PatientId = u.UserId
		   join Roles as r on u.RoleId = r.RoleId
		   where p.PatientId = @patientId
RETURN 0