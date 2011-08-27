CREATE PROCEDURE [dbo].[GetParametersForPatientNowDate]
	@patient_id int,
	@date datetime = 0
AS
BEGIN
	set language Russian -- говорим что первый день недели это понедельник	
	SET NOCOUNT ON;
	if (@date = 0)
		set @date = getdate()
	declare @now_date datetime = @date -- текущая дата
	declare @now_time time(7) = convert(time(7), @now_date, 108) -- текущее время
	declare @month_id int = datepart(mm, @now_date) -- текущий месяц
	declare @week_day int = datepart(dw, @now_date)-- день недели
	declare @month_day int = datepart(dd, @now_date) -- текущий день
	declare @is_even_week bit = datepart(ww, @now_date) % 2 -- четная / нечетная неделя
	declare @week_number int = 
					datepart(ww, @now_date) - 
					datepart(ww, dateadd(dd, -@month_day + 1, @now_date)) + 1 -- номер недели в месяце
			
	/*
	select * from PersonalSchedule ps where
		(@now_date between ps.DateStart and ps.DateEnd) and
		(@now_time between ps.TimeStart and ps.TimeEnd) and
		(
			(ps.WeekDay = @week_day or ps.WeekDay is null) and
			(ps.MonthId = @month_id or ps.MonthId is null) and
			(ps.WeekId = @is_even_week or ps.WeekId is null) and
			(ps.WeekNumber = @week_number or ps.WeekNumber is null) and
			(ps.MonthDay = @month_day or ps.MonthDay is null)
		)
	*/
	select @is_even_week, @week_day
END