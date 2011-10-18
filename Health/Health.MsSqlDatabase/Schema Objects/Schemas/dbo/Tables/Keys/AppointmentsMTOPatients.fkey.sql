ALTER TABLE [dbo].[Appointments]
	ADD CONSTRAINT [AppointmentsMTOPatients] 
	FOREIGN KEY (PatientId)
	REFERENCES Patients (PatientId)	

	

