ALTER TABLE [dbo].[Patients]
	ADD CONSTRAINT [PatientsMTOFunctionalClasses] 
	FOREIGN KEY (FunctionalClassesId)
	REFERENCES FunctionalClasses (FunctionalClassesId)	

