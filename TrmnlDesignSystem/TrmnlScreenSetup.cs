using Microsoft.Extensions.Configuration;

namespace TrmnlDesignSystem;

public sealed class TrmnlScreenSetup
{
    private const string _pluginsSource = "PluginsSource";

    public const string LayoutName = "_TrmnlScreenLayout";

    public string DefaultBaseUrl { get; init; } = "https://usetrmnl.com";

    [ConfigurationKeyName($"{_pluginsSource}:Css")]
    public string PluginsCss { get; init; } = "/css/latest/plugins.css";

    [ConfigurationKeyName($"{_pluginsSource}:JavaScript")]
    public string PluginsJavaScript { get; init; } = "/js/latest/plugins.js";

    internal string PluginsCssUrl => GetUrlToResource(PluginsCss);

    internal string PluginsJavaScriptUrl => GetUrlToResource(PluginsJavaScript);

    private string GetUrlToResource(string relativePath)
    {
        if (string.IsNullOrEmpty(DefaultBaseUrl))
        {
            return relativePath;
        }

        var uriBuilder = new UriBuilder(DefaultBaseUrl)
        {
            Path = relativePath
        };
        return uriBuilder.Uri.ToString();
    }
}