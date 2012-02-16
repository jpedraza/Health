CREATE PROCEDURE [dbo].[GetValueTypeDataById]
	@ValueTypeId int
AS
	declare @status int =1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	
	begin try
		SELECT 
			@status as Status,
			@statusMessage as StatusMessage,
			vt.ValueTypeId as ValueTypeId,
			vt.ValueTypeId as Id,
			vt.Name as Name	
				
		from ValueTypes as vt
		where vt.ValueTypeId = @ValueTypeId
	end try
		
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(000000)
		select @status as Status, @statusMessage as StatusMessage
	end catch
		
RETURN 0