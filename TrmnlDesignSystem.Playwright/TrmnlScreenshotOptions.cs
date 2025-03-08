namespace TrmnlDesignSystem.Playwright;

public sealed class TrmnlScreenshotOptions
{
    public required string ScreenshotPath { get; init; }

    public string? WebsocketsEndpointUrl { get; init; }

    public required string Html { get; init; }

    public int Width { get; init; } = 800;

    public int Height { get; init; } = 480;
}
