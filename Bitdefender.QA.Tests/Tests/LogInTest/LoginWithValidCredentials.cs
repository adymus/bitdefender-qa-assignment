using Bitdefender.QA.Tests.Pages;
using Bitdefender.QA.Tests.Tests.Setup;
using System.Text.RegularExpressions;


namespace Bitdefender.QA.Tests.Tests.LogInTest;

/// <summary>
/// Tests successful login using the configured test account.
/// </summary>
public class LoginWithValidCredentials : BaseTest
{
    /// <summary>
    /// Verifies that valid credentials redirect the user to Profile
    /// and display the correct username.
    /// </summary>
    [Category("Login")]
    [Test(Description = "Login - Valid credentials should redirect user to profile page")]
    public async Task LoginWithValidCredentialsAsync()
    {
        var loginPage = new LoginPage(Page);

        await loginPage.GoToAsync(AppUrl);
        await loginPage.LoginAsync(AdminUser, AdminPassword);

        await Expect(Page).ToHaveURLAsync(
            new Regex(".*/profile/?$"));

        await Expect(Page.Locator("#userName-value"))
            .ToHaveTextAsync(AdminUser);
    }
}