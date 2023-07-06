# King-Users

[![Build+Deploy](https://github.com/jasontaylordev/RapidBlazor/actions/workflows/workflow.yml/badge.svg)](https://github.com/jasontaylordev/RapidBlazor/actions/workflows/workflow.yml)
[![Nuget](https://img.shields.io/nuget/v/JasonTaylorDev.RapidBlazor?label=NuGet&)](https://www.nuget.org/packages/JasonTaylorDev.RapidBlazor)
[![Nuget](https://img.shields.io/nuget/dt/JasonTaylorDev.RapidBlazor?label=Downloads&)](https://www.nuget.org/packages/JasonTaylorDev.RapidBlazor)

Welcome to KingUsers! This project aims demostrate good clean Union archtecture, using Blazor WASM.

## Table of Contents

- [Installation](#installation)
- [Development Tasks](#development-tasks)
- [License](#license)

## Installation

To install KingUsers, follow these steps:

1. Clone the repository.
2. Run the setup script: `./setup.sh`.
3. Configure the database connection in `config.yml`.
4. Start the application: `npm start`.

## Development Tasks

In the following tasks, we emphasize maintainability in code structure, good database design, performance, and scalability. When completed, please submit the code and database structure via GitHub.

### Task 1

Create a database using code first (SQL database).

- A user should be able to appear in multiple groups, and conversely, there should be multiple users in a group.
- Each group should have multiple permissions (e.g., Level 1, Level 2, etc.) and group names.

### Task 2

Create a simple web service that supports user management (Add, Remove, Update).

- Add endpoints for user count and the number of users per group count.
- Use the .NET framework or .NET Core.

### Task 3

Create a web interface where users can be added, edited, and deleted.

- The visual aspect is not important.
- Use the Web API created in Task 2.
- Use the .NET framework or .NET Core.

**Extra Points:**

- Add unit tests and integration tests.
- Ensure that the project can be pulled from GitHub and deployed to a local machine.

## License

KingUsers is licensed under the [MIT License](LICENSE).

## Screenshots

Here are a few screenshots of the KingUsers application:

![Screenshot 1](screenshots/screenshot1.png)
![Screenshot 2](screenshots/screenshot2.png)
