CREATE TABLE [dbo].[Patients]
(
	PatientId int NOT NULL,
	Policy nvarchar(MAX) NOT NULL,
	Card nvarchar(MAX) NOT NULL,
	Mother nvarchar(MAX) NOT NULL,
	StartDateOfObservation datetime NOT NULL,
	Phone1 int,
	Phone2 int,
	FunctionalClassesId int NULL
)
