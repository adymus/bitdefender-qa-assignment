using Bitdefender.QA.Tests.Pages;
using Bitdefender.QA.Tests.Tests.Setup;

namespace Bitdefender.QA.Tests.Tests.BookStoreTest;

/// <summary>
/// Tests book search functionality in the Book Store.
/// </summary>
public class SearchBooks : BaseTest
{
    /// <summary>
    /// Verifies that searching by book title filters the results
    /// and clearing the search restores the book list.
    /// </summary>
    [Test]
    [Category("UI")]
    [Category("BookStore")]
    public async Task SearchBooksByTitleAsync()
    {
        const string bookTitle = "Speaking JavaScript";
        const string otherBook = "Git Pocket Guide";

        var bookStorePage = new BookStorePage(Page);

        // Open Book Store
        await bookStorePage.GoToAsync(AppUrl);

        // Verify books are initially displayed
        await Expect(bookStorePage.GetBookLink(bookTitle))
            .ToBeVisibleAsync();

        await Expect(bookStorePage.GetBookLink(otherBook))
            .ToBeVisibleAsync();

        // Search by typing the book title
        await bookStorePage.SearchBookByTypingAsync(bookTitle);

        // Verify filtered results
        await Expect(bookStorePage.GetBookLink(bookTitle))
            .ToBeVisibleAsync();

        await Expect(bookStorePage.GetBookLink(otherBook))
            .ToBeHiddenAsync();

        TestContext.Out.WriteLine(
            $"Search results correctly filtered for '{bookTitle}'.");

        // Clear search
        await bookStorePage.ClearSearchAsync();

        // Verify original books are displayed again
        await Expect(bookStorePage.GetBookLink(bookTitle))
            .ToBeVisibleAsync();

        await Expect(bookStorePage.GetBookLink(otherBook))
            .ToBeVisibleAsync();

        TestContext.Out.WriteLine(
            "Book list successfully restored after clearing search.");
    }
}