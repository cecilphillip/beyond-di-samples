using Autofac;
using RulesMapper.Core;
using RulesMapper.Logic;
using RulesMapper.Web.App_Start;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

namespace RulesMapper.Web.Controllers.Api
{
    [RoutePrefix("api/events")]
    public class EventsController : ApiController
    {
        private IRulesEngine _engine;

        public EventsController(IRulesEngine engine)
        {
            _engine = engine;
        }

        [HttpGet]
        [Route("specs")]
        public IHttpActionResult GetSpecs() {
            return Ok(_engine.RetrieveSpecs());
        }

        [HttpGet]
        [Route("handlers")]
        public IHttpActionResult GetHandlers()
        {
            return Ok(_engine.RetrieveHandlers());
        }

        [HttpGet]
        [Route("definitions")]
        public IHttpActionResult GetDefinitions()
        {
            return Ok(_engine.RetrieveDefinitions());
        }

        [HttpGet]
        [Route("definitionviews")]
        public IHttpActionResult GetDefinitionsView()
        {
            return Ok(_engine.RetrieveDefinitionViews());
        }


        [HttpPost]
        [Route("evaluate")]
        public IHttpActionResult Evaluate(Event state)
        {
            // Don't make this habbit in your production application
            HostingEnvironment.QueueBackgroundWorkItem(ct =>
            {
                try
                {
                    using (var nestedScope = Bootstrapper.Container.BeginLifetimeScope())
                    {
                        var nestedEngine = nestedScope.Resolve<IRulesEngine>();
                        return nestedEngine.ExecuteAsync(state);
                    }
                }
                catch (Exception e) {

                    return Task.FromException(e);
                }
            });

            return StatusCode(HttpStatusCode.Accepted);
        }
    }
}
