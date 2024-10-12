# SimpleTrader - Fullstack WPF MVVM Demo Application

This project is a **Simple Trading Demo Application** built with WPF, MVVM architecture, and .NET. It features CRUD operations, live data visualization, and portfolio management using various technologies and patterns such as Entity Framework, Repository Pattern, Dependency Injection, and more. The application allows users to search for assets, buy and sell stocks, view portfolios, and manage accounts with login and registration functionalities.

## Features

- **CRUD Operations**: Full support for Create, Read, Update, Delete operations for managing user portfolios and stock transactions.
- **Entity Framework**: Used for database interaction and management of data models.
- **Repository Pattern**: Clean separation between business logic and data access with the repository pattern.
- **Live Data Visualization**: Integrated **LiveCharts** library to visualize stock data and portfolio performance through dynamic charts.
- **Stock Trading**: Search assets, buy and sell stocks, and maintain a portfolio of owned assets.
- **Dependency Injection**: Used throughout the WPF application via **HostBuilder**, ensuring clean architecture and testability.
- **API Integration**: Uses **HttpClient** to interact with third-party APIs for retrieving stock prices and other relevant data.
- **Domain Layer**: Separated domain layer with business logic and core services. This layer is fully unit-tested for reliability.
- **Asynchronous Commands**: Support for both synchronous and asynchronous commands to improve responsiveness and performance.
- **State Management**: Utilizes custom state classes for managing:
  - **Account State**: Handles user login, registration, and authentication.
  - **Asset State**: Manages the current portfolio and asset ownership.
  - **Navigation State**: Controls navigation within the application using ViewModels.
- **Custom UI Controls**: Implements reusable WPF UI controls to enhance the user experience.
- **Workflow for Release**: The project includes a GitHub Actions workflow that automates the build and publishing of a new release when a push with a tag command is executed.

## Technology Stack

- **.NET** (WPF, MVVM)
- **Entity Framework** (EF Core)
- **LiveCharts** (Data visualization)
- **HttpClient** (API communication)
- **Dependency Injection** via HostBuilder
- **Repository Pattern**
- **Unit Testing** with separated Domain layer
- **Async and Sync Commands** for improved app performance
- **State Management** for authentication, account, and navigation

## Usage
After launching the application, users can:

- **View their portfolio**: Filter by criteria such as asset type and value.
- **Search for assets**: Look up stock symbols and view real-time data.
- **Buy/Sell assets**: Execute trades and update your portfolio.
- **Login/Register**: Manage account information securely.

## Getting Started

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/jv813yh/SimpleTrader.git
2. Navigate to the project directory:
   ```bash
   cd SimpleTrader
3. Restore dependencies:
   ```bash
   dotner restore
5. Update EF migrations and apply the database:
   ```bash
   dotnet ef database update
6. Run the application:
   ```bash
   dotnet run --project SimpleTrader.WPF/SimpleTrader.WPF.csproj

### Running Tests
The project includes unit tests for the Domain layer. To run the tests:
  ```bash
  dotnet test

