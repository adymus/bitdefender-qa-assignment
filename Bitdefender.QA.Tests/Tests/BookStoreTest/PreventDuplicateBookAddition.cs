using Bitdefender.QA.Tests.Api;
using Bitdefender.QA.Tests.Pages;
using Bitdefender.QA.Tests.Tests.Setup;

namespace Bitdefender.QA.Tests.Tests.BookStoreTest;

/// <summary>
/// Tests duplicate book prevention in the user's collection.
/// </summary>
public class PreventDuplicateBookAddition : BaseTest
{
    /// <summary>
    /// Verifies that adding the same book twice is rejected
    /// and only one copy remains in the collection.
    /// </summary>
    [Test]
    [Category("UI")]
    [Category("BookStore")]
    public async Task PreventDuplicateBookAdditionAsync()
    {
        const string bookTitle = "Speaking JavaScript";
        const string duplicateMessage =
            "Book already present in the your collection!";

        await using var user = await TemporaryUser.CreateAsync(Playwright);

        var loginPage = new LoginPage(Page);
        var bookStorePage = new BookStorePage(Page);
        var bookDetailsPage = new BookDetailsPage(Page);
        var profilePage = new ProfilePage(Page);

        // Login
        await loginPage.GoToAsync(AppUrl);
        await loginPage.LoginAsync(user.Username, user.Password);

        await Expect(Page.Locator("#userName-value"))
            .ToHaveTextAsync(user.Username);

        // Open the selected book
        await bookStorePage.GoToAsync(AppUrl);
        await bookStorePage.OpenBookAsync(bookTitle);

        // Add the book for the first time
        var firstAlert =
            await bookDetailsPage.AddToCollectionAndGetAlertAsync();

        Assert.That(firstAlert, Is.EqualTo("Book added to your collection."));

        // Try to add the same book again
        var duplicateAlert =
            await bookDetailsPage.AddToCollectionAndGetAlertAsync();

        Assert.That(duplicateAlert, Is.EqualTo(duplicateMessage));

        TestContext.Out.WriteLine($"Duplicate alert: {duplicateAlert}");

        // Verify only one book exists in Profile
        await profilePage.GoToAsync(AppUrl);

        await Expect(profilePage.GetBookTitle(bookTitle))
            .ToBeVisibleAsync();

        Assert.That(
            await profilePage.GetDisplayedBookCountAsync(),
            Is.EqualTo(1));

        // Verify collection after refresh
        await Page.ReloadAsync();

        await Expect(profilePage.GetBookTitle(bookTitle))
            .ToBeVisibleAsync();

        Assert.That(
            await profilePage.GetDisplayedBookCountAsync(),
            Is.EqualTo(1));

        TestContext.Out.WriteLine(
            $"Duplicate prevented: only one copy of '{bookTitle}' exists.");
    }
}