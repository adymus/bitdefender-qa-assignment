using Microsoft.Playwright;

namespace Bitdefender.QA.Tests.Pages;

/// <summary>
/// Contains the locators and actions used on the Profile page.
/// </summary>
public class ProfilePage
{
    private readonly IPage _page;

    private ILocator SearchInput =>
        _page.Locator("#searchBox");

    private ILocator BookRows =>
        _page.Locator("table tbody tr");

    private ILocator GoToBookStoreButton =>
        _page.Locator("#gotoStore");

    private ILocator LogoutButton =>
        _page.GetByRole(AriaRole.Button, new()
        {
            Name = "Logout",
            Exact = true
        });

    private ILocator DeleteBookButton(string isbn) =>
        _page.Locator($"span[id='delete-record-{isbn}']");

    private ILocator DeleteBookModal =>
        _page.GetByRole(AriaRole.Dialog);

    private ILocator ConfirmDeleteButton =>
        DeleteBookModal.Locator("#closeSmallModal-ok");

    private ILocator CancelDeleteButton =>
        DeleteBookModal.Locator("#closeSmallModal-cancel");

    /// <summary>
    /// Initializes the Profile page object.
    /// </summary>
    /// <param name="page">The current Playwright page.</param>
    public ProfilePage(IPage page)
    {
        _page = page;
    }

    /// <summary>
    /// Navigates to the user's Profile page.
    /// </summary>
    /// <param name="appUrl">The configured application URL.</param>
    public async Task GoToAsync(string appUrl)
    {
        var profileUrl = new Uri(new Uri(appUrl), "/profile");

        await _page.GotoAsync(profileUrl.ToString());
    }

    /// <summary>
    /// Gets the locator for a book title in the collection.
    /// </summary>
    /// <param name="title">The book title to find.</param>
    /// <returns>The matching book title locator.</returns>
    public ILocator GetBookTitle(string title)
    {
        return BookRows
            .Locator("td:nth-child(2)")
            .GetByText(title, new()
            {
                Exact = true
            });
    }

    /// <summary>
    /// Searches for books in the user's collection.
    /// </summary>
    /// <param name="searchValue">The search text.</param>
    public async Task SearchBookAsync(string searchValue)
    {
        await SearchInput.FillAsync(searchValue);
    }

    /// <summary>
    /// Clears the Profile search field.
    /// </summary>
    public async Task ClearSearchAsync()
    {
        await SearchInput.ClearAsync();
    }

    /// <summary>
    /// Gets the number of currently displayed book rows.
    /// </summary>
    /// <returns>The number of displayed books.</returns>
    public async Task<int> GetDisplayedBookCountAsync()
    {
        return await BookRows.CountAsync();
    }

    /// <summary>
    /// Opens the delete confirmation modal for a selected book.
    /// </summary>
    /// <param name="isbn">The ISBN of the book to delete.</param>
    public async Task OpenDeleteBookConfirmationAsync(string isbn)
    {
        await DeleteBookButton(isbn).ClickAsync();
    }

    /// <summary>
    /// Gets the confirmation message displayed in the Delete Book modal.
    /// </summary>
    /// <returns>The confirmation message.</returns>
    public async Task<string> GetDeleteConfirmationMessageAsync()
    {
        return await DeleteBookModal
            .Locator(".modal-body")
            .InnerTextAsync();
    }

    /// <summary>
    /// Confirms deletion of the selected book.
    /// </summary>
    public async Task ConfirmDeleteBookAsync()
    {
        await ConfirmDeleteButton.ClickAsync();
    }

    /// <summary>
    /// Cancels deletion of the selected book.
    /// </summary>
    public async Task CancelDeleteBookAsync()
    {
        await CancelDeleteButton.ClickAsync();
    }

    /// <summary>
    /// Navigates from Profile to the Book Store.
    /// </summary>
    public async Task GoToBookStoreAsync()
    {
        await GoToBookStoreButton.ClickAsync();
    }

    /// <summary>
    /// Logs out the current user.
    /// </summary>
    public async Task LogoutAsync()
    {
        await LogoutButton.ClickAsync();
    }
}