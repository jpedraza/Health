ALTER TABLE [dbo].[DefaultSchedule]
	ADD CONSTRAINT [DefaultScheduleMTOParameters] 
	FOREIGN KEY (ParameterId)
	REFERENCES Parameters (ParameterId)	

