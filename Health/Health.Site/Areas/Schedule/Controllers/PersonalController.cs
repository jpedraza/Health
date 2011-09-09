using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Core.API;
using Health.Site.Areas.Schedule.Models;
using Health.Site.Attributes;
using Health.Site.Controllers;

namespace Health.Site.Areas.Schedule.Controllers
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
    }
}
