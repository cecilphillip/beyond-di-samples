using RulesMapper.Core;
using RulesMapper.Logic;
using System.Web.Mvc;

namespace RulesMapper.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRulesEngine _engine;

        public HomeController(IRulesEngine engine)
        {
            _engine = engine;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Generator()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Mapper()
        {
            return View(_engine.RetrieveDefinitionViews());
        }

        [HttpPost]
        public ActionResult Mapper(string txtEventType, string txtName, string txtSpec, string txtHandler)
        {
            _engine.AddRule(new RuleDefinition {
                Event_Type = txtEventType,
                Spec_Code = txtSpec,
                Handler_Code = txtHandler,
                Name = txtName
            });

            return View(_engine.RetrieveDefinitionViews());
        }

    }
}