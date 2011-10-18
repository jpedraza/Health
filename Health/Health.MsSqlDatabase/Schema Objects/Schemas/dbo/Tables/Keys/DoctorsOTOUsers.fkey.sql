ALTER TABLE [dbo].[Doctors]
	ADD CONSTRAINT [DoctorsOTOUsers] 
	FOREIGN KEY (DoctorId)
	REFERENCES Users (UserId)	

