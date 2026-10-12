using Microsoft.Playwright;

namespace Bitdefender.QA.Tests.Pages;

/// <summary>
/// Contains the locators and actions used on the Book Store page.
/// </summary>
public class BookStorePage
{
    private readonly IPage _page;

    private ILocator SearchInput =>
        _page.Locator("#searchBox");

    private ILocator BookLink(string title) =>
        _page.GetByRole(AriaRole.Link, new()
        {
            Name = title,
            Exact = true
        });

    /// <summary>
    /// Initializes the Book Store page object.
    /// </summary>
    /// <param name="page">The current Playwright page.</param>
    public BookStorePage(IPage page)
    {
        _page = page;
    }

    /// <summary>
    /// Navigates to the Book Store page.
    /// </summary>
    /// <param name="appUrl">The configured application URL.</param>
    public async Task GoToAsync(string appUrl)
    {
        var booksUrl = new Uri(new Uri(appUrl), "/books");

        await _page.GotoAsync(booksUrl.ToString());
    }

    /// <summary>
    /// Searches for books by filling the search field.
    /// </summary>
    /// <param name="searchValue">The text used to search for books.</param>
    public async Task SearchBookAsync(string searchValue)
    {
        await SearchInput.FillAsync(searchValue);
    }

    /// <summary>
    /// Searches for books by simulating keyboard typing.
    /// </summary>
    /// <param name="searchValue">The text to type.</param>
    public async Task SearchBookByTypingAsync(string searchValue)
    {
        await SearchInput.FillAsync("");
        await SearchInput.PressSequentiallyAsync(searchValue);
    }

    /// <summary>
    /// Clears the Book Store search field.
    /// </summary>
    public async Task ClearSearchAsync()
    {
        await SearchInput.ClearAsync();
    }

    /// <summary>
    /// Gets the locator for a book title in the Book Store.
    /// </summary>
    /// <param name="title">The book title.</param>
    /// <returns>The matching book link locator.</returns>
    public ILocator GetBookLink(string title)
    {
        return BookLink(title);
    }

    /// <summary>
    /// Opens the details page for the selected book.
    /// </summary>
    /// <param name="title">The title of the book to open.</param>
    public async Task OpenBookAsync(string title)
    {
        await BookLink(title).ClickAsync();
    }
}