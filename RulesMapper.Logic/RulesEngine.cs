using RulesMapper.Core;
using RulesMapper.Logic.Handlers;
using RulesMapper.Logic.Specs;
using RulesMapper.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RulesMapper.Logic
{
    public interface IRulesEngine
    {
        Task<OperationResult> ExecuteAsync(Event state);
        IEnumerable<IRuleMetadata> RetrieveSpecs();
        IEnumerable<IRuleMetadata> RetrieveHandlers();
        IEnumerable<RuleDefinition> RetrieveDefinitions();
        IEnumerable<RuleDefinitionView> RetrieveDefinitionViews();
        void AddRule(RuleDefinition ruleDefinition);
    }

    public class RulesEngine : IRulesEngine
    {
        private readonly IEnumerable<Lazy<IRuleSpec, IRuleMetadata>> _specs;
        private readonly IEnumerable<Lazy<IRuleHandler, IRuleMetadata>> _handlers;

        private IEventStore _eventStore;
        public RulesEngine(IEnumerable<Lazy<IRuleSpec, IRuleMetadata>> specs, IEnumerable<Lazy<IRuleHandler, IRuleMetadata>> handlers, IEventStore eventStore)
        {
            if (!specs.Any())
            {
                throw new ArgumentException("No Evaluators Registered", "evaluators");
            }

            _handlers = handlers;
            _specs = specs;
            _eventStore = eventStore;
        }

        public async Task<OperationResult> ExecuteAsync(Event state)
        {
            try
            {
                state.Date_Created = DateTime.UtcNow;
                _eventStore.AddEventItem(state);

                var ruleMapping =  _eventStore.GetRuleDefinitions().Where(r => r.Event_Type == state.Event_Type);

                if (!ruleMapping.Any())
                {
                    return new OperationResult(OperationStatus.Failure, String.Format("A mapping for {0} does not exisit.", state.Event_Type));
                }

                //this looks over every rule definition
                var evaluations = ruleMapping.Select(c =>
                {
                    var evaluator = _specs.Where(e => e.Metadata.Code == c.Spec_Code).Select(e => e.Value).SingleOrDefault();

                    var task = (evaluator == null ? Task.FromResult(false) : evaluator.IsSatisfiedBy(state));

                    return task.ContinueWith(t =>
                                 {
                                     if (!t.IsFaulted && t.Result)
                                     {
                                         var handler = _handlers.Where(e => e.Metadata.Code == c.Handler_Code).Select(e => e.Value).SingleOrDefault();
                                         if (handler != null) handler.Execute(state);
                                     }
                                 }, TaskContinuationOptions.ExecuteSynchronously);

                });

                await Task.WhenAll(evaluations).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return new OperationResult(OperationStatus.Failure, "Unable to evaluate event");
            }

            return new OperationResult(OperationStatus.Success, "Works, I guess?");
        }

        public IEnumerable<IRuleMetadata> RetrieveSpecs()
        {
            return _specs.Select(s => s.Metadata);
        }
        public IEnumerable<IRuleMetadata> RetrieveHandlers()
        {
            return _handlers.Select(s => s.Metadata);
        }

        public IEnumerable<RuleDefinition> RetrieveDefinitions()
        {
            return _eventStore.GetRuleDefinitions();
        }

        public IEnumerable<RuleDefinitionView> RetrieveDefinitionViews()
        {
            var result = new List<RuleDefinitionView>();
            _eventStore.GetRuleDefinitions().ForEach(d =>
            {
                var vm = new RuleDefinitionView
                {
                    Name = d.Name,
                    Event_Type = d.Event_Type,
                };

                var handler = _handlers.SingleOrDefault(h => h.Metadata.Code == d.Handler_Code);
                if (handler != null)
                {
                    vm.Handler_Name = handler.Metadata.Name;
                    vm.Handler_Description = handler.Metadata.Description;
                }

                var spec = _specs.SingleOrDefault(h => h.Metadata.Code == d.Spec_Code);
                if (handler != null)
                {
                    vm.Spec_Name = spec.Metadata.Name;
                    vm.Spec_Description = spec.Metadata.Description;
                }

                result.Add(vm);
            });


            return result;
        }

        public void AddRule(RuleDefinition ruleDefinition)
        {
            _eventStore.AddRuleDefinition(ruleDefinition);
        }
    }
}
