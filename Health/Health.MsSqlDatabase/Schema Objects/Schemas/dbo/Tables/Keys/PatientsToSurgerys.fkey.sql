ALTER TABLE [dbo].[PatientsToSurgerys]
	ADD CONSTRAINT [PatientsToSurgerysMTMPatients]
	FOREIGN KEY (PatientId)
	REFERENCES Patients (PatientId)
GO

ALTER TABLE [dbo].[PatientsToSurgerys]
	ADD CONSTRAINT [PatientsToSurgerysMTMSurgerys]
	FOREIGN KEY (SurgeryId)
	REFERENCES Surgerys (SurgeryId)
