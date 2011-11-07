ALTER TABLE [dbo].[PatientsToDiagnosis]
	ADD CONSTRAINT [PatientsToDiagnosisMTMPatients] 
	FOREIGN KEY (PatientId)
	REFERENCES Patients (PatientId)	

GO

ALTER TABLE [dbo].[PatientsToDiagnosis]
	ADD CONSTRAINT [PatientsToDiagnosisMTMDiagnosis] 
	FOREIGN KEY (DiagnosisId)
	REFERENCES Diagnosis (DiagnosisId)	