ALTER TABLE [dbo].[PersonalSchedule]
	ADD CONSTRAINT [PersonalScheduleMTOParameters] 
	FOREIGN KEY (ParameterId)
	REFERENCES Parameters (ParameterId)	

