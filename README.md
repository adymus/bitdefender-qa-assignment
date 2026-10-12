# Bitdefender QA Assignment

This repository contains my QA Engineer take-home assignment for the [DemoQA Book Store Application](https://demoqa.com/books).

The assignment covers manual test design, exploratory testing, UI and API automation using C# and Playwright, and a testing strategy for an AI-powered security assistant.

## Project Structure

```text
bitdefender-qa-assignment/
|
|-- Part1-TestDesign/
|   |-- test-plan.md
|   |-- test-cases.md
|   |-- exploratory-findings.md
|   `-- api-checks.md
|
|-- Bitdefender.QA.Tests/
|   |-- Api/
|   |   |-- AccountApiClient.cs
|   |   `-- TemporaryUser.cs
|   |
|   |-- Pages/
|   |   |-- LoginPage.cs
|   |   |-- BookStorePage.cs
|   |   |-- BookDetailsPage.cs
|   |   `-- ProfilePage.cs
|   |
|   |-- Tests/
|   |   |-- Setup/
|   |   |   `-- BaseTest.cs
|   |   |-- LoginTest/
|   |   |-- BookStoreTest/
|   |   `-- APITest/
|   |
|   `-- test.runsettings
|
|-- Bitdefender.QA.sln
`-- README.md
```

## Part 1 - Test Design & Planning

I performed manual and exploratory testing of the Book Store Application, focusing on authentication, book searching, collection management, and negative scenarios.

I inspected browser Console and Network activity and investigated selected Account API operations using Swagger and Postman.

The documentation includes:

- [Test Plan](Part1-TestDesign/test-plan.md) - Testing scope, risks, priorities, approach, and exclusions.
- [Test Cases](Part1-TestDesign/test-cases.md) - 10 structured test cases covering positive, negative, and edge-case scenarios.
- [Exploratory Findings](Part1-TestDesign/exploratory-findings.md) - Issues and observations identified during exploratory testing.
- [API Checks](Part1-TestDesign/api-checks.md) - Manual Account API verification using Swagger and Postman.

All documents are written in Markdown and can be viewed directly on GitHub.

## Part 2 - Test Automation

### Technology Stack

- C#
- .NET 10
- Microsoft Playwright for .NET
- NUnit
- Visual Studio 2022
- DemoQA Account API

### Automated Test Coverage

Six UI scenarios were automated using Playwright and NUnit, along with an additional API test.

| Test | Description | Type |
|---|---|---|
| Valid Login | Verify successful login with valid credentials | UI - Positive |
| Invalid Login | Verify login rejection with invalid credentials | UI - Negative |
| TC-05 - Search Books | Verify search filtering and restoring results after clearing the search | UI - Positive |
| TC-06 - Add Book to Collection | Verify that a book can be added and persists after refresh | UI - Positive |
| TC-07 - Prevent Duplicate Addition | Verify that a book cannot be added twice to the same collection | UI - Negative |
| TC-08 - Delete Book and Add Again | Verify deletion, confirmation, removal, and successful re-addition | UI - Positive |
| Account API Test | Verify user creation, token generation, and deletion | API |

**Execution result: 7 tests executed, 7 passed, 0 failed.**

All tests were successfully executed together using Visual Studio 2022 Test Explorer.

### Automation Design

The project follows the Page Object Model (POM) pattern to separate UI interactions from test assertions.

- **Pages:** Reusable locators and actions for Login, Book Store, Book Details, and Profile.
- **Tests:** NUnit test classes containing test scenarios and assertions.
- **Api:** Account API client and temporary test-user management.
- **Setup:** Shared Playwright test configuration through BaseTest.
- **Configuration:** Application URL and credentials are read from test.runsettings.

Key implementation decisions:

- Use role-based and element-specific selectors instead of fragile positional selectors.
- Use Playwright's built-in automatic waiting and assertions instead of fixed delays.
- Handle JavaScript alerts and HTML confirmation modals appropriately.
- Create unique temporary users through the Account API for collection-related tests.
- Delete temporary users automatically after test execution.
- Keep assertions inside test classes and UI interactions inside Page Objects.

The tests interact with the public DemoQA environment, so external service availability and network conditions may affect execution.

### Prerequisites

Before running the automation project, install:

1. Visual Studio 2022 with .NET development support, or the .NET 10 SDK.
2. Google Chrome.
3. The project's NuGet dependencies.
4. Playwright browser binaries.

### Installation

Clone the repository and open `Bitdefender.QA.sln` in Visual Studio 2022.

Alternatively, open a terminal in the repository root and run:

```powershell
dotnet restore
dotnet build Bitdefender.QA.sln
```

Install Playwright browsers on Windows:

```powershell
powershell -ExecutionPolicy Bypass -File ".\Bitdefender.QA.Tests\bin\Debug\net10.0\playwright.ps1" install
```

### Test Configuration

The project uses `Bitdefender.QA.Tests/test.runsettings`.

The following parameters are used:

| Parameter | Description |
|---|---|
| AppUrl | Base URL of the DemoQA application |
| AdminUser | Username used for login testing |
| AdminPassword | Password used for login testing |

Use a dedicated test account and do not commit real credentials to the repository.

Collection-related tests use temporary users created through the Account API. These users are automatically deleted during cleanup and do not affect the existing login test account.

### Running Tests

#### Option 1 - Visual Studio 2022

1. Open `Bitdefender.QA.sln`.
2. Build the solution.
3. Go to **Test > Configure Run Settings** and select `test.runsettings`.
4. Open **Test Explorer**.
5. Click **Run All Tests** or run an individual test.

#### Option 2 - Command Line

From the repository root, execute:

```powershell
dotnet test Bitdefender.QA.sln --settings ".\Bitdefender.QA.Tests\test.runsettings"
```

Test results can be reviewed in Visual Studio Test Explorer or in the terminal output.

## Part 3 - Test Strategy for an AI-Powered Feature

The AI testing strategy describes how I would validate a security assistant that generates summaries and recommendations based on alert data.

The approach focuses on:

- Defining pass/fail criteria for non-deterministic responses.
- Building a repeatable, risk-based evaluation dataset.
- Detecting hallucinations and unsupported security claims.
- Combining automated validation with manual security reviews.
- Establishing release criteria based on security impact.

[Read the AI Test Strategy](ai-test-strategy.md)

## Part 4 - AI Usage Statement

- I used ChatGPT & Claude Code throughout the assignment for brainstorming, documentation, automation development, and troubleshooting.
- For manual testing, I provided the test ideas, scenarios, and exploratory findings, while AI helped refine, organize, and clearly document them.
- AI also suggested additional scenarios and improvements, which I reviewed and selected based on their relevance.
- For automation, I worked with AI to build the C# Playwright/NUnit project, including Page Objects, test classes, and reusable methods.
- I provided the application behavior, HTML elements, test steps, and expected results, while AI helped generate and improve the implementation.
- We discussed different approaches to make the tests more maintainable, reusable, and easier to understand.
- I reviewed and corrected generated code, including selectors, expected messages, and API cleanup behavior, based on actual test results.
- I manually investigated failures, provided feedback, and reran the tests to verify the corrections.
- AI was an active development assistant, while I remained responsible for the testing decisions, reviewing the output, and validating the results.