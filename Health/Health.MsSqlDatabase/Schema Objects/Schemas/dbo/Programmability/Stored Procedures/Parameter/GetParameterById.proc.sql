CREATE PROCEDURE [dbo].[GetParameterById]
	@parameterId int
AS
	declare @status int =1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	
	begin try		
		SELECT ParameterId, Name, DefaultValue,
			ParameterId as Id,
			@status as Status,
			@statusMessage as StatusMessage
		from 
		Parameters where ParameterId = @parameterId
	end try
		
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(000000)
	end catch
RETURN 0