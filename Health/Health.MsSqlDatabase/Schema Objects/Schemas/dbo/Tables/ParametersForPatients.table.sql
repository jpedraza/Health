CREATE TABLE [dbo].[ParametersForPatients]
(
	ParameterId int NOT NULL, 
	PatientId int NOT NULL,
	Value varbinary(MAX) NOT NULL,
	[Date] DateTime NOT NULL
)
