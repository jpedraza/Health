CREATE TABLE [dbo].[DiagnosisClass]
(
	DiagnosisClassId int NOT NULL IDENTITY(1, 1), 
	Name nvarchar(MAX) NOT NULL,
	Code nvarchar(MAX) NOT NULL
)
