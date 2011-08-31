using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Site.Models;
using Health.Site.Models.Configuration;
using Health.Site.Models.Configuration.Providers;
using Health.Site.Models.Rules;

namespace Health.Site.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            string ser = BinaryMetadataConfigurationProvider.Serialize(new ModelMetadataConfiguration
            {
                Properties = new Dictionary<string, ModelMetadataPropertyConfiguration>
                                                                                                {
                                                                                                    {"Name", new ModelMetadataPropertyConfiguration
                                                                                                                 {
                                                                                                                     DisplayName = "Some name",
                                                                                                                     ValidatorRule = new List<IValidatorRuleConfig>
                                                                                                                                         {
                                                                                                                                             new RangeValidatorConfig
                                                                                                                                                 {
                                                                                                                                                     ErrorMessage = "Some error message",
                                                                                                                                                     Max = 500,
                                                                                                                                                     Min = 500
                                                                                                                                                 }
                                                                                                                                         }
                                                                                                                 }}
                                                                                                }
            });
            ModelMetadataConfiguration des = BinaryMetadataConfigurationProvider.Deserialize(ser);
            return View();
        }

        [HttpPost]
        public ActionResult Index(TestModel test_model)
        {
            if (ModelState.IsValid){}
            return View(test_model);
        }
    }
}
