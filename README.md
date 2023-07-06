# KingUsers

Welcome to KingUsers! This project aims to demonstrate a simple .NET 6 API Backend and Blazor WASM Frontend. 
This project is based on a simple development test that was assigned. 
Built with the quickest basic implementation.

.NET6, Blazor, SqlLiteDb

## Table of Contents

- [Installation](#installation)
- [Development Tasks](#development-tasks)
- [ToDoList](#todo)
- [Screenshots](#screenshots)

## Installation

To install KingUsers, follow these steps:

1. Clone the repository.
2. Run the VS Solution: `/KingUsers.sln`.
3. Make sure you have both `KingUsers` API and `KingUsersApp` Blazor APP set as the startup projects.
4. Build and run.

## Development Tasks

In the following tasks, we emphasize maintainability in code structure, good database design, performance, and scalability. When completed, please submit the code and database structure via GitHub.

### Task 1

Create a database using code-first (SQL database).

- [x] A user should be able to appear in multiple groups, and conversely, there should be multiple users in a group.
- [x] Each group should have multiple permissions (e.g., Level 1, Level 2, etc.) and group names.

### Task 2

Create a simple web service that supports user management (Add, Remove, Update).

- [x] Add endpoints for user count and the number of users per group count.
- [x] Use the .NET framework or .NET Core.

### Task 3

Create a web interface where users can be added, edited, and deleted.

- [x] The visual aspect is not important.
- [x] Use the Web API created in Task 2.
- [x] Use the .NET framework or .NET Core.

**Extra Points:**

- [x] Add unit tests and integration tests.
- [x] Ensure that the project can be pulled from GitHub and deployed to a local machine.

## Todo List
- [ ] Change the architecture to Clean architecture using Union
- [ ] Add caching
- [ ] Add MutlTendancy
- [ ] Add user Roles
- [ ] Add Logging (Response and Request Middleware
- [ ] Add Security (Login and Roles)
- [ ] Add Localization
- [ ] Add Multi Data provides (Mysql, MariaDB, MsSQL


## License

KingUsers is licensed under the [MIT License](LICENSE).

## Screenshots

Here are a few screenshots of the KingUsers application:

![Screenshot 1](screenshots/screenshot1.png)
![Screenshot 2](screenshots/screenshot2.png)
