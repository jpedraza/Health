using System.Collections.Generic;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities.POCO;
using Health.Site.Models;

namespace Health.Site.Controllers
{
    public class TestController : CoreController
    {
        public TestController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        public ActionResult Index()
        {
            var test_model = new TestModel()
                                 {
                                     Enother = new List<Patient>
                                                   {
                                                       new Patient
                                                           {
                                                               Login = "some1"
                                                           },
                                                       new Patient
                                                           {
                                                               Login = "some2"
                                                           }
                                                   }
                                 };
            return View(test_model);
        }

        [HttpPost]
        public ActionResult Index(TestModel test_model)
        {
            if (ModelState.IsValid){}
            return View(test_model);
        }
    }
}
