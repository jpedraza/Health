using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;
using Health.Site.Models;
using Health.Site.Models.Metadata;
using Health.Site.Models.Providers;

namespace Health.Site.Areas.Schedules.Models
{
    public class DefaultScheduleForm : CoreViewModel
    {
        [ClassMetadata(typeof (DefaultScheduleFormMetadata))]
        public DefaultSchedule DefaultSchedule { get; set; }

        public IEnumerable<Parameter> Parameters { get; set; }

        public IEnumerable<SelectListItem> ParametersSelectList
        {
            get
            {
                var selectedList = new BindingList<SelectListItem>();
                if (Parameters == null) return selectedList;
                foreach (Parameter parameter in Parameters)
                {
                    selectedList.Add(new SelectListItem
                                          {
                                              Selected =
                                                  !(DefaultSchedule == null || DefaultSchedule.Parameter == null) &&
                                                  parameter.Id == DefaultSchedule.Parameter.Id,
                                              Text = parameter.Name,
                                              Value = parameter.Id.ToString()
                                          });
                }
                return selectedList;
            }
        }

        public string Message { get; set; }

        public IEnumerable<SelectListItem> AllDaysInWeek
        {
            get
            {
                IEnumerable<Day> days = DaysInWeek.GetAll();
                var selectListItems = new BindingList<SelectListItem>();
                int inWeek = DefaultSchedule == null || DefaultSchedule.Day == null ? 0 : DefaultSchedule.Day.InWeek;
                foreach (Day day in days)
                {
                    selectListItems.Add(new SelectListItem
                                              {
                                                  Selected = day.InWeek == inWeek,
                                                  Text = day.Name,
                                                  Value = day.InWeek.ToString()
                                              });
                }
                return selectListItems;
            }
        }

        public IEnumerable<SelectListItem> AllDaysInMonth
        {
            get
            {
                var allDays = new BindingList<SelectListItem>
                                   {
                                       new SelectListItem
                                           {
                                               Text = "Любой",
                                               Value = "0"
                                           }
                                   };
                int inMonth = DefaultSchedule == null || DefaultSchedule.Day == null ? 0 : DefaultSchedule.Day.InMonth;
                for (int i = 1; i <= 31; i++)
                {
                    var item = new SelectListItem
                                   {
                                       Selected = inMonth == i,
                                       Text = i.ToString(),
                                       Value = i.ToString()
                                   };
                    allDays.Add(item);
                }
                return allDays;
            }
        }

        public IEnumerable<SelectListItem> AllMonthsInYear
        {
            get
            {
                IEnumerable<Month> months = MonthsInYear.GetAll();
                var selectListItems = new BindingList<SelectListItem>();
                int inYear = DefaultSchedule == null || DefaultSchedule.Month == null
                                  ? 0
                                  : DefaultSchedule.Month.InYear;
                foreach (Month month in months)
                {
                    selectListItems.Add(new SelectListItem
                                              {
                                                  Selected = month.InYear == inYear,
                                                  Text = month.Name,
                                                  Value = month.InYear.ToString()
                                              });
                }
                return selectListItems;
            }
        }

        public IEnumerable<SelectListItem> WeekParities
        {
            get
            {
                ParityOfWeek parityOfWeek = DefaultSchedule == null || DefaultSchedule.Week == null
                                                  ? ParityOfWeek.All
                                                  : DefaultSchedule.Week.Parity;
                var weekParity = new BindingList<SelectListItem>
                                      {
                                          new SelectListItem
                                              {
                                                  Selected = parityOfWeek == ParityOfWeek.All,
                                                  Text = "Любая",
                                                  Value = ParityOfWeek.All.ToString()
                                              },
                                          new SelectListItem
                                              {
                                                  Selected = parityOfWeek == ParityOfWeek.Odd,
                                                  Text = "Нечетная",
                                                  Value = ParityOfWeek.Odd.ToString()
                                              },
                                          new SelectListItem
                                              {
                                                  Selected = parityOfWeek == ParityOfWeek.Even,
                                                  Text = "Четная",
                                                  Value = ParityOfWeek.Even.ToString()
                                              }
                                      };
                return weekParity;
            }
        }
    }

    public class DefaultScheduleList : CoreViewModel
    {
        public IEnumerable<DefaultSchedule> DefaultSchedules { get; set; }
    }
}