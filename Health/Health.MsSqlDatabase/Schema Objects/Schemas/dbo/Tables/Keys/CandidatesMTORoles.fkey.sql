ALTER TABLE [dbo].[Candidates]
	ADD CONSTRAINT [CandidatesMTORoles] 
	FOREIGN KEY (RoleId)
	REFERENCES Roles (RoleId)	

