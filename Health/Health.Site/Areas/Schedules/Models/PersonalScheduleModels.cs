using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;
using Health.Core.TypeProvider;
using Health.Site.Models;
using Health.Site.Models.Metadata;

namespace Health.Site.Areas.Schedules.Models
{
    public class PersonalScheduleList : CoreViewModel
    {
        public IEnumerable<PersonalSchedule> PersonalSchedules { get; set; }
    }

    public class PersonalScheduleForm : CoreViewModel
    {
        [ClassMetadata(typeof (PersonalScheduleFormMetadata))]
        public PersonalSchedule PersonalSchedule { get; set; }

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
                                                  !(PersonalSchedule == null || PersonalSchedule.Parameter == null) &&
                                                  parameter.Id == PersonalSchedule.Parameter.Id,
                                              Text = parameter.Name,
                                              Value = parameter.Id.ToString()
                                          });
                }
                return selectedList;
            }
        }

        public IEnumerable<Patient> Patients { get; set; }

        public IEnumerable<SelectListItem> PatientsSelectList
        {
            get
            {
                var selectedList = new BindingList<SelectListItem>();
                foreach (Patient patient in Patients)
                {
                    selectedList.Add(new SelectListItem
                                          {
                                              Selected =
                                                  PersonalSchedule.Patient != null && (!(PersonalSchedule != null || PersonalSchedule.Patient != null) &&
                                                                                       patient.Id == PersonalSchedule.Patient.Id),
                                              Text = patient.FirstName + patient.LastName + patient.ThirdName,
                                              Value = patient.Id.ToString()
                                          });
                }
                return selectedList;
            }
        }

        public IEnumerable<SelectListItem> AllDaysInWeek
        {
            get
            {
                IEnumerable<Day> days = DaysInWeek.GetAll();
                var selectListItems = new BindingList<SelectListItem>();
                int inWeek = PersonalSchedule == null || PersonalSchedule.Day == null ? 0 : PersonalSchedule.Day.InWeek;
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
                int inMonth = PersonalSchedule == null || PersonalSchedule.Day == null
                                   ? 0
                                   : PersonalSchedule.Day.InMonth;
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
                int inYear = PersonalSchedule == null || PersonalSchedule.Month == null
                                  ? 0
                                  : PersonalSchedule.Month.InYear;
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
                ParityOfWeek parityOfWeek = PersonalSchedule == null || PersonalSchedule.Week == null
                                                  ? ParityOfWeek.All
                                                  : PersonalSchedule.Week.Parity;
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

        public string Message { get; set; }
    }
}