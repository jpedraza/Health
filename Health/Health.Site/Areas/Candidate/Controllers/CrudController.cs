using System.Web.Mvc;
using Health.Core.API;
using Health.Site.Controllers;

namespace Health.Site.Areas.Candidate.Controllers
{
    public class CrudController : CoreController
    {
        public CrudController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        public ActionResult RejectBid(int candidate_id)
        {
            CoreKernel.RegServ.RejectBid(new Core.Entities.POCO.Candidate {Id = candidate_id});
            return RedirectTo<HomeController>(a => a.Index());
        }

        public ActionResult AcceptBid(int candidate_id)
        {
            CoreKernel.RegServ.AcceptBid(CoreKernel.CandRepo.GetById(candidate_id));
            return RedirectTo<HomeController>(a => a.Index());
        }
    }
}
