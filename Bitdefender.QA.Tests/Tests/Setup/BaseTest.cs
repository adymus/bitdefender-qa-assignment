using Microsoft.Playwright.NUnit;

namespace Bitdefender.QA.Tests.Tests.Setup;

/// <summary>
/// Base class for UI tests. Provides the Playwright page
/// and reads test configuration from test.runsettings.
/// </summary>
public abstract class BaseTest : PageTest
{
    /// <summary>
    /// Gets the application URL from test.runsettings.
    /// </summary>
    protected string AppUrl =>
        TestContext.Parameters.Get("AppUrl", "")!;

    /// <summary>
    /// Gets the test account username from test.runsettings.
    /// </summary>
    protected string AdminUser =>
        TestContext.Parameters.Get("AdminUser", "")!;

    /// <summary>
    /// Gets the test account password from test.runsettings.
    /// </summary>
    protected string AdminPassword =>
        TestContext.Parameters.Get("AdminPassword", "")!;
}