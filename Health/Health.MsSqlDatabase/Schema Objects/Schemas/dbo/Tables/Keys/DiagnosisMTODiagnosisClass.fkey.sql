ALTER TABLE [dbo].[Diagnosis]
	ADD CONSTRAINT [DiagnosisMTODiagnosisClass] 
	FOREIGN KEY (DiagnosisClassId)
	REFERENCES DiagnosisClass (DiagnosisClassId)