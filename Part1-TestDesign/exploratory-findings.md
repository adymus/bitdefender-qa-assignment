# Exploratory Testing Findings – Book Store Application

**Application:** https://demoqa.com/books  
**Browser:** Microsoft Edge  
**Testing Type:** Exploratory Testing

During exploratory testing, I found several issues related to registration, searching for books, and collection management.

The findings below include the steps to reproduce the behavior, the expected result, and what I observed. These are additional observations, separate from the 10 planned test cases.

## Findings

| ID | Scenario / Steps to Reproduce | Expected Result | Actual Result |
|---|---|---|---|
| **EXP-01** | Open Registration and enter a password that does not meet the requirements. Check the red validation message. | The password requirements should remain visible long enough for the user to read them and correct the password. | The red validation message disappears after a very short time, making it difficult to read. |
| **EXP-02** | Open Book Store, paste `Git Pocket Guide` into the Search field, wait for the results, and click the Search button. Compare with manually typing `git`. | Both search methods should return the existing book. | Pasting the full title returns no results, even after clicking Search. Manually typing `git` returns the correct book. |
| **EXP-03** | Search for `spea` in Book Store and check the image displayed for Speaking JavaScript. Clear the Search field afterward. | Speaking JavaScript should display its correct book cover. | The search result displays the cover used for Git Pocket Guide. After clearing Search, the original book covers are displayed correctly again. |
| **EXP-04** | Add Git Pocket Guide to the collection. Open the Delete Book confirmation modal and click **OK** repeatedly and quickly. | The deletion should be processed only once, without additional errors or duplicate requests. | The book is deleted, but multiple DELETE requests return **400 Bad Request**. An additional alert displays `ISBN supplied is not available in User's Collection!`. The error alert remains visible briefly even after clicking OK. |
| **EXP-05** | Add at least two books to the collection. Open Profile, click **Delete All Books**, and confirm. | All books should be removed and the collection should update immediately without errors. | The books remain visible after deletion, and Console displays `Unexpected end of JSON input`. After refreshing the page, the collection is empty. Attempting to delete a book still displayed before refresh produces an error. |
| **EXP-06** | Log in with a dedicated test account. Click **Delete Account** and confirm with **OK**. Click OK again if the confirmation modal remains open. | The account should be deleted after the first confirmation, the modal should close, and the user should no longer be authenticated. | After the first confirmation, the modal remains open and Console displays `Unexpected end of JSON input`. Clicking OK again displays `User Id not correct!`. After refreshing the page, the account appears to be deleted, but the Profile page shows a user-not-found state. |

## Test Execution Notes

- The findings above were observed during exploratory testing and can be retested as part of regression testing.
- Console and Network were checked for errors related to the tested functionality.
- Relevant screenshots and error details can be attached as evidence when reproducing the issues.
- The findings are kept separate from the planned test cases, where expected results describe the correct application behavior.