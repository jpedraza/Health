ALTER TABLE [dbo].[DiagnosisClass]
	ADD CONSTRAINT [DiagnosisClassMTO] 
	FOREIGN KEY (Parent)
	REFERENCES DiagnosisClass (DiagnosisClassId)	

