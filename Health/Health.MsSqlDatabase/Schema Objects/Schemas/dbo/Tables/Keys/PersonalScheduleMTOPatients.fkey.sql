ALTER TABLE [dbo].[PersonalSchedule]
	ADD CONSTRAINT [PersonalScheduleMTOPatients] 
	FOREIGN KEY (PatientId)
	REFERENCES Patients (PatientId)	

