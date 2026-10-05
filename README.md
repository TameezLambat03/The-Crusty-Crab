# 🦀 The Crusty Crab

### Restaurant Ordering & Management System

The **Crusty Crab** is a full-stack restaurant ordering and management system developed as a collaborative software project.

The system provides customers with a web and mobile ordering experience while giving restaurant staff and administrators tools to manage menu items, orders, payments and staff.

The project consists of three main components working together through a central REST API:

- 🌐 ASP.NET Core MVC Web Application
- 📱 Kotlin Android Application
- ⚙️ ASP.NET Core REST API

---

## 📋 Project Overview

The Crusty Crab simulates a modern restaurant ordering platform supporting both customers and restaurant management.

### Customer Features

- Browse available menu items
- View menu item details and images
- Add items to a shopping cart
- Adjust cart quantities
- Select spice levels and add-ons
- Choose collection or delivery
- Manage delivery addresses
- Manage saved payment methods
- Place orders
- View order information
- Track order status
- Receive order and payment confirmation
- Interact with **CrabbyBot** for restaurant FAQs

### Staff & Administration

- Access the management portal
- Manage menu items
- Add, edit and remove menu items
- Upload menu item images
- Toggle menu item availability
- Manage restaurant staff
- Manage administrators
- View and update orders
- Manage payment methods
- Update payment status

---

# 🏗️ System Architecture

The project uses a multi-application architecture with the **CrustyAPI** acting as the central backend.

```text
                    ┌─────────────────────────┐
                    │        Customer         │
                    └────────────┬────────────┘
                                 │
                    ┌────────────┴────────────┐
                    │                         │
                    ▼                         ▼
          ┌──────────────────┐      ┌──────────────────┐
          │  ASP.NET MVC Web │      │ Android App      │
          │    Application   │      │   Kotlin / XML   │
          └────────┬─────────┘      └────────┬─────────┘
                   │                         │
                   │       REST API          │
                   └───────────┬─────────────┘
                               ▼
                    ┌─────────────────────┐
                    │     CrustyAPI       │
                    │ ASP.NET Core Web API │
                    └──────────┬──────────┘
                               │
                               ▼
                    ┌─────────────────────┐
                    │    MySQL Database   │
                    │   crusty_crab_db    │
                    └─────────────────────┘
```

---

# 💻 Technologies

### Web Application

- ASP.NET Core MVC
- .NET 10
- Razor Views
- HTML
- CSS
- JavaScript
- Bootstrap/custom styling

### REST API

- ASP.NET Core Web API
- .NET 10
- Entity Framework Core
- MySQL
- BCrypt password hashing

### Android Application

- Android Studio
- Kotlin
- XML layouts
- Gradle
- Android testing

### Infrastructure & Deployment

- Microsoft Azure
- Azure App Service
- MySQL
- GitHub
- GitHub Actions
- HTTPS

---

# 🌐 Deployed Applications & Services

### 🌐 Web Application

**Crusty Crab Web Application:**

https://crustycrab-aze3fccnc6gvfchd.southafricanorth-01.azurewebsites.net/

The web application provides the primary customer ordering interface and restaurant management portal.

### ⚙️ CrustyAPI

**REST API:**

https://crusty-crab-api-tameez-hjechmamdneee7c6.southafricanorth-01.azurewebsites.net/

The **CrustyAPI** is the backend REST API used by the web and Android applications. The root API URL does not display a traditional webpage because the application is designed to provide data through API endpoints.

For example, the **Menu endpoint** can be accessed here:

https://crusty-crab-api-tameez-hjechmamdneee7c6.southafricanorth-01.azurewebsites.net/api/menu

The MVC application and Android application communicate with the deployed CrustyAPI through these endpoints.
---

# 🎥 Demonstration Videos

The following videos demonstrate the functionality and features of the Crusty Crab system.

### 🎬 Web Application Demonstration

[YouTube Video – Web Application Demo] https://youtu.be/wsye5qpRHlw?si=6lh--PtJdHAy-Z6Q

### 📱 Android Application Demonstration

[YouTube Video – Android Application Demo] https://youtu.be/sIX8pzTgMb4?si=n1m2C49IucuvWmpa

## 📑 Project Presentation

[Download the Crusty Crab PowerPoint](Documentation/CrustyCrab-Presentation.pptx)
---

# 📁 Project Structure

The `main` branch contains the integrated project:

```text
TheCrustyCrab/
│
├── CrustyAPI/
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   ├── Migrations/
│   └── CrustyAPI.csproj
│
├── CrustyAPI.Tests/
│   └── ...
│
├── Controllers/
│   └── MVC Controllers
│
├── Models/
│   └── MVC Models
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
├── app/
│   └── Android application
│
├── gradle/
│
├── .github/
│   └── workflows/
│
├── CrustyCrab.csproj
├── CrustyCrab.slnx
├── Program.cs
└── README.md
```

---

# 🔌 API Integration

The **CrustyAPI** provides the central backend used by both the MVC and Android applications.

The API handles functionality including:

- Customer authentication
- Customer management
- Menu management
- Menu image uploads
- Cart operations
- Order creation and management
- Delivery addresses
- Payment methods
- Saved customer payment methods
- Payment creation and status updates
- Staff management
- Administrator management
- CrabbyBot
- Order tracking

The MVC application communicates with the API through `CrustyApiClient`.

The Android application communicates with the same deployed API.

---

# 🛒 Ordering Flow

A typical customer order follows this process:

```text
Browse Menu
     ↓
Select Item
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
```

---

# 💳 Payment Handling

The system supports saved customer payment methods.

Saved cards display masked information using only the final four digits.

Example:

```text
•••• •••• •••• 1234
```

Sensitive card security information such as CVC is not stored with saved payment methods.

The current payment implementation is a **simulated payment flow** for the project and does not connect to a live banking or payment gateway.

---

# 🤖 CrabbyBot

The project includes **CrabbyBot**, a controlled restaurant FAQ assistant.

CrabbyBot provides predefined responses for common questions regarding:

- Menu information
- Ordering
- Collection
- Delivery
- Payment
- Restaurant information

CrabbyBot is intentionally implemented as a hardcoded FAQ system rather than a generative AI service.

---

# 🔐 Authentication & Security

The system supports separate customer and administrator authentication.

Customer authentication uses:

- Email/identifier
- Password
- BCrypt password hashing

The MVC application maintains authenticated session state using server-side sessions.

Administrative functionality is protected through role-based access checks.

Communication with the deployed API uses HTTPS.

---

# 🚀 Running the Project Locally

## Requirements

Install:

- .NET 10 SDK
- Visual Studio 2022 or later
- Git
- MySQL Server for a complete local backend setup
- Android Studio for the Android application

## Clone the Repository

```bash
git clone <repository-url>
cd TheCrustyCrab
```

## Run the MVC Application

From the MVC project directory:

```bash
dotnet restore
dotnet build
dotnet run
```

Alternatively, open the appropriate `.csproj` or `.slnx` file in Visual Studio.

## Run the Android Application

Open the project in Android Studio, allow Gradle to synchronise, select an emulator or Android device, and run the application.

The Android application is configured to communicate with the deployed CrustyAPI.

---

# 🧪 Testing

The project contains automated tests for the API and Android application.

### API Tests

The API test project is located at:

```text
CrustyAPI.Tests/
```

Run API tests with:

```bash
dotnet test
```

### Android Tests

From the Android project:

```powershell
.\gradlew.bat test
```

---

# ☁️ Continuous Integration & Deployment

GitHub Actions is used to automate building, testing and deployment.

The repository contains workflows for the project's different components.

The general deployment process is:

```text
Git Push
    ↓
GitHub Actions
    ↓
Build
    ↓
Automated Tests
    ↓
Publish
    ↓
Azure App Service
```

The deployed API and MVC application are hosted through Microsoft Azure.

---

# 🌿 Git Branches

The repository uses separate development branches for the major project components:

```text
main
│
├── CrustyAPI
├── MVC-front-end
└── CrustyAndroidApp
```

### `main`

The integrated branch containing the combined Crusty Crab system.

### `CrustyAPI`

Backend API development branch.

### `MVC-front-end`

ASP.NET Core MVC web application development branch.

### `CrustyAndroidApp`

Android application development branch.

Changes from the individual development branches are integrated into `main` when appropriate.

---

# 👥 Project

**The Crusty Crab** was developed as a collaborative group software project.

The system combines:

- Web development
- Mobile development
- REST API development
- Database design
- Authentication
- Payment workflows
- API integration
- Cloud deployment
- Automated testing
- Continuous integration and deployment

---

## 🦀 The Crusty Crab

**One restaurant. Three applications. One API.**