using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;
using Health.Site.Models;

namespace Health.Site.Areas.Schedule.Models
{
    public class DefaultScheduleFormModel : CoreViewModel, IValidatableObject
    {
        public DefaultSchedule DefaultSchedule { get; set; }

        #region Implementation of IValidatableObject

        /// <summary>
        /// Определяет, является ли заданный объект допустимым.
        /// </summary>
        /// <returns>
        /// Коллекция, в которой хранятся сведения о проверках, завершившихся неудачей.
        /// </returns>
        /// <param name="validation_context">Контекст проверки.</param>
        public IEnumerable<ValidationResult> Validate(ValidationContext validation_context)
        {
            var result = new BindingList<ValidationResult>();
            if (DefaultSchedule != null)
            {
                if (DefaultSchedule.Parameter != null && DefaultSchedule.Parameter.ParameterId != 0)
                {
                    if (DefaultSchedule.Period == null)
                    {
                        throw new Exception("Не переданы данные для периода.");
                    }
                    if (DefaultSchedule.Period.Minutes < 0 ||
                        DefaultSchedule.Period.Hours < 0 ||
                        DefaultSchedule.Period.Days < 0 || 
                        DefaultSchedule.Period.Weeks < 0 ||
                        DefaultSchedule.Period.Months < 0 || 
                        DefaultSchedule.Period.Years < 0)
                    {
                        result.Add(new ValidationResult("Ни одно из значений не может быть меньше нуля.", new[]
                                                                                                              {
                                                                                                                  "DefaultSchedule.Period"
                                                                                                              }));
                    }
                    if (DefaultSchedule.Day.InWeek > 7 || DefaultSchedule.Day.InWeek < 0)
                    {
                        result.Add(new ValidationResult("Такого дня недели не существует.", new[]
                                                                                                {
                                                                                                    "DefaultSchedule.Day.InWeek"
                                                                                                }));
                    }
                    if (DefaultSchedule.Day.InMonth > 31 || DefaultSchedule.Day.InMonth < 0)
                    {
                        result.Add(new ValidationResult("Ни в одном из месяцев нет такого дня недели.", new[]
                                                                                                            {
                                                                                                                "DefaultSchedule.Day.InMonth"
                                                                                                            }));
                    }
                    if (DefaultSchedule.Week.InMonth < 0)
                    {
                        result.Add(new ValidationResult("Номер недели в месяце не может быть отрицательным.", new[]
                                                                                                                  {
                                                                                                                      "DefaultSchedule.Week.InMonth"
                                                                                                                  }));
                    }
                    if (DefaultSchedule.Week.Parity != ParityOfWeek.All &&
                        DefaultSchedule.Week.Parity != ParityOfWeek.Even &&
                        DefaultSchedule.Week.Parity != ParityOfWeek.Odd)
                    {
                        result.Add(new ValidationResult("Неделя может быть только Четной/Нечетной/Любой.", new[]
                                                                                                               {
                                                                                                                   "DefaultSchedule.Week.Parity"
                                                                                                               }));
                    }
                    if (DefaultSchedule.Month.InYear > 12 || DefaultSchedule.Month.InYear < 0)
                    {
                        result.Add(new ValidationResult("Такого месяца не существует.", new[]
                                                                                            {
                                                                                                "DefaultSchedule.Month.InYear"
                                                                                            }));
                    }
                }
                else
                {
                    throw new Exception("Не переданы данные параметра.");
                }
            }
            return result;
        }

        #endregion
    }
}