using Microsoft.Playwright;

namespace Bitdefender.QA.Tests.Pages;

/// <summary>
/// Contains the locators and actions used on the Login page.
/// </summary>
public class LoginPage
{
    private readonly IPage _page;

    private ILocator UsernameInput => _page.Locator("#userName");
    private ILocator PasswordInput => _page.Locator("#password");
    private ILocator LoginButton => _page.Locator("#login");
    private ILocator ErrorMessage => _page.Locator("#name");

    /// <summary>
    /// Initializes the Login page object.
    /// </summary>
    /// <param name="page">The current Playwright page.</param>
    public LoginPage(IPage page)
    {
        _page = page;
    }

    /// <summary>
    /// Navigates to the application's Login page.
    /// </summary>
    /// <param name="appUrl">The base application URL.</param>
    public async Task GoToAsync(string appUrl)
    {
        await _page.GotoAsync($"{appUrl.TrimEnd('/')}/login");
    }

    /// <summary>
    /// Enters the user credentials and submits the Login form.
    /// </summary>
    /// <param name="username">The username used for login.</param>
    /// <param name="password">The password used for login.</param>
    public async Task LoginAsync(string username, string password)
    {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
    }

    /// <summary>
    /// Gets the error message displayed after an unsuccessful login.
    /// </summary>
    /// <returns>The login error message.</returns>
    public async Task<string> GetErrorMessageAsync()
    {
        return await ErrorMessage.InnerTextAsync();
    }
}