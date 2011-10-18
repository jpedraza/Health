ALTER TABLE [dbo].[Patients]
	ADD CONSTRAINT [PatientsOTOUsers] 
	FOREIGN KEY (PatientId)
	REFERENCES Users (UserId)	

