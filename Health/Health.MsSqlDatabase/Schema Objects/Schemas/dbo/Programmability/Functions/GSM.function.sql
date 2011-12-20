CREATE FUNCTION [dbo].[GSM]
(
	@code int
)
RETURNS nvarchar(MAX)
AS
BEGIN
	RETURN (select Message from Status where Code = @code)
END