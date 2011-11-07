ALTER TABLE [dbo].[Diagnosis]
	ADD CONSTRAINT [DiagnosisMTODiagnosisClass] 
	FOREIGN KEY (DiagnosisClassId)
	REFERENCES DiagnosisClass (DiagnosisClassId)	

GO

ALTER TABLE [dbo].[Diagnosis]
	ADD CONSTRAINT [DiagnosisMTODiagnosisSubClass] 
	FOREIGN KEY (DiagnosisSubClassId)
	REFERENCES DiagnosisClass (DiagnosisClassId)	