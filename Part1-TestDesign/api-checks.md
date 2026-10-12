
# Account API – Exploratory Checks (Swagger and Postman)

**Application:** https://demoqa.com/books  
**API Documentation:** https://demoqa.com/swagger/  
**Tools:** Swagger UI, Postman  
**Testing Type:** Manual / Exploratory Testing

I tested several Account API operations using Swagger and Postman, focusing on user creation, authentication, and account management. These checks are separate from the 10 planned test cases.

## 1. API Checks and Results

| Check | Action | Result |
|---|---|---|
| **Create user – Invalid password** | Sent `POST /Account/v1/User` with a new username and an invalid password (`test1@`). | **400 Bad Request**. The API returned a message explaining the password requirements. |
| **Create user – Valid password** | Repeated `POST /Account/v1/User` with a valid password. | **201 Created**. The response included the new `userID`, username, and an empty `books` array. |
| **Login with API-created account** | Logged in to the Book Store application using the newly created account. | Login was successful and the user's Profile page was displayed. |
| **Check authorization** | Sent `POST /Account/v1/Authorized` using the account credentials. | **200 OK**, response `true`. |
| **Swagger Basic Authorization** | Entered the account credentials using Swagger's **Authorize** option. | Basic authorization was configured successfully in Swagger. |
| **Generate Token** | Sent `POST /Account/v1/GenerateToken` using the account credentials. | **200 OK**. The response included a Bearer token and an expiration value. |
| **Swagger Bearer Authorization** | Entered the generated token in Swagger's Bearer authorization field. | Swagger displayed Bearer authorization as configured. |
| **GET User – Swagger** | Entered the UserID in `GET /Account/v1/User/{UUID}` and executed the request. Also tried a UserID obtained from DevTools for a UI-created account. | **401**, with code `1207` and message `User not found!`. The generated Request URL still contained `{UUID}`. |
| **GET User – Postman** | Sent GET using the actual UUID in the request URL with Bearer authorization. | **200 OK**. The response returned the correct user details and books from the collection. |
| **DELETE User – Swagger** | Entered the UserID in `DELETE /Account/v1/User/{UUID}` and executed the request. | HTTP **200**, but the response body contained code `1207` and message `User Id not correct!`. The generated Request URL still contained `{UUID}`. |
| **DELETE User – Postman** | Sent DELETE using the actual UUID in the request URL with the account's authorization configured. | **204 No Content**, indicating successful deletion. |

## 2. Authentication and Token Handling

For the authenticated requests in Postman, I generated a Bearer token using `POST /Account/v1/GenerateToken` and added it in the Authorization tab.

The response included an expiration value, so a new token will be needed when the current one expires.

GET User returned **200 OK** using Bearer authorization. DELETE User also worked with the authorization configured for the test account.

## 3. GET and DELETE – Investigation Results

Both GET and DELETE returned errors when tested through Swagger.

Although I entered a valid UserID, the generated Request URL still contained `{UUID}` instead of the actual value.

I tested the same endpoints in Postman, using the correct UUID directly in the URL:

- **GET User:** Returned 200 OK with the user's details and books.
- **DELETE User:** Returned 204 No Content.

Both operations worked in Postman. Based on these results, the issue appears to be related to how Swagger handles the UUID parameter rather than the API endpoints themselves. The exact cause has not been confirmed.

## 4. Test Data Setup for Automation

Since user creation and deletion worked through the API, I plan to use these endpoints for test data setup and cleanup in Part 2.

The approach is to create a new test account, use it during the UI tests, and delete it afterward. This will help keep the tests independent and avoid relying on the same account for every execution.

Authenticated requests will use a valid Bearer token generated for the test user when needed.

I will also verify that a deleted account can no longer log in and make sure cleanup is executed even when a test fails.