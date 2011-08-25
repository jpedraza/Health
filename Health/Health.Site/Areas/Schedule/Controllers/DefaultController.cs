using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities.POCO;
using Health.Data.Repository.Fake;
using Health.Site.Areas.Schedule.Models;
using Health.Site.Areas.Schedule.Models.Forms;
using Health.Site.Controllers;

namespace Health.Site.Areas.Schedule.Controllers
{
    public class DefaultController : CoreController
    {
        //
        // GET: /Schedule/DefaultSchedule/

        public DefaultController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        public ActionResult Index()
        {
            var view_model = new ScheduleViewModel
                                 {
                                     DefaultScheduleForm = new DefaultScheduleForm
                                                               {
                                                                   DefaultSchedules =
                                                                       new DefaultScheduleFakeRepository(DIKernel,
                                                                                                         CoreKernel).
                                                                       GetAll()
                                                               }
                                 };
            return View(view_model);
        }

    }
}
