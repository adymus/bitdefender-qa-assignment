using Microsoft.Playwright;
using System.Text.Json;

namespace Bitdefender.QA.Tests.Api;

/// <summary>
/// Provides Account API operations for creating test users,
/// generating authentication tokens and deleting users.
/// </summary>
public class AccountApiClient
{
    private readonly IAPIRequestContext _request;

    /// <summary>
    /// Initializes the Account API client.
    /// </summary>
    /// <param name="request">
    /// The Playwright API request context.
    /// </param>
    public AccountApiClient(IAPIRequestContext request)
    {
        _request = request;
    }

    /// <summary>
    /// Creates a new user through the Account API.
    /// </summary>
    /// <param name="username">The username for the new account.</param>
    /// <param name="password">The password for the new account.</param>
    /// <returns>The ID of the created user.</returns>
    public async Task<string> CreateUserAsync(
        string username,
        string password)
    {
        var response = await _request.PostAsync(
            "https://demoqa.com/Account/v1/User",
            new()
            {
                DataObject = new
                {
                    userName = username,
                    password
                }
            });

        var responseBody = await response.TextAsync();

        if (response.Status != 201)
        {
            throw new Exception(
                $"Create User failed. Status: {response.Status}. Response: {responseBody}");
        }

        using var document = JsonDocument.Parse(responseBody);

        var userId = document.RootElement
            .GetProperty("userID")
            .GetString();

        if (string.IsNullOrEmpty(userId))
        {
            throw new Exception(
                "Create User returned an empty userID.");
        }

        return userId;
    }

    /// <summary>
    /// Generates a Bearer token for an existing user.
    /// </summary>
    /// <param name="username">The account username.</param>
    /// <param name="password">The account password.</param>
    /// <returns>The generated Bearer token.</returns>
    public async Task<string> GenerateTokenAsync(
        string username,
        string password)
    {
        var response = await _request.PostAsync(
            "https://demoqa.com/Account/v1/GenerateToken",
            new()
            {
                DataObject = new
                {
                    userName = username,
                    password
                }
            });

        var responseBody = await response.TextAsync();

        if (response.Status != 200)
        {
            throw new Exception(
                $"Generate Token failed. Status: {response.Status}. Response: {responseBody}");
        }

        using var document = JsonDocument.Parse(responseBody);

        var token = document.RootElement
            .GetProperty("token")
            .GetString();

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception(
                "Generate Token returned an empty token.");
        }

        return token;
    }

    /// <summary>
    /// Deletes a user using the account ID and a valid Bearer token.
    /// </summary>
    /// <param name="userId">The ID of the user to delete.</param>
    /// <param name="token">A valid Bearer token for the account.</param>
    public async Task DeleteUserAsync(
        string userId,
        string token)
    {
        var response = await _request.DeleteAsync(
            $"https://demoqa.com/Account/v1/User/{Uri.EscapeDataString(userId)}",
            new()
            {
                Headers = new Dictionary<string, string>
                {
                    ["Authorization"] = $"Bearer {token}"
                }
            });

        if (response.Status != 204)
        {
            var responseBody = await response.TextAsync();

            throw new Exception(
                $"Delete User failed. Status: {response.Status}. Response: {responseBody}");
        }
    }
}