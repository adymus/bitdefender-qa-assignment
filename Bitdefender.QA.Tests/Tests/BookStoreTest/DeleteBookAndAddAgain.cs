using Bitdefender.QA.Tests.Api;
using Bitdefender.QA.Tests.Pages;
using Bitdefender.QA.Tests.Tests.Setup;

namespace Bitdefender.QA.Tests.Tests.BookStoreTest;

/// <summary>
/// Tests deleting a book and adding it again to the collection.
/// </summary>
public class DeleteBookAndAddAgain : BaseTest
{
    /// <summary>
    /// Verifies that a book can be deleted and added again successfully.
    /// </summary>
    [Test]
    [Category("UI")]
    [Category("BookStore")]
    public async Task DeleteBookAndAddAgainAsync()
    {
        const string bookTitle = "Speaking JavaScript";
        const string bookIsbn = "9781449365035";

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

        // Add book to collection
        await bookStorePage.GoToAsync(AppUrl);
        await bookStorePage.OpenBookAsync(bookTitle);

        var addAlert = await bookDetailsPage.AddToCollectionAndGetAlertAsync();

        Assert.That(addAlert, Is.EqualTo("Book added to your collection."));

        // Verify book exists in Profile
        await profilePage.GoToAsync(AppUrl);

        await Expect(profilePage.GetBookTitle(bookTitle))
            .ToBeVisibleAsync();

        // Delete book
        await profilePage.OpenDeleteBookConfirmationAsync(bookIsbn);

        var confirmationMessage =
            await profilePage.GetDeleteConfirmationMessageAsync();

        Assert.That(
            confirmationMessage,
            Is.EqualTo("Do you want to delete this book?"));

        await profilePage.ConfirmDeleteBookAsync();

        // Verify book was removed
        await Expect(profilePage.GetBookTitle(bookTitle))
            .ToHaveCountAsync(0);

        TestContext.Out.WriteLine($"Book deleted: {bookTitle}");

        // Add the same book again
        await bookStorePage.GoToAsync(AppUrl);
        await bookStorePage.OpenBookAsync(bookTitle);

        var reAddAlert = await bookDetailsPage.AddToCollectionAndGetAlertAsync();

        Assert.That(reAddAlert, Is.EqualTo("Book added to your collection."));

        // Verify book was added again
        await profilePage.GoToAsync(AppUrl);

        await Expect(profilePage.GetBookTitle(bookTitle))
            .ToBeVisibleAsync();

        Assert.That(
            await profilePage.GetDisplayedBookCountAsync(),
            Is.EqualTo(1));

        // Verify persistence after refresh
        await Page.ReloadAsync();

        await Expect(profilePage.GetBookTitle(bookTitle))
            .ToBeVisibleAsync();

        TestContext.Out.WriteLine(
            $"Book '{bookTitle}' was successfully added again.");
    }
}