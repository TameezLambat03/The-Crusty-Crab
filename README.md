# 🦀 The Crusty Crab – MVC Frontend
### ASP.NET Core Restaurant Ordering & Management System

The Crusty Crab MVC Frontend is the customer-facing and restaurant-management web application developed as part of The Crusty Crab restaurant ordering platform.

Built using ASP.NET Core MVC and .NET 10, the application provides an interface for customers to browse menus, manage shopping carts, place orders, and interact with restaurant services. It also provides management interfaces for staff and administrators.

The MVC application communicates with the CrustyAPI backend through a central HTTP API client. The backend must be running for API-dependent functionality to work.

**Current development environment:** Local development. Previous Azure deployments are no longer active.

# YouTube Video Demo

Watch the Crusty Crab application demonstration:

[![Watch the demonstration](https://img.youtube.com/vi/wARdxpGVP3A/0.jpg)](https://youtu.be/wARdxpGVP3A)

---

## 📋 Features

### Customer Features

- Browse available menu items.
- View menu item descriptions, prices, and images.
- Add menu items to a shopping cart.
- Adjust shopping cart quantities.
- Select spice levels and available add-ons.
- Choose between collection and delivery.
- Manage delivery addresses.
- Manage saved payment methods.
- Place restaurant orders.
- View order information.
- Receive order and payment confirmation.
- Interact with CrabbyBot for restaurant FAQs.

### 🎡 Spin the Wheel

The MVC application includes a post-order Spin the Wheel feature.

After successfully placing an order, eligible customers can access a spinning wheel through the order confirmation page.

The feature includes:

- A wheel interface displayed after order placement.
- Eligibility handling for the spinning feature.
- JavaScript-driven wheel animation.
- CSS transformations for the spinning effect.
- Backend API integration for the associated spin functionality.

The wheel animation is handled on the frontend, while the backend API supports the feature's server-side operations.

### Staff and Administration

- Access the management portal.
- Create, edit, and remove menu items.
- Upload menu item images.
- Toggle menu item availability.
- Manage restaurant staff.
- Manage administrators.
- View and update orders.
- Manage payment methods.
- Update payment status.

---

## 🏗️ Architecture

The MVC application follows the Model-View-Controller architectural pattern.

Its primary responsibility is presenting the application's functionality through web interfaces and communicating with the backend API.

```text
       ┌──────────────────────────┐
       │      Customer / Staff    │
       │         Browser          │
       └─────────────┬────────────┘
                     │
                     ▼
       ┌──────────────────────────┐
       │    ASP.NET Core MVC      │
       │                          │
       │ Controllers              │
       │ Models                   │
       │ Razor Views              │
       │ JavaScript / CSS         │
       └─────────────┬────────────┘
                     │
                     ▼
       ┌──────────────────────────┐
       │      CrustyApiClient     │
       │       HTTP Client        │
       └─────────────┬────────────┘
                     │
                     │ REST API
                     ▼
       ┌──────────────────────────┐
       │        CrustyAPI         │
       │   ASP.NET Core Web API   │
       └──────────────────────────┘
```

### MVC Responsibilities

| Component | Responsibility |
|---|---|
| Models | Represent application data and view models |
| Views | Render the user interface using Razor |
| Controllers | Handle incoming requests and application workflows |
| Services | Handle communication with external services, including CrustyAPI |
| wwwroot | Contains frontend CSS, JavaScript, images and other static assets |

The MVC application does not directly communicate with MySQL. Database operations are handled by the backend API.

---

## 💻 Technologies

### Backend Integration

- ASP.NET Core Web API
- REST API communication
- HTTP Client
- JSON data exchange

### Frontend

- ASP.NET Core MVC
- .NET 10
- Razor Views
- HTML5
- CSS3
- JavaScript
- Bootstrap and custom styling

### Development Tools

- Visual Studio
- Visual Studio Code
- Git and GitHub
- .NET SDK
- Postman for API testing, where applicable

---

## 📁 Project Structure

The MVC application follows a structured ASP.NET Core MVC project layout.

```text
CrustyCrab/
│
├── Controllers/
│   ├── AuthController.cs
│   ├── CartController.cs
│   ├── CheckoutController.cs
│   ├── CustomerController.cs
│   ├── ManagerController.cs
│   ├── MenuController.cs
│   └── ...
│
├── Models/
│   ├── ApiModels.cs
│   ├── CheckoutViewModel.cs
│   └── ...
│
├── Services/
│   └── CrustyApiClient.cs
│
├── Views/
│   ├── Auth/
│   ├── Cart/
│   ├── Checkout/
│   ├── Customer/
│   ├── Manager/
│   ├── Menu/
│   └── Shared/
│
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── images/
│
├── appsettings.json
├── Program.cs
└── CrustyCrab.csproj
```

---

## 🔌 Backend API Integration

The MVC frontend communicates with the CrustyAPI backend through the `CrustyApiClient` service.

The client centralises HTTP communication between the frontend and backend.

Operations handled through this integration include:

- Customer authentication.
- Customer management.
- Menu retrieval and management.
- Menu item image uploads.
- Cart operations.
- Order creation and management.
- Delivery address management.
- Payment method management.
- Payment creation and status updates.
- Staff management.
- Administrator management.
- CrabbyBot requests.
- Spin the Wheel functionality.

### API Configuration

The API base URL is configured through the MVC application's configuration.

For local development, the current API address is:

```text
https://localhost:7098/
```

Example configuration:

```json
{
  "Api": {
    "BaseUrl": "https://localhost:7098/"
  }
}
```

The configured address must match the address on which the backend API is running.

The API URL is centralised in configuration rather than hardcoded throughout the frontend.

**Important:** The MVC application depends on CrustyAPI. Starting the frontend without the backend running will cause API-dependent operations to fail.

---

## 🎡 Spin the Wheel – Frontend Implementation

The Spin the Wheel feature is integrated into the post-order experience.

The MVC application provides the order confirmation page and the interactive wheel interface.

The feature uses:

- Razor Views to render the wheel interface.
- Server-provided eligibility information to control whether the wheel can be used.
- JavaScript to control the spinning interaction.
- CSS transformations to animate the wheel.
- Backend API communication for the associated spin functionality.

The wheel's visual rotation is performed on the client side, while the backend handles the server-side functionality associated with the feature.

The feature is available as part of the order confirmation workflow rather than as a standalone menu or shopping cart function.

---

## 🛒 Ordering Workflow

The MVC frontend supports the following customer ordering workflow:

```text
Browse Menu
     ↓
Select Menu Item
     ↓
Choose Spice Level / Add-ons
     ↓
Add to Cart
     ↓
Checkout
     ↓
Choose Collection / Delivery
     ↓
Choose Payment Method
     ↓
Create Order
     ↓
Create Payment
     ↓
Update Payment Status
     ↓
Order Confirmation
     ↓
Spin the Wheel (Eligible Customers)
```

Customers can also proceed directly to checkout for individual menu items where supported.

---

## 💳 Payment Handling

The frontend supports payment methods managed through the backend API.

Saved customer payment methods display masked card information using the final four digits rather than exposing complete card numbers.

Example:

```text
•••• •••• •••• 1234
```

Card security information such as CVC is not stored with saved payment methods and must be entered when required.

**Payment implementation:** The current payment workflow is simulated for the project. It does not connect to a live banking or payment gateway.

---

## 🤖 CrabbyBot

The MVC application includes CrabbyBot, a predefined restaurant FAQ assistant.

CrabbyBot provides responses to common restaurant-related questions, including:

- Menu information.
- Ordering.
- Collection.
- Delivery.
- Payment.
- Restaurant information.

It uses predefined responses rather than a generative AI service.

---

## 🔐 Authentication and Security

The MVC frontend supports separate customer and administrator login flows.

Customer authentication includes:

- Email or identifier-based login.
- Password verification.
- BCrypt password hashing handled by the backend.

The MVC frontend maintains authenticated user state using server-side session data.

Administrative functionality is restricted through role checks within the MVC application, with backend authorisation handled by the API where implemented.

Saved payment methods display masked card information instead of exposing complete card numbers.

---

## 👤 Initial Administrator Account

The CrustyAPI backend includes an administrator seeding mechanism that automatically creates an initial administrator account when the application is first configured.

The seeding logic is implemented in:

```text
CrustyAPI/Data/AdminSeeder.cs
```

### Default Administrator Credentials

The initial administrator account is configured with the following credentials:

| Field | Value |
|---|---|
| Username | `Admin2` |
| Email | `admin@crustycrab.co.za` |
| Password | `ChangeMe123!` |

### How Administrator Seeding Works

The backend uses the `AdminSeeder` class to initialise an administrator account.

The seeding process:

1. Checks whether an administrator with the username `Admin2` already exists.
2. If the account exists, the seeding process stops without creating a duplicate.
3. If the account does not exist, a new administrator record is created.
4. The password is hashed using BCrypt before being stored.
5. The new administrator is saved to the database.

The administrator is created without an associated staff record (`StaffId = null`).

### Important Notes

- Administrator seeding is handled by CrustyAPI, not the MVC frontend.
- The administrator account is created in the backend database.
- The account is only created if the specified username does not already exist.
- The password is stored as a BCrypt hash rather than plaintext.
- The default credentials should be changed before using the application in a real production environment.

Once the backend has successfully seeded the account, it can be used to access the administrator login functionality through the MVC frontend, subject to the application's authentication configuration.

---

## 🚀 Running the MVC Application Locally

### Prerequisites

Install:

- .NET 10 SDK.
- Visual Studio 2022 or later.
- Git.
- MySQL Server (required by the backend API).

**The CrustyAPI backend must be running before starting the MVC application.**

### 1. Obtain the Source Code

Clone the repository:

```bash
git clone <repository-url>
```

Navigate to the MVC project directory containing `CrustyCrab.csproj`.

### 2. Start the Backend API

Before starting MVC, ensure the backend API is running.

The API must have:

- A correctly configured MySQL connection.
- An accessible database.
- Its required dependencies restored.
- Its local HTTPS development environment configured.

In the backend API directory:

```bash
dotnet restore
dotnet build
dotnet run
```

The current local API address is:

```text
https://localhost:7098
```

Keep the backend running while using the MVC application.

### 3. Configure the MVC API URL

Ensure the MVC application's configuration points to the running API:

```json
{
  "Api": {
    "BaseUrl": "https://localhost:7098/"
  }
}
```

Update the address if the backend is configured to use a different port.

### 4. Start the MVC Frontend

Open a separate terminal in the MVC project directory.

Restore dependencies:

```bash
dotnet restore
```

Build the application:

```bash
dotnet build
```

Run the application:

```bash
dotnet run
```

Alternatively, open `CrustyCrab.csproj` in Visual Studio and run the application using its configured launch profile.

Open the local address displayed in the terminal.

### Startup Order

The recommended startup sequence is:

1. Start MySQL Server.
2. Start CrustyAPI.
3. Start the MVC frontend.
4. Open the MVC application in your browser.

If the API is unavailable, operations that require backend communication will not work correctly.

---

## 🌿 Git Branch

The MVC frontend development work is maintained on:

```text
MVC-front-end
```

The backend API is maintained separately.

This separation allows the frontend and backend to be developed and maintained independently while communicating through the REST API.

---

## 👥 Project Information

**The Crusty Crab** was developed as a collaborative group software project for a client.

The MVC frontend demonstrates:

- ASP.NET Core MVC development.
- Model-View-Controller architecture.
- Razor-based frontend development.
- REST API integration.
- Customer ordering workflows.
- Shopping cart and checkout functionality.
- Menu and order management interfaces.
- Interactive JavaScript functionality.
- Post-order Spin the Wheel functionality.
- Customer and administrator interfaces.

The MVC application forms the web interface of the larger Crusty Crab restaurant ordering and management system.
