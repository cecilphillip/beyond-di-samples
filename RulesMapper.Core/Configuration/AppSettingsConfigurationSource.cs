using System;
using System.Configuration;
using System.Linq;

namespace RulesMapper.Core.Configuration
{
    public class AppSettingsConfigurationSource : IConfigurationSource
    {
        private readonly string _prefix;

        public AppSettingsConfigurationSource() : this("dnm")
        {
        }

        public AppSettingsConfigurationSource(string prefix)
        {            
            _prefix = prefix;
        }

       
        public virtual string GetValue(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "Setting Key was NULL or Empty");
            }
          
            var prefixedKey = string.Format("{0}-{1}", _prefix, key);

            return ConfigurationManager.AppSettings.AllKeys.Contains(prefixedKey) ?
                ConfigurationManager.AppSettings[prefixedKey] : string.Empty;
        }
            
        public virtual bool Contains(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "Setting Key was NULL or Empty");
            }

            var prefixedKey = string.Format("{0}-{1}", _prefix, key);

            return ConfigurationManager.AppSettings.AllKeys.Contains(prefixedKey);
        }
    }
}
