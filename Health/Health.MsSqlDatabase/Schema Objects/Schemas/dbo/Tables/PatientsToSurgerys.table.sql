CREATE TABLE [dbo].[PatientsToSurgerys]
(
	SurgeryId int NOT NULL,
	PatientId int NOT NULL,
	SurgeryDate Datetime NOT NULL,
	SurgeryStatus bit
)
