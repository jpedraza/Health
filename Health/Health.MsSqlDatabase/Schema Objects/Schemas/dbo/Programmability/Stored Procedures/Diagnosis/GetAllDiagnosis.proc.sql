CREATE PROCEDURE [dbo].[GetAllDiagnosis]
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	SELECT d.DiagnosisId as Id,
		   d.Name,
		   d.Code,
		   dc.Name as ClassName,
		   @status as Status, @statusMessage as StatusMessage
		   from Diagnosis as d
		   join DiagnosisClass as dc on d.DiagnosisClassId = dc.DiagnosisClassId
RETURN 0