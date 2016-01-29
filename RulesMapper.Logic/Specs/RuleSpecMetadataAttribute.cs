using RulesMapper.Logic.Handlers;
using System;
using System.ComponentModel.Composition;

namespace RulesMapper.Logic.Specs
{
    [MetadataAttribute]
    public class RuleSpecMetadataAttribute : Attribute, IRuleMetadata
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string Description { get; set; }

        public RuleSpecMetadataAttribute(string code, string name, string description = "")
        {
            Code = code;
            Name = name;
            Description = description;
        }
    }
}
