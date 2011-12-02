ALTER TABLE [dbo].[ParameterMetadata]
	ADD CONSTRAINT [ParameterMetadataMTOValueTypes] 
	FOREIGN KEY (ValueTypeId)
	REFERENCES ValueTypes (ValueTypeId)	

