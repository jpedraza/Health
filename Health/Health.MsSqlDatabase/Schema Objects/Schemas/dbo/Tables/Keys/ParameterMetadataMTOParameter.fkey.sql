ALTER TABLE [dbo].[ParameterMetadata]
	ADD CONSTRAINT [ParameterMetadataMTOParameter] 
	FOREIGN KEY (ParameterId)
	REFERENCES Parameters (ParameterId)	

