using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using RethinkDb.Driver;
using RethinkDb.Driver.Net;
using RulesMapper.Core.Configuration;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using RulesMapper.Core;

namespace RulesMapper.Storage
{
    public class EventStore : IEventStore
    {
        private const string DATABASE_NAME = "rulemapper";
        private const string EVENTS_TABLE_NAME = "events";
        private const string RULE_DEFINITION_TABLE_NAME = "ruledefinitions";

        private IConfigurationSource _config;
        protected static Connection _conn;
        protected static RethinkDB _r = RethinkDB.r;
        public EventStore(IConfigurationSource config)
        {
            this._config = config;
            var hostname = config.GetValue("rethink-ip");
            var port = int.Parse(config.GetValue("rethink-port"));

            if (_conn == null)
            {
                _conn = _r.connection()
                          .hostname(hostname)
                          .port(port)
                          .connect();
            }
        }
        public virtual void AddEventItem(JObject state)
        {
            dynamic expand = JsonConvert.DeserializeObject<ExpandoObject>(state.ToString(), new ExpandoObjectConverter());

            var result = _r.db(DATABASE_NAME).table(EVENTS_TABLE_NAME)
                .insert(expand).run(_conn);

        }

        public virtual void AddEventItem(Event state)
        {
            var result = _r.db(DATABASE_NAME).table(EVENTS_TABLE_NAME)
                .insert(state).run(_conn);
        }

        public virtual IEnumerable<Event> GetEvents()
        {
            return _r.db(DATABASE_NAME).table(EVENTS_TABLE_NAME)
                  .runCursor<Event>(_conn).ToList();
        }

        public virtual void AddRuleDefinition(RuleDefinition def)
        {
            var rs = _r.db(DATABASE_NAME).table(RULE_DEFINITION_TABLE_NAME).insert(def)
                .runResult(_conn);
        }

        public virtual IEnumerable<RuleDefinition> GetRuleDefinitions()
        {
            var cursor =  _r.db(DATABASE_NAME).table(RULE_DEFINITION_TABLE_NAME)
                 .runCursor<RuleDefinition>(_conn);
            
            return cursor.ToList();
        }
    }
}
