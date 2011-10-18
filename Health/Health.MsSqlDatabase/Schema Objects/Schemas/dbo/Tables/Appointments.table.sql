CREATE TABLE [dbo].[Appointments]
(
	AppointmentId int NOT NULL IDENTITY(1, 1),
	PatientId int NOT NULL,
	DoctorId int NOT NULL,
	Date Datetime NOT NULL
)
