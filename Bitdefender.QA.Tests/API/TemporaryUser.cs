using Microsoft.Playwright;

namespace Bitdefender.QA.Tests.Api;

/// <summary>
/// Manages a temporary test user through the Account API.
/// Automatically deletes the user when disposed.
/// </summary>
public sealed class TemporaryUser : IAsyncDisposable
{
    private readonly IAPIRequestContext _request;
    private readonly AccountApiClient _accountApi;
    private readonly string _userId;
    private readonly string _token;

    public string Username { get; }
    public string Password { get; }

    private TemporaryUser(
        IAPIRequestContext request,
        AccountApiClient accountApi,
        string username,
        string password,
        string userId,
        string token)
    {
        _request = request;
        _accountApi = accountApi;
        Username = username;
        Password = password;
        _userId = userId;
        _token = token;
    }

    /// <summary>
    /// Creates a temporary user and generates an authentication token.
    /// </summary>
    /// <param name="playwright">The Playwright instance.</param>
    /// <returns>A temporary user ready for testing.</returns>
    public static async Task<TemporaryUser> CreateAsync(
        IPlaywright playwright)
    {
        var request = await playwright.APIRequest.NewContextAsync();
        var accountApi = new AccountApiClient(request);

        var username = $"qa_book_{Guid.NewGuid():N}"[..20];
        var password = "TestQA2026@";

        string? userId = null;

        try
        {
            userId = await accountApi.CreateUserAsync(
                username, password);

            var token = await accountApi.GenerateTokenAsync(
                username, password);

            TestContext.Out.WriteLine("=== API SETUP ===");
            TestContext.Out.WriteLine($"Created user: {username}");
            TestContext.Out.WriteLine($"User ID: {userId}");
            TestContext.Out.WriteLine("Bearer token generated.");

            return new TemporaryUser(
                request,
                accountApi,
                username,
                password,
                userId,
                token);
        }
        catch
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    var token = await accountApi.GenerateTokenAsync(
                        username, password);

                    await accountApi.DeleteUserAsync(userId, token);
                }
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine(
                    $"Setup cleanup failed: {ex.Message}");
            }
            finally
            {
                await request.DisposeAsync();
            }

            throw;
        }
    }

    /// <summary>
    /// Deletes the temporary user and releases the API request context.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        try
        {
            // Generate a fresh token before cleanup
            var cleanupToken = await _accountApi.GenerateTokenAsync(
                Username, Password);

            await _accountApi.DeleteUserAsync(_userId, cleanupToken);

            TestContext.Out.WriteLine("");
            TestContext.Out.WriteLine("=== API CLEANUP ===");
            TestContext.Out.WriteLine($"Deleted user: {Username}");
            TestContext.Out.WriteLine($"User ID: {_userId}");
        }
        finally
        {
            await _request.DisposeAsync();
        }
    }
}