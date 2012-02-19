CREATE PROCEDURE [dbo].[DeleteParameter]
	@Id int
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		if EXISTS( select * 
					FROM Parameters as p
					WHERE p.ParameterId=@Id)
			begin
				DELETE FROM ParameterMetadata
				WHERE ParameterId=@Id
								
				DELETE FROM Parameters WHERE ParameterId=@Id
			end
		else
		begin
			set @status = 0
			set @statusMessage = dbo.GSM(4001001)
		end

	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(0000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage
RETURN 0