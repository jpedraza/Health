CREATE PROCEDURE [dbo].[GetMetadataById]
	@Id int
AS
	declare @status int =1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	
	begin try
		SELECT 
			@status as Status,
			@statusMessage as StatusMessage,
			pm.MetadataId as Id,
			pm.ParameterId as ParameterId,
			pm.Value as Value,
			pm.[Key] as [Key],
			pm.ValueTypeId as ValueTypeId,
			p.Name as ParameterName,
			vt.Name as ValueTypeName

		FROM ParameterMetadata as pm,
			Parameters as p,
			ValueTypes as vt
		WHERE pm.MetadataId=@Id and p.ParameterId=pm.ParameterId 
			and vt.ValueTypeId=pm.ValueTypeId
		
	end try
		
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(000000)
		select @status as Status, @statusMessage as StatusMessage
	end catch
RETURN 0