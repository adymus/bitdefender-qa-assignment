
# Test Plan – Book Store Application

**Application:** https://demoqa.com/books  
**Testing:** Functional, black-box  
**Browsers:** Edge

## 1. Objective and Scope

The goal is to verify that users can create an account, log in, search for books and manage their collection correctly. I will focus on account access and saved data first, since issues in these areas have the highest impact on users.

**What I will test:**

- **Accounts:** Valid registration, invalid password, successful and unsuccessful login, logout and Profile access after logout.
- **Book Store:** Search by title, partial title and author, book details and searches with no matching results.
- **Collections:** Add a book, prevent duplicate additions, delete a single book (Cancel/Confirm), add it again and delete all books.
- **Account deletion:** Cancel and confirm account deletion, then attempt to log in with the deleted account.
- **Persistence:** Verify that collection changes remain saved after refreshing the Profile page.

## 2. Risks and Priorities

| Priority | Risk | Why It Matters |
|---|---|---|
| **P1** | Account creation or login fails; logout leaves access | Users may be unable to access their accounts or may still be able to view private information after logging out. |
| **P1** | Incorrect collection changes | Books may be duplicated, removed incorrectly or lost after refreshing the page. |
| **P1** | Deletion leaves an incorrect state | Users may not know whether their books or account were successfully deleted. |
| **P2** | Search misses existing books | Users may have difficulty finding books, although saved collection data is not directly affected. |
| **P2** | Unclear password validation feedback | Users may not understand why registration failed or how to correct their password. |
| **P3** | Minor wording or layout issues | These usually do not prevent users from completing the main flows. |

I will prioritize P1 scenarios because account access, collection management and deletion have the highest impact. P2 scenarios will follow, while lower-impact issues will be recorded if found during testing.

## 3. Test Approach

I started with exploratory testing to understand the application and check how it behaves in different situations. This included searching using typed and pasted values, cancelling deletion actions and clicking confirmation buttons repeatedly.

Based on this exploration, I selected **10 test cases** covering positive, negative and edge-case scenarios. Each test includes the required login and data preparation steps, so it can be executed without depending on another test case.

I use the existing `adymus` account for regular functional tests and create a separate account when testing registration or account deletion. Tests that modify the same collection will be executed sequentially to avoid affecting each other.

I also check **Console** and **Network** for errors related to the tested actions and refresh the Profile page to verify that changes are saved correctly.

For additional investigation, I used **Swagger and Postman** to check selected Account API endpoints, including user creation, authorization, token generation, retrieving a user and account deletion. These results are documented separately in `api-checks.md`.

## 4. Deliberately Out of Scope

| Not Covered in Detail | Reason |
|---|---|
| Full visual and UI review | Functional correctness is the main focus. Spacing, colors and other visual details can be reviewed separately. |
| Every book cover and external link | These have lower impact than account access and collection management. Incorrect covers found during exploration are still recorded. |
| Load and performance testing | This would require agreed performance targets and a controlled test environment. |
| New features and enhancements | The focus is on validating existing functionality. New features and usability improvements would need separate requirements and prioritization. |
| Full cross-browser and responsive testing | Chrome is the planned functional baseline. Additional browsers, devices and screen sizes would require a wider testing cycle. |

## 5. Reporting

Each test case will be marked as **Passed**, **Failed** or **Blocked**. For failed cases, I will record the actual result and relevant evidence, including Console or Network errors where applicable.

Additional issues found during exploratory testing are documented in [Exploratory Findings](exploratory-findings.md).

Manual Account API checks performed using Swagger and Postman are documented in [API Checks](api-checks.md).

These observations are separate from the 10 planned test cases.