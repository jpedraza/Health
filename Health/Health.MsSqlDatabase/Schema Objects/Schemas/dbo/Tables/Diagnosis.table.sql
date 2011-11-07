CREATE TABLE [dbo].[Diagnosis]
(
	DiagnosisId int NOT NULL IDENTITY(1, 1),
	Name nvarchar(MAX),
	Code nvarchar(MAX),
	DiagnosisClassId int NOT NULL,
	DiagnosisSubClassId int NOT NULL
)
