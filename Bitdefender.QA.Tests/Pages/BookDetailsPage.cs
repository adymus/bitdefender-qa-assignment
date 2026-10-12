using Microsoft.Playwright;

namespace Bitdefender.QA.Tests.Pages;

/// <summary>
/// Contains the locators and actions used on the Book Details page.
/// </summary>
public class BookDetailsPage
{
    private readonly IPage _page;

    private ILocator BookTitle =>
        _page.Locator("#title-wrapper .col-md-9 label");

    private ILocator BookIsbn =>
        _page.Locator("#ISBN-wrapper .col-md-9 label");

    private ILocator AddToCollectionButton =>
        _page.GetByRole(AriaRole.Button, new()
        {
            Name = "Add To Your Collection",
            Exact = true
        });

    private ILocator BackToBookStoreButton =>
        _page.GetByRole(AriaRole.Button, new()
        {
            Name = "Back To Book Store",
            Exact = true
        });

    /// <summary>
    /// Initializes the Book Details page object.
    /// </summary>
    /// <param name="page">The current Playwright page.</param>
    public BookDetailsPage(IPage page)
    {
        _page = page;
    }

    /// <summary>
    /// Gets the title displayed on the Book Details page.
    /// </summary>
    /// <returns>The book title.</returns>
    public async Task<string> GetBookTitleAsync()
    {
        return await BookTitle.InnerTextAsync();
    }

    /// <summary>
    /// Gets the ISBN displayed on the Book Details page.
    /// </summary>
    /// <returns>The book ISBN.</returns>
    public async Task<string> GetBookIsbnAsync()
    {
        return await BookIsbn.InnerTextAsync();
    }

    /// <summary>
    /// Adds the book to the user's collection, accepts
    /// the browser alert and returns its message.
    /// </summary>
    /// <returns>The browser alert message.</returns>
    public async Task<string> AddToCollectionAndGetAlertAsync()
    {
        var alertResult = new TaskCompletionSource<string>(
            TaskCreationOptions.RunContinuationsAsynchronously);

        async void HandleDialog(object? sender, IDialog dialog)
        {
            try
            {
                var message = dialog.Message;
                await dialog.AcceptAsync();
                alertResult.TrySetResult(message);
            }
            catch (Exception ex)
            {
                alertResult.TrySetException(ex);
            }
        }

        _page.Dialog += HandleDialog;

        try
        {
            await AddToCollectionButton.ClickAsync();

            return await alertResult.Task.WaitAsync(
                TimeSpan.FromSeconds(10));
        }
        finally
        {
            _page.Dialog -= HandleDialog;
        }
    }

    /// <summary>
    /// Returns to the Book Store list.
    /// </summary>
    public async Task BackToBookStoreAsync()
    {
        await BackToBookStoreButton.ClickAsync();
    }
}