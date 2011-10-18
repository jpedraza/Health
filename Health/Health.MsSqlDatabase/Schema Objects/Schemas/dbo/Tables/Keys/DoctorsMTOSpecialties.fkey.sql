ALTER TABLE [dbo].[Doctors]
	ADD CONSTRAINT [DoctorsMTOSpecialties] 
	FOREIGN KEY (SpecialtyId)
	REFERENCES Specialties (SpecialtyId)	

