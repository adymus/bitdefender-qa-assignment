using Bitdefender.QA.Tests.Api;
using Bitdefender.QA.Tests.Pages;
using Bitdefender.QA.Tests.Tests.Setup;

namespace Bitdefender.QA.Tests.Tests.BookStoreTest;

/// <summary>
/// Tests adding a book to a user's collection.
/// </summary>
public class AddBookToCollection : BaseTest
{
    /// <summary>
    /// Verifies that a book is added successfully and
    /// remains in the collection after refreshing the page.
    /// </summary>
    [Test]
    [Category("UI")]
    [Category("BookStore")]
    public async Task AddBookToCollectionAndVerifyPersistenceAsync()
    {
        const string bookTitle = "Speaking JavaScript";
        const string bookIsbn = "9781449365035";

        await using var user = await TemporaryUser.CreateAsync(Playwright);

        var loginPage = new LoginPage(Page);
        var bookStorePage = new BookStorePage(Page);
        var bookDetailsPage = new BookDetailsPage(Page);
        var profilePage = new ProfilePage(Page);

        // Login with temporary user
        await loginPage.GoToAsync(AppUrl);
        await loginPage.LoginAsync(user.Username, user.Password);

        await Expect(Page.Locator("#userName-value"))
            .ToHaveTextAsync(user.Username);

        // Open book details
        await bookStorePage.GoToAsync(AppUrl);
        await bookStorePage.OpenBookAsync(bookTitle);

        var actualTitle = await bookDetailsPage.GetBookTitleAsync();
        var actualIsbn = await bookDetailsPage.GetBookIsbnAsync();

        Assert.Multiple(() =>
        {
            Assert.That(actualTitle, Is.EqualTo(bookTitle));
            Assert.That(actualIsbn, Is.EqualTo(bookIsbn));
        });

        // Add book to collection
        var alertMessage = await bookDetailsPage.AddToCollectionAndGetAlertAsync();

        Assert.That(alertMessage, Is.Not.Empty);
        TestContext.Out.WriteLine($"Add book alert: {alertMessage}");

        // Verify book in Profile
        await profilePage.GoToAsync(AppUrl);

        await Expect(profilePage.GetBookTitle(bookTitle))
            .ToBeVisibleAsync();

        // Verify persistence after refresh
        await Page.ReloadAsync();

        await Expect(profilePage.GetBookTitle(bookTitle))
            .ToBeVisibleAsync();

        TestContext.Out.WriteLine(
            $"Book '{bookTitle}' persisted after refresh.");
    }
}