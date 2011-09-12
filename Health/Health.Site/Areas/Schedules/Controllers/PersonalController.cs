using System.Web.Mvc;
using Health.Core.API;
using Health.Site.Areas.Schedules.Models;
using Health.Site.Controllers;

namespace Health.Site.Areas.Schedules.Controllers
{
    public class PersonalController : CoreController
    {
        public PersonalController(IDIKernel di_kernel) : base(di_kernel) { }

        public ActionResult Index()
        {
            var form_model = new PersonalScheduleList()
                                 {
                                     PersonalSchedules = CoreKernel.PersonalScheduleRepo.GetAll()
                                 };
            return View(form_model);
        }
    }
}
