using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;
using Health.Site.Models;

namespace Health.Site.Areas.Schedules.Models
{
    public class DefaultScheduleForm : CoreViewModel
    {
        public DefaultSchedule DefaultSchedule { get; set; }

        public IEnumerable<Parameter> Parameters { get; set; }

        public IEnumerable<SelectListItem> ParametersSelectList
        {
            get
            {
                var selected_list = new BindingList<SelectListItem>();
                foreach (Parameter parameter in Parameters)
                {
                    selected_list.Add(new SelectListItem
                                          {
                                              Text = parameter.Name,
                                              Value = parameter.Id.ToString()
                                          });
                }
                return selected_list;
            }
        }

        public string Message { get; set; }

        public IEnumerable<SelectListItem> AllDaysInWeek
        {
            get
            {
                IEnumerable<Day> days = DaysInWeek.GetAll();
                var select_list_items = new BindingList<SelectListItem>();
                int in_week = DefaultSchedule == null || DefaultSchedule.Day == null ? 0 : DefaultSchedule.Day.InWeek;
                foreach (Day day in days)
                {
                    select_list_items.Add(new SelectListItem
                    {
                        Selected = day.InWeek == in_week,
                        Text = day.Name,
                        Value = day.InWeek.ToString()
                    });
                }
                return select_list_items;
            }
        }

        public IEnumerable<SelectListItem> AllDaysInMonth
        {
            get
            {
                var all_days = new BindingList<SelectListItem>
                                   {
                                       new SelectListItem
                                           {
                                               Text = "Любой",
                                               Value = "0"
                                           }
                                   };
                int in_month = DefaultSchedule == null || DefaultSchedule.Day == null ? 0 : DefaultSchedule.Day.InMonth;
                for (int i = 1; i <= 31; i++)
                {
                    var item = new SelectListItem
                    {
                        Selected = in_month == i,
                        Text = i.ToString(),
                        Value = i.ToString()
                    };
                    all_days.Add(item);
                }
                return all_days;
            }
        }

        public IEnumerable<SelectListItem> AllMonthsInYear
        {
            get
            {
                IEnumerable<Month> months = MonthsInYear.GetAll();
                var select_list_items = new BindingList<SelectListItem>();
                int in_year = DefaultSchedule == null || DefaultSchedule.Month == null ? 0 : DefaultSchedule.Month.InYear;
                foreach (Month month in months)
                {
                    select_list_items.Add(new SelectListItem
                    {
                        Selected = month.InYear == in_year,
                        Text = month.Name,
                        Value = month.InYear.ToString()
                    });
                }
                return select_list_items;
            }
        }

        public IEnumerable<SelectListItem> WeekParities
        {
            get
            {
                ParityOfWeek parity_of_week = DefaultSchedule == null || DefaultSchedule.Week == null
                                                  ? ParityOfWeek.All
                                                  : DefaultSchedule.Week.Parity;
                var week_parity = new BindingList<SelectListItem>
                                          {
                                              new SelectListItem
                                                  {
                                                      Selected = parity_of_week == ParityOfWeek.All,
                                                      Text = "Любая",
                                                      Value = ParityOfWeek.All.ToString()
                                                  },
                                              new SelectListItem
                                                  {
                                                      Selected = parity_of_week == ParityOfWeek.Odd,
                                                      Text = "Нечетная",
                                                      Value = ParityOfWeek.Odd.ToString()
                                                  },
                                              new SelectListItem
                                                  {
                                                      Selected = parity_of_week == ParityOfWeek.Even,
                                                      Text = "Четная",
                                                      Value = ParityOfWeek.Even.ToString()
                                                  }
                                          };
                return week_parity;
            }
        }
    }

    public class DefaultScheduleList : CoreViewModel
    {
        public IEnumerable<DefaultSchedule> DefaultSchedules { get; set; }
    }
}