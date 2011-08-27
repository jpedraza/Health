-- Персональные расписания
-- [class PersonalSchedule]
CREATE TABLE [dbo].[PersonalSchedule]
(
	PatientId int, -- идентификатор пользователя
	ParameterId int, -- идентификатор параметра
	DiagnosesId int, -- идентификатор диагноза

	-- Период в течении которого отслеживается параметр
		DateStart DateTime, -- начало периода
		DateEnd DateTime, -- конец периода
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
