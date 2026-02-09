namespace Rossina.API.Extensions;

internal static class ConfigurationExtensions
{
    internal static void AddModuleConfiguration(this IConfigurationBuilder builder, string[] modules)
    {
        foreach (var module in modules)
        {
            builder.AddJsonFile($"modules.{module}.json", false, true);
            builder.AddJsonFile($"modules.{module}.Development.json", true, true);
            
        }
    }
}