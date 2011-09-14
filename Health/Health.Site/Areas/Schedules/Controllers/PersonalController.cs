using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities.POCO;
using Health.Site.Areas.Schedules.Models;
using Health.Site.Controllers;

namespace Health.Site.Areas.Schedules.Controllers
{
    public class PersonalController : CoreController
    {
        public PersonalController(IDIKernel di_kernel) : base(di_kernel) { }

        public ActionResult Index()
        {
            var form_model = new PersonalScheduleList
                                 {
                                     PersonalSchedules = CoreKernel.PersonalScheduleRepo.GetAll()
                                 };
            return View(form_model);
        }

        public string Delete(int schedule_id, bool? confirm)
        {
            if (confirm == null)
            {
                //PersonalSchedule schedule = CoreKernel.PersonalScheduleRepo.GetById(schedule_id);
            }
            return "dsad";
        }
    }
}
