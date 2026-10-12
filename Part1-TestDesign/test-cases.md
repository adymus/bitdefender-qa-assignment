# Test Cases – Book Store Application

**Application:** https://demoqa.com/books  
**Testing Type:** Functional / Black-box Testing  
**Browsers:** Microsoft Edge (exploratory testing), Chrome (planned execution)

## Test Execution Notes

- Each test case can be executed independently.
- The existing test account is used for the main functional scenarios.
- New accounts are created only for Registration and Delete Account testing.
- Prepare the required books before executing collection-related tests.
- Keep Developer Tools (Console and Network) open to check for errors related to the tested functionality.
- Record test results as Passed, Failed or Blocked, with evidence for failures.

**Existing Test Account:**
- Username: `adymus`
- Password: `Andromeda12@`

## Test Case Overview

| ID | Test Case | Type | Priority |
|---|---|---|---|
| TC-01 | Register a new user with valid data and log in | Positive | P1 |
| TC-02 | Register with an invalid password | Negative | P2 |
| TC-03 | Login with invalid credentials | Negative | P1 |
| TC-04 | Logout and verify Profile access | Negative / Session | P1 |
| TC-05 | Search for books using different criteria | Positive / Negative | P2 |
| TC-06 | Add Speaking JavaScript to the collection | Positive | P1 |
| TC-07 | Prevent adding the same book twice | Negative / Edge | P1 |
| TC-08 | Delete one book and add it again | Positive | P1 |
| TC-09 | Delete all books from the collection | Positive / Regression | P1 |
| TC-10 | Delete a user account | Positive / Destructive | P1 |

---

## TC-01 – Register a new user with valid data and log in

**Priority:** P1 – High  
**Type:** Positive

**Preconditions:**
- The user is logged out.
- The selected username does not already exist.

**Test Data:**
- First Name: `Test`
- Last Name: `User`
- Username: `qa_register_<unique-number>`
- Password: `TestQA2026@`

| Step | Action | Expected Result |
|---|---|---|
| 1 | Navigate to https://demoqa.com/login. | The Login page is displayed. |
| 2 | Click **New User**. | The Registration page is displayed. |
| 3 | Enter the First Name, Last Name, UserName and Password from Test Data. | All fields accept the entered values. |
| 4 | Click **Register**. | A browser alert displays "User Registered Successfully." |
| 5 | Click **OK** to close the alert. | The alert closes. |
| 6 | Click **Back to Login**. | The Login page is displayed. |
| 7 | Enter the newly created username and password. | Both fields accept the credentials. |
| 8 | Click **Login**. | The user is redirected to Profile and the correct username is displayed. |

---

## TC-02 – Register with an invalid password

**Priority:** P2 – Medium  
**Type:** Negative

**Preconditions:**
- The user is logged out.
- A unique username is available.

**Test Data:**
- First Name: `Test`
- Last Name: `User`
- Username: `qa_invalid_<unique-number>`
- Invalid Password: `password123`

| Step | Action | Expected Result |
|---|---|---|
| 1 | Navigate to https://demoqa.com/login. | The Login page is displayed. |
| 2 | Click **New User**. | The Registration page is displayed. |
| 3 | Enter the First Name, Last Name and UserName from Test Data. | All fields accept the entered values. |
| 4 | Enter `password123` in the Password field. | The password is entered. |
| 5 | Click **Register**. | Registration is rejected because the password does not meet the requirements. |
| 6 | Verify the password validation message. | A readable red validation message explains the password requirements. |
| 7 | Verify the registration result. | The user remains on the Registration page and no account is created. |

**Related Finding:** EXP-01 – Password validation message disappears too quickly.

---

## TC-03 – Login with invalid credentials

**Priority:** P1 – High  
**Type:** Negative

**Preconditions:**
- The `adymus` test account exists.
- The user is logged out.

**Test Data:**
- Username: `adymus`
- Invalid Password: `wrongPassword123!`

| Step | Action | Expected Result |
|---|---|---|
| 1 | Navigate to https://demoqa.com/login. | The Login page is displayed. |
| 2 | Enter `adymus` in the UserName field. | The username is entered. |
| 3 | Enter the invalid password from Test Data. | The password is entered. |
| 4 | Click **Login**. | Login is rejected and "Invalid username or password!" is displayed. |
| 5 | Verify the current page. | The user remains on the Login page and is not authenticated. |
| 6 | Navigate directly to https://demoqa.com/profile. | No private account or collection data is displayed. |
| 7 | Verify the Profile message. | The page displays "Currently you are not logged into the Book Store application, please visit the login page to enter or register page to register yourself". |

---

## TC-04 – Logout and verify Profile access

**Priority:** P1 – High  
**Type:** Negative / Session

**Preconditions:**
- The `adymus` test account exists.
- The user is logged out.

**Test Data:**
- Username: `adymus`
- Password: `Andromeda12@`
- Book: `Speaking JavaScript` (if the collection is empty)

| Step | Action | Expected Result |
|---|---|---|
| 1 | Navigate to https://demoqa.com/login. | The Login page is displayed. |
| 2 | Enter the username and password from Test Data. | Both fields accept the credentials. |
| 3 | Click **Login**. | The Profile page opens and displays `adymus`. |
| 4 | Check whether the collection contains at least one book. | If a book is already present, continue at step 8. Otherwise, follow steps 5–7. |
| 5 | If the collection is empty, click **Go To Book Store** and select **Speaking JavaScript**. | The Book Details page is displayed. |
| 6 | Click **Add To Your Collection** and accept the confirmation alert. | Speaking JavaScript is added successfully. |
| 7 | Return to Profile. | Speaking JavaScript is visible in the collection. |
| 8 | Click **Logout**. | The user is logged out and redirected to the Login page. |
| 9 | Click the browser **Back** button. | The browser returns to Profile, but the user's private information is no longer displayed. The not-logged-in message appears. |
| 10 | Navigate directly to https://demoqa.com/profile. | The same message is displayed and the user's collection is not accessible. |

**Expected Profile message:**

"Currently you are not logged into the Book Store application, please visit the login page to enter or register page to register yourself"

---

## TC-05 – Search for books using different criteria

**Priority:** P2 – Medium  
**Type:** Positive / Negative

**Preconditions:**
- The `adymus` test account exists.
- The user is logged out.

**Test Data:**
- Username: `adymus`
- Password: `Andromeda12@`
- Search Values: `git`, `Git Pocket Guide`, `6`, `eric`, `no`, `spea`, `NonExistingBook12345`

| Step | Action | Expected Result |
|---|---|---|
| 1 | Navigate to https://demoqa.com/login. | The Login page is displayed. |
| 2 | Enter the username and password from Test Data. | Both fields accept the credentials. |
| 3 | Click **Login**. | The Profile page displays the correct username. |
| 4 | Click **Go To Book Store**. | The Book Store page and Search field are displayed. |
| 5 | Type `git` manually, one character at a time. | Git Pocket Guide is displayed in the results. |
| 6 | Clear Search, paste `Git Pocket Guide`, wait for the results and click the Search button. | Git Pocket Guide is displayed as the matching result. |
| 7 | Clear Search and enter `6`. | Understanding ECMAScript 6 is displayed. |
| 8 | Clear Search and enter `eric`. | Programming JavaScript Applications, by Eric Elliott, is displayed. |
| 9 | Clear Search and enter `no`. | Matching books are displayed, including You Don't Know JS, which contains `no` in `Know`. |
| 10 | Clear Search and enter `spea`. | Speaking JavaScript is displayed with the correct book information and cover image. |
| 11 | Clear Search and enter `NonExistingBook12345`. | No matching books are displayed and the page remains usable. |
| 12 | Clear the Search field. | The complete book list is displayed again with the correct book details and cover images. |

**Related Findings:**
- EXP-02 – Search using copy/paste returns no results.
- EXP-03 – Incorrect book cover displayed after filtering.

---

## TC-06 – Add Speaking JavaScript to the collection

**Priority:** P1 – High  
**Type:** Positive

**Preconditions:**
- The `adymus` test account exists.
- The user is logged out.

**Test Data:**
- Username: `adymus`
- Password: `Andromeda12@`
- Book: `Speaking JavaScript`
- ISBN: `9781449365035`

| Step | Action | Expected Result |
|---|---|---|
| 1 | Navigate to https://demoqa.com/login. | The Login page is displayed. |
| 2 | Enter the username and password from Test Data. | Both fields accept the credentials. |
| 3 | Click **Login**. | The Profile page is displayed with the correct username. |
| 4 | Check whether Speaking JavaScript is already in the collection. | If the book is present, remove it using steps 5–7. Otherwise, continue at step 8. |
| 5 | If present, click the Delete icon next to Speaking JavaScript. | The Delete Book confirmation modal is displayed. |
| 6 | Click **OK** in the modal, then **OK** on the "Book deleted." alert. | Speaking JavaScript is removed from the collection. |
| 7 | Refresh the Profile page. | Speaking JavaScript is no longer displayed. Other books remain unchanged. |
| 8 | Click **Go To Book Store**. | The Book Store page is displayed. |
| 9 | Select Speaking JavaScript. | The Book Details page displays the correct title and ISBN. |
| 10 | Click **Add To Your Collection**. | A confirmation alert indicates the book was added successfully. |
| 11 | Click **OK** to close the alert. | The alert closes. |
| 12 | Navigate to Profile. | Speaking JavaScript is displayed once in the collection. |
| 13 | Refresh the Profile page. | Speaking JavaScript remains in the collection. |

---

## TC-07 – Prevent adding the same book twice

**Priority:** P1 – High  
**Type:** Negative / Edge Case

**Preconditions:**
- The `adymus` test account exists.
- The user is logged out.

**Test Data:**
- Username: `adymus`
- Password: `Andromeda12@`
- Book: `Speaking JavaScript`

| Step | Action | Expected Result |
|---|---|---|
| 1 | Navigate to https://demoqa.com/login. | The Login page is displayed. |
| 2 | Enter the username and password from Test Data. | Both fields accept the credentials. |
| 3 | Click **Login**. | The Profile page is displayed. |
| 4 | Check whether Speaking JavaScript is already in the collection. | If present, remove it using steps 5–7. Otherwise, continue at step 8. |
| 5 | If present, click its Delete icon. | The Delete Book confirmation modal is displayed. |
| 6 | Click **OK** in the modal, then **OK** on the "Book deleted." alert. | Speaking JavaScript is removed. |
| 7 | Refresh the Profile page. | Speaking JavaScript is no longer displayed. |
| 8 | Click **Go To Book Store** and select Speaking JavaScript. | The Book Details page is displayed. |
| 9 | Click **Add To Your Collection**. | A confirmation alert indicates the book was added successfully. |
| 10 | Click **OK** to close the alert. | The alert closes and the Book Details page is available. |
| 11 | Click **Add To Your Collection** again. | The application rejects the duplicate and displays "Book already present in the your collection!". |
| 12 | Close the alert and navigate to Profile. | Speaking JavaScript is displayed only once. |
| 13 | Refresh the Profile page. | The collection still contains only one instance of Speaking JavaScript. |

---

## TC-08 – Delete one book and add it again

**Priority:** P1 – High  
**Type:** Positive / Functional

**Preconditions:**
- The `adymus` test account exists.
- The user is logged out.

**Test Data:**
- Username: `adymus`
- Password: `Andromeda12@`
- Book to Delete: `Git Pocket Guide`
- Book to Keep: `Speaking JavaScript`

| Step | Action | Expected Result |
|---|---|---|
| 1 | Navigate to https://demoqa.com/login. | The Login page is displayed. |
| 2 | Enter the username and password from Test Data. | Both fields accept the credentials. |
| 3 | Click **Login**. | The Profile page displays `adymus`. |
| 4 | Check whether Git Pocket Guide or Speaking JavaScript is already in the collection. | If either book is present, prepare the collection using steps 5–7. Otherwise, continue at step 8. |
| 5 | For each book already present, click its Delete icon. | The Delete Book confirmation modal is displayed for the selected book. |
| 6 | Click **OK** in the modal, then **OK** on the "Book deleted." alert. | The selected book is removed. |
| 7 | Refresh Profile and repeat steps 5–6 for the other test book if needed. | Neither test book is in the collection. Unrelated books remain unchanged. |
| 8 | Click **Go To Book Store** and select Git Pocket Guide. | The Book Details page for Git Pocket Guide is displayed. |
| 9 | Click **Add To Your Collection**. | A successful addition alert is displayed. |
| 10 | Click **OK** to close the alert, then **Back To Book Store**. | The Book Store page is displayed. |
| 11 | Select Speaking JavaScript. | The Book Details page for Speaking JavaScript is displayed. |
| 12 | Click **Add To Your Collection**. | A successful addition alert is displayed. |
| 13 | Click **OK** and navigate to Profile. | Both books are displayed in the collection. |
| 14 | Click the Delete icon next to Git Pocket Guide. | The Delete Book confirmation modal displays "Do you want to delete this book?". |
| 15 | Click **Cancel**. | The modal closes and both books remain in the collection. No related errors appear in Console or Network. |
| 16 | Click the Delete icon next to Git Pocket Guide again. | The confirmation modal is displayed again. |
| 17 | Click **OK** once to confirm deletion. | A browser alert displays "Book deleted.". |
| 18 | Click **OK** to close the alert. | Git Pocket Guide disappears from the collection, while Speaking JavaScript remains. No related Console or Network errors appear. |
| 19 | Refresh the Profile page. | Git Pocket Guide is still absent and Speaking JavaScript remains. |
| 20 | Click **Go To Book Store** and select Git Pocket Guide. | The Book Details page is displayed. |
| 21 | Click **Add To Your Collection**. | A confirmation alert indicates the book was added successfully. |
| 22 | Click **OK** and navigate to Profile. | Both books are displayed in the collection, each only once. |
| 23 | Refresh the Profile page. | Both books remain saved in the collection. |

---

## TC-09 – Delete all books from the collection

**Priority:** P1 – High  
**Type:** Positive / Regression

**Preconditions:**
- The `adymus` test account exists.
- The user is logged out.
- The account is dedicated to testing, and its collection can be cleared.

**Test Data:**
- Username: `adymus`
- Password: `Andromeda12@`
- Book 1: `Git Pocket Guide`
- Book 2: `Speaking JavaScript`

| Step | Action | Expected Result |
|---|---|---|
| 1 | Navigate to https://demoqa.com/login. | The Login page is displayed. |
| 2 | Enter the username and password from Test Data. | Both fields accept the credentials. |
| 3 | Click **Login**. | The Profile page displays `adymus`. |
| 4 | Check the current collection. If it contains books, click **Delete All Books** and confirm deletion to prepare an empty collection. | The collection is empty before adding the test books. |
| 5 | If the list still shows books, refresh Profile. | The collection displays no books. |
| 6 | Click **Go To Book Store** and select Git Pocket Guide. | The Book Details page is displayed. |
| 7 | Click **Add To Your Collection**. | A successful addition alert is displayed. |
| 8 | Click **OK**, then **Back To Book Store**. | The Book Store page is displayed. |
| 9 | Select Speaking JavaScript. | The Book Details page for Speaking JavaScript is displayed. |
| 10 | Click **Add To Your Collection**. | A successful addition alert is displayed. |
| 11 | Click **OK** and navigate to Profile. | Git Pocket Guide and Speaking JavaScript are both displayed in the collection. |
| 12 | Click **Delete All Books**. | The confirmation dialog is displayed. |
| 13 | Click **Cancel**. | The dialog closes and both books remain in the collection. |
| 14 | Click **Delete All Books** again. | The confirmation dialog is displayed again. |
| 15 | Click **OK** once to confirm deletion. | All books are removed immediately and the collection updates without errors. |
| 16 | Check Console and Network. | No JavaScript errors or failed requests related to the deletion are present. |
| 17 | Refresh the Profile page. | The collection remains empty. |

**Related Finding:** EXP-05 – Delete All Books does not refresh the UI correctly.

---

## TC-10 – Delete a user account

**Priority:** P1 – High  
**Type:** Positive / Functional

**Preconditions:**
- The user is logged out.
- A unique username is available.
- The main test account `adymus` must not be deleted.

**Test Data:**
- First Name: `Delete`
- Last Name: `Test`
- Username: `qa_delete_<unique-number>`
- Password: `DeleteQA2026@`

| Step | Action | Expected Result |
|---|---|---|
| 1 | Navigate to https://demoqa.com/login. | The Login page is displayed. |
| 2 | Click **New User**. | The Registration page is displayed. |
| 3 | Enter the First Name, Last Name, UserName and Password from Test Data. | All fields accept the entered values. |
| 4 | Click **Register**. | A browser alert displays "User Registered Successfully." |
| 5 | Click **OK** to close the alert. | The alert closes. |
| 6 | Click **Back to Login**. | The Login page is displayed. |
| 7 | Enter the newly created username and password. | Both fields accept the credentials. |
| 8 | Click **Login**. | The user is redirected to Profile and the correct username is displayed. |
| 9 | Click **Delete Account**. | A confirmation modal appears with the message "Do you want to delete your account?". |
| 10 | Click **Cancel**. | The modal closes and the user remains on Profile. The account is not deleted. |
| 11 | Verify the Profile page. | The username is still displayed and no related errors appear in Console or Network. |
| 12 | Click **Delete Account** again. | The confirmation modal is displayed again. |
| 13 | Click **OK** once to confirm deletion. | The account is deleted, the modal closes and the user is logged out. No related errors appear in Console or Network. |
| 14 | Navigate to https://demoqa.com/login. | The Login page is displayed. |
| 15 | Enter the deleted account's username and password. | Both fields accept the credentials. |
| 16 | Click **Login**. | Login is rejected with "Invalid username or password!". |
| 17 | Navigate directly to https://demoqa.com/profile. | No private information for the deleted account is accessible. |

**Related Finding:** EXP-06 – Delete Account leaves the confirmation modal open and displays errors.

---

## Related Exploratory Testing

Additional issues observed during exploratory testing are documented separately in [Exploratory Findings](exploratory-findings.md).

Account API checks performed using Swagger and Postman are documented in [API Checks](api-checks.md).