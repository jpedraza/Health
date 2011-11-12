ALTER TABLE [dbo].[FunctionalDisorders]
	ADD CONSTRAINT [FunctionalDisordersMTO] 
	FOREIGN KEY (Parent)
	REFERENCES FunctionalDisorders (FunctionalDisordersId)	

