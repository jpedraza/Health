-- Таблица с рассписанием по умолчанию
-- [class DefaultSchedule]
CREATE TABLE [dbo].[DefaultSchedule]
(	
	DefaultScheduleId int NOT NULL IDENTITY(1, 1),
	ParameterId int, -- идентификатор параметра
	DiagnosesId int, -- идентификатор диагноза

	-- [class Period] - период отслеживания параметра
		Years int, 
		Months int,
		Weeks int,
		Days int,
		Hours int,
		Minutes int,
	--//

	-- Промежуток времени, в течении которого возможен ввод параметра
		TimeStart Time, -- начало промежутка
		TimeEnd Time, -- конец промежутка
	--//

	-- Характеристика дня в который возможен ввод параметра
		-- [class Day]
		DayOfWeek int, 
		DayOfMonth int,

		-- [class Month]
		MonthOfYear int,

		-- [class Week]
		WeekOfMonth int,
		WeekParity int,
	--//
)
--//
