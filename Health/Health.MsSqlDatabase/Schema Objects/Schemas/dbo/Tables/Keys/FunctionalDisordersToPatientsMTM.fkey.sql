ALTER TABLE [dbo].[FunctionalDisordersToPatients]
	ADD CONSTRAINT [FunctionalDisordersToDiagnosisMTOFunctionalDisorders] 
	FOREIGN KEY (FunctionalDisordersId)
	REFERENCES FunctionalDisorders (FunctionalDisordersId)	

GO

ALTER TABLE [dbo].[FunctionalDisordersToPatients]
	ADD CONSTRAINT [FunctionalDisordersToDiagnosisMTOPatients] 
	FOREIGN KEY (PatientId)
	REFERENCES Patients (PatientId)	