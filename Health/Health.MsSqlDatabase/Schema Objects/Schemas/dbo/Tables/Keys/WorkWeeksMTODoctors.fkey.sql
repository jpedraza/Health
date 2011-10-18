ALTER TABLE [dbo].[WorkWeeks]
	ADD CONSTRAINT [WorkWeeksMTODoctors] 
	FOREIGN KEY (DoctorId)
	REFERENCES Doctors (DoctorId)	

