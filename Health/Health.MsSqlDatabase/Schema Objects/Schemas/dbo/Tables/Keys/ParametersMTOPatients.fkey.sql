ALTER TABLE [dbo].[ParametersForPatients]
	ADD CONSTRAINT [ParametersMTOPatientsParameter] 
	FOREIGN KEY (ParameterId)
	REFERENCES Parameters (ParameterId)	

GO

ALTER TABLE [dbo].[ParametersForPatients]
	ADD CONSTRAINT [ParametersMTOPatientsPatient] 
	FOREIGN KEY (PatientId)
	REFERENCES Patients (PatientId)	