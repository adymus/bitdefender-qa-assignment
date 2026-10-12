using Bitdefender.QA.Tests.Api;
using Microsoft.Playwright;

namespace Bitdefender.QA.Tests.Tests.API;

/// <summary>
/// Tests Account API operations used for test data setup and cleanup.
/// </summary>
public class AccountApiTests
{
    /// <summary>
    /// Verifies that a temporary user can be created,
    /// authenticated through a Bearer token and deleted.
    /// </summary>
    [Test]
    [Category("API")]
    public async Task CreateGenerateTokenAndDeleteUserAsync()
    {
        using var playwright = await Playwright.CreateAsync();

        var request = await playwright.APIRequest.NewContextAsync();
        var accountApi = new AccountApiClient(request);

        var username = $"qa_api_{Guid.NewGuid():N}"[..19];
        var password = "TestQA2026@";

        string? userId = null;
        string? token = null;
        bool userDeleted = false;

        try
        {
            // Create a temporary test account
            userId = await accountApi.CreateUserAsync(
                username, password);

            Assert.That(userId, Is.Not.Empty);

            TestContext.Out.WriteLine("=== CREATE USER ===");
            TestContext.Out.WriteLine($"Username: {username}");
            TestContext.Out.WriteLine($"User ID: {userId}");
            TestContext.Out.WriteLine("Result: 201 Created");

            // Generate a Bearer token for the new account
            token = await accountApi.GenerateTokenAsync(
                username, password);

            Assert.That(token, Is.Not.Empty);

            TestContext.Out.WriteLine("");
            TestContext.Out.WriteLine("=== GENERATE TOKEN ===");
            TestContext.Out.WriteLine($"Username: {username}");
            TestContext.Out.WriteLine(
                $"Bearer token: {token[..Math.Min(12, token.Length)]}... (masked)");
            TestContext.Out.WriteLine("Result: 200 OK");

            // Delete the temporary account
            await accountApi.DeleteUserAsync(userId, token);

            userDeleted = true;

            TestContext.Out.WriteLine("");
            TestContext.Out.WriteLine("=== DELETE USER ===");
            TestContext.Out.WriteLine($"Username: {username}");
            TestContext.Out.WriteLine($"User ID: {userId}");
            TestContext.Out.WriteLine("Result: 204 No Content");
            TestContext.Out.WriteLine("Account deleted successfully.");
        }
        finally
        {
            try
            {
                // Attempt cleanup if the account was created
                // but the normal deletion did not complete.
                if (!userDeleted && !string.IsNullOrEmpty(userId))
                {
                    TestContext.Out.WriteLine("");
                    TestContext.Out.WriteLine(
                        "=== CLEANUP: RETRY DELETE ===");

                    token ??= await accountApi.GenerateTokenAsync(
                        username, password);

                    await accountApi.DeleteUserAsync(userId, token);

                    TestContext.Out.WriteLine(
                        $"Deleted test account: {username} ({userId})");
                }
            }
            finally
            {
                await request.DisposeAsync();
            }
        }
    }
}