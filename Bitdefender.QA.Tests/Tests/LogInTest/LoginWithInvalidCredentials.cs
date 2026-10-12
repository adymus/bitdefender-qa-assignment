using Bitdefender.QA.Tests.Pages;
using Bitdefender.QA.Tests.Tests.Setup;
using System.Text.RegularExpressions;

namespace Bitdefender.QA.Tests.Tests.LogInTest;

/// <summary>
/// Tests login rejection and Profile access with invalid credentials.
/// </summary>
public class LoginWithInvalidCredentials : BaseTest
{
    /// <summary>
    /// Verifies that an incorrect password is rejected
    /// and Profile information remains inaccessible.
    /// </summary>
    [Category("Login")]
    [Test(Description = "TC-03 - Login with invalid credentials")]
    public async Task LoginWithInvalidCredentialsAsync()
    {
        var loginPage = new LoginPage(Page);

        await loginPage.GoToAsync(AppUrl);
        await loginPage.LoginAsync(AdminUser, "wrongPassword123!");

        await Expect(Page.Locator("#name"))
            .ToHaveTextAsync("Invalid username or password!");

        await Expect(Page).ToHaveURLAsync(
            new Regex(".*/login/?$"));

        await Page.GotoAsync($"{AppUrl.TrimEnd('/')}/profile");

        await Expect(Page.Locator("body"))
            .ToContainTextAsync(
                "Currently you are not logged into the Book Store application");
    }
}