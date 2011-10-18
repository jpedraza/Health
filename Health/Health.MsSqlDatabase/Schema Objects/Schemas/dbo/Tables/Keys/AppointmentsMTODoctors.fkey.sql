ALTER TABLE [dbo].[Appointments]
	ADD CONSTRAINT [AppointmentsMTODoctors] 
	FOREIGN KEY (DoctorId)
	REFERENCES Doctors (DoctorId)	

