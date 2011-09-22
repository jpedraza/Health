using System.Web.Mvc;
using Health.Core.API;
using Health.Site.Areas.Candidates.Models;
using Health.Site.Controllers;

namespace Health.Site.Areas.Candidates.Controllers
{
    public class HomeController : CoreController
    {
        public HomeController(IDIKernel di_kernel) : base(di_kernel) { }

        public ActionResult Index()
        {
            var form_model = new CandidateList()
                                 {
                                     Candidates = CoreKernel.CandRepo.GetAll()
                                 };
            return View(form_model);
        }
    }
}
