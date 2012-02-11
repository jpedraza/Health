﻿-- =============================================
-- Script Template
-- =============================================
/*
Правила формирования кодов статусов:
	Используется семизначный код 0000000, где
	- первая цифра - тип статуса 
		0 - статус
		1 - сообщение
		2 - предупреждение
		3 - ошибка
		4 - критическая ошибка
	- затем 3 цифры - класс ошибки
	- остальные 3 цифры - уникальный код ошибки, для родителя равен 0000
*/

-- Общие
insert into Status values(0000000, 'Все плохо!')
insert into Status values(0000001, 'Все хорошо!')

-- Общие ошибки
insert into Status values(3001000, 'Отсутствует запись в базе для данного идентификатора.')

-- Доктора
	-- Сообщения
	insert into Status values(1001001, 'У доктора нет ведомых пациентов.')
	-- Предупреждения
	insert into Status values(2001001, 'У доктора есть ведомые пациенты.')	
	-- Ошибки
	insert into Status values(3001001, 'Отсутствует информация о докторе в базе.')

-- Пациенты
	-- Ошибки
	insert into Status values(3001002, 'Отсутствует информация о пациенте в базе.')