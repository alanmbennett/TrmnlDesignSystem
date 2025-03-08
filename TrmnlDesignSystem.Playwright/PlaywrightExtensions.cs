using Microsoft.Playwright;

namespace TrmnlDesignSystem.Playwright;

public static class PlaywrightExtensions
{
    /// <summary>
    /// Returns the bytes of a browser page screenshot where the page is
    /// configured for TRMNL display.
    /// </summary>
    /// <param name="browserType"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static async Task<byte[]> GetTrmnlScreenshot(
        this IBrowserType browserType,
        TrmnlScreenshotOptions options
    )
    {
        var width = options.Width;
        var height = options.Height;

        await using var browser = options.WebsocketsEndpointUrl is null
            ? await browserType.LaunchAsync(new()
            {
                Headless = true,
                Args = [$"--window-size={width},{height}", "--disable-web-security"]
            })
            : await browserType.ConnectAsync(options.WebsocketsEndpointUrl);

        var page = await browser.NewPageAsync();
        await page.SetViewportSizeAsync(width, height);
        await page.SetContentAsync(options.Html);

        await page.EvaluateAsync(
            "document.getElementsByTagName(\"html\")[0].style.overflow = \"hidden\";"
            + "document.getElementsByTagName(\"body\")[0].style.overflow = \"hidden\";"
        );

        var bytes = await page.ScreenshotAsync(new()
        {
            Path = options.ScreenshotPath
        });

        await browser.CloseAsync();

        return bytes;
    }
}
