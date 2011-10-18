ALTER TABLE [dbo].[PatientsToDoctors]
	ADD CONSTRAINT [PatientsMTMDoctorsToPatients] 
	FOREIGN KEY (PatientId)
	REFERENCES Patients (PatientId)	

GO

ALTER TABLE [dbo].[PatientsToDoctors]
	ADD CONSTRAINT [PatientsMTMDoctorsToDoctors] 
	FOREIGN KEY (DoctorId)
	REFERENCES Doctors (DoctorId)	
