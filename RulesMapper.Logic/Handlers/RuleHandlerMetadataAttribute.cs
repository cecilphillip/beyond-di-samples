using System;
using System.ComponentModel.Composition;

namespace RulesMapper.Logic.Handlers
{  
    [MetadataAttribute]
    public class RuleHandlerMetadataAttribute : Attribute, IRuleMetadata
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public RuleHandlerMetadataAttribute(string code, string name, string description)
        {
            Name = name;
            Description = description;
            Code = code;
        }
    }
}
