namespace RulesMapper.Core.Configuration
{
    public interface IConfigurationSource
    {
        string GetValue(string key);

        bool Contains(string key);
    }
}
