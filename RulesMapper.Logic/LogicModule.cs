using Autofac;
using Autofac.Extras.AttributeMetadata;
using Autofac.Integration.Mef;
using RulesMapper.Logic.Handlers;
using RulesMapper.Logic.Specs;
using System.Linq;

namespace RulesMapper.Logic
{
    public class LogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Enables the use of Interface based metadata
            builder.RegisterMetadataRegistrationSources();

            // Enables the use of metadata attributes 
            builder.RegisterModule<AttributedMetadataModule>();

            var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();

            #region Register IRuleSpecs
            
            currentAssembly.GetTypes()
                           .Where(t => t.GetInterfaces().Contains(typeof(IRuleSpec)) && !t.IsAbstract)
                           .ForEach(specType => {

                               builder.RegisterType(specType).As<IRuleSpec>()
                                      .WithAttributeFilter();
                                        
                           });
            #endregion

            #region Register IRuleHandlers
            
            currentAssembly.GetTypes()
                           .Where(t => t.GetInterfaces().Contains(typeof(IRuleHandler)) && !t.IsAbstract)
                           .ForEach(handlerType => {

                               builder.RegisterType(handlerType).As<IRuleHandler>()
                                      .WithAttributeFilter();
                           });

            #endregion 

            builder.RegisterType<RulesEngine>().As<IRulesEngine>();
        }
    }
}
