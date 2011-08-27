ALTER TABLE [dbo].[Users]
	ADD CONSTRAINT [UsersMTORoles] 
	FOREIGN KEY (RoleId)
	REFERENCES Roles (RoleId)	

