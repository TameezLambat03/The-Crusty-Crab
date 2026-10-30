# 🍔 Crusty API

The **Crusty API** is the backend REST API for the Crusty Crab restaurant application.

It provides the backend functionality required by the web and Android applications, including customer accounts, authentication, menu management, shopping carts, orders, payments, staff and administrator management, delivery addresses, customer reviews, rewards, and the Crabby Bot dialogue assistant.

The API is built using **ASP.NET Core Web API** and connects to a **MySQL** database using **Entity Framework Core**.

---

## 🛠️ Technologies Used

- ASP.NET Core Web API
- C#
- .NET 10
- Entity Framework Core 9
- MySQL
- Pomelo.EntityFrameworkCore.MySql
- BCrypt.Net-Next
- REST API architecture

---

# 📁 Project Structure

```text
CrustyAPI/
│
├── Controllers/
│   ├── AuthController.cs
│   ├── CustomerController.cs
│   ├── DeliveryAddressController.cs
│   ├── MenuController.cs
│   ├── CartController.cs
│   ├── OrderController.cs
│   ├── PaymentController.cs
│   ├── AdminStaffController.cs
│   ├── CrabbyBotController.cs
│   ├── ReviewController.cs
│   └── RewardController.cs
│
├── Data/
│   ├── AppDbContext.cs
│   └── AdminSeeder.cs
│
├── DTOs/
│   ├── AuthDTOs.cs
│   ├── CustomerDTOs.cs
│   ├── DeliveryAddressDTOs.cs
│   ├── MenuDTOs.cs
│   ├── CartDTOs.cs
│   ├── OrderDTOs.cs
│   ├── PaymentDTOs.cs
│   ├── AdminStaffDTOs.cs
│   ├── CrabbyBotDTOs.cs
│   ├── ReviewDTOs.cs
│   └── RewardDTOs.cs
│
├── Models/
│   ├── Customer.cs
│   ├── AdminUser.cs
│   ├── DeliveryAddress.cs
│   ├── MenuItem.cs
│   ├── Cart.cs
│   ├── CartItem.cs
│   ├── Order.cs
│   ├── OrderItem.cs
│   ├── Payment.cs
│   ├── PaymentMethod.cs
│   ├── CustomerPaymentMethod.cs
│   ├── CustomerReview.cs
│   ├── CustomerReward.cs
│   └── Staff.cs
│
├── Services/
│   ├── AuthService.cs
│   ├── CustomerService.cs
│   ├── DeliveryAddressService.cs
│   ├── MenuService.cs
│   ├── CartService.cs
│   ├── OrderService.cs
│   ├── PaymentService.cs
│   ├── AdminStaffService.cs
│   ├── CrabbyBotService.cs
│   ├── GeoapifyService.cs
│   ├── ReviewService.cs
│   └── RewardService.cs
│
├── Images/
│
├── appsettings.json
├── Program.cs
└── CrustyAPI.csproj
```

---

# ⚙️ Prerequisites

Before running the API, ensure the following software is installed:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- [MySQL Workbench](https://dev.mysql.com/downloads/workbench/)
- Visual Studio or another .NET-compatible IDE

---

# 🗄️ Database Configuration

The API uses MySQL as its database provider through Entity Framework Core.

The database is named:

```text
crusty_crab_db
```

## 1. Configure MySQL

Start your local MySQL Server and connect to it using MySQL Workbench.

Create the database if it does not already exist:

```sql
CREATE DATABASE crusty_crab_db;
```

The database must contain the tables required by the API before it can start successfully.

The API's `AppDbContext` defines the database entities and their relationships. The current startup configuration does not automatically create the database tables.

## 2. Create `appsettings.json`

The `appsettings.json` file is not included in the repository because it contains environment-specific configuration and database credentials.

In the root of the `CrustyAPI` project, create a new file named:

```text
appsettings.json
```

Copy the following configuration into it:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=crusty_crab_db;user=YOUR_MYSQL_USERNAME;password=YOUR_MYSQL_PASSWORD;"
  },
  "AllowedHosts": "*"
}
```

Replace the placeholders with your own MySQL Server credentials.

- `YOUR_MYSQL_USERNAME`: Your MySQL username.
- `YOUR_MYSQL_PASSWORD`: Your MySQL password.

These are the credentials used to connect to your local MySQL Server through MySQL Workbench.

The API retrieves the connection string using:

```csharp
builder.Configuration.GetConnectionString("DefaultConnection");
```

**Important:** Do not commit your personal `appsettings.json` or database credentials to GitHub.

---

# 🚀 Running the API

Once MySQL is running, the database has been prepared, and `appsettings.json` has been configured:

Open a terminal in the `CrustyAPI` project directory and run:

```powershell
dotnet restore
dotnet build
dotnet run
```

The API runs locally on:

```text
HTTPS: https://localhost:7098
HTTP:  http://localhost:5083
```

For Postman testing, the HTTPS address can be used:

```text
https://localhost:7098
```

During development, you may need to trust your local ASP.NET Core development certificate:

```powershell
dotnet dev-certs https --trust
```

---

# 🔐 Authentication

The API supports separate customer and administrator authentication.

Passwords are hashed using **BCrypt.Net-Next** before being stored in the database.

## Customer Login

### `POST /api/auth/customer/login`

Logs a customer into the application.

The user can provide their email address as the identifier together with their password.

### Request

```json
{
  "identifier": "patrick@krustykrab.com",
  "password": "patrick123"
}
```

### Response

```json
{
  "success": true,
  "userId": 1,
  "username": "Patrick",
  "role": "Customer",
  "message": "Login successful."
}
```

---

## Admin Login

### `POST /api/auth/admin/login`

Authenticates an administrator using their admin account.

### Request

```json
{
  "identifier": "admin",
  "password": "password123"
}
```

The API verifies the supplied password against the securely hashed password stored in the database.

---

# 👤 Customer Endpoints

## Get All Customers

### `GET /api/customer`

Returns all registered customers.

Passwords are not returned to the client.

---

## Get Customer

### `GET /api/customer/{id}`

Returns a specific customer using their Customer ID.

Example:

```text
GET /api/customer/1
```

---

## Create Customer

### `POST /api/customer`

Creates a new customer account.

### Request

```json
{
  "firstName": "Patrick",
  "lastName": "Star",
  "phoneNumber": "0812345678",
  "emailAddress": "patrick@example.com",
  "password": "patrick123"
}
```

The API hashes the password before storing it in the database.

Duplicate email addresses are rejected.

---

## Update Customer

### `PUT /api/customer/{id}`

Updates an existing customer's information.

Example:

```text
PUT /api/customer/1
```

---

## Delete Customer

### `DELETE /api/customer/{id}`

Deletes a customer.

The database is configured so that related delivery addresses and carts can be removed through the customer relationship.

---

# 📍 Delivery Address Endpoints

## Get All Delivery Addresses

### `GET /api/deliveryaddress`

Returns all delivery addresses stored in the database.

---

## Get Delivery Address

### `GET /api/deliveryaddress/{id}`

Returns a specific delivery address.

Example:

```text
GET /api/deliveryaddress/1
```

---

## Get Customer Addresses

### `GET /api/deliveryaddress/customer/{customerId}`

Returns all delivery addresses belonging to a particular customer.

Example:

```text
GET /api/deliveryaddress/customer/1
```

---

## Create Delivery Address

### `POST /api/deliveryaddress`

Creates a new delivery address for a customer.

### Request

```json
{
  "suburb": "Sandton",
  "houseNumber": "123",
  "streetName": "Main Street",
  "buildingType": "House",
  "customerID": 1
}
```

---

## Update Delivery Address

### `PUT /api/deliveryaddress/{id}`

Updates an existing delivery address.

---

## Delete Delivery Address

### `DELETE /api/deliveryaddress/{id}`

Deletes a delivery address.

---

# 🍔 Menu Endpoints

## Get Menu

### `GET /api/menu`

Returns the available menu items.

This endpoint is used by the web and Android applications to retrieve menu data.

Menu items contain information such as:

- Item name
- Description
- Price
- Category
- Availability
- Image URL

---

## Get Menu Item

### `GET /api/menu/{id}`

Returns one specific menu item.

Example:

```text
GET /api/menu/1
```

---

## Create Menu Item

### `POST /api/menu`

Creates a new menu item.

### Request

```json
{
  "itemName": "Krabby Patty",
  "description": "A delicious Krabby Patty.",
  "price": 59.99,
  "category": "Burgers",
  "availability": true,
  "imageUrl": "/Images/Menu/krabby-patty.jpg"
}
```

---

## Update Menu Item

### `PUT /api/menu/{id}`

Updates an existing menu item.

This can be used by administrators to change:

- Name
- Description
- Price
- Category
- Availability
- Image URL

---

## Delete Menu Item

### `DELETE /api/menu/{id}`

Deletes a menu item.

---

# 🛒 Cart Endpoints

## Create Cart

### `POST /api/cart`

Creates a shopping cart for a customer.

The API prevents a customer from unnecessarily creating multiple carts.

---

## Get Cart

### `GET /api/cart/{id}`

Returns a cart and its cart items.

Example:

```text
GET /api/cart/1
```

---

## Get Customer Cart

### `GET /api/cart/customer/{customerId}`

Returns the cart belonging to a particular customer.

Example:

```text
GET /api/cart/customer/1
```

---

## Add Item to Cart

### `POST /api/cart/{cartId}/items`

Adds a menu item to a customer's cart.

### Request

```json
{
  "menuItemID": 1,
  "quantity": 2
}
```

The API obtains the current menu price from the database rather than trusting a price supplied by the client.

---

## Update Cart Item

### `PUT /api/cart/items/{cartItemId}`

Changes the quantity of an item already in the cart.

### Request

```json
{
  "quantity": 3
}
```

The cart total is recalculated after the update.

---

## Remove Cart Item

### `DELETE /api/cart/items/{cartItemId}`

Removes an item from the cart.

The cart total is recalculated afterwards.

---

# 📦 Order Endpoints

## Get All Orders

### `GET /api/order`

Returns all orders.

This can be used by the administration side of the application to view orders.

---

## Get Order

### `GET /api/order/{id}`

Returns a specific order and its order items.

Example:

```text
GET /api/order/1
```

---

## Get Customer Orders

### `GET /api/order/customer/{customerId}`

Returns the orders belonging to a particular customer.

Example:

```text
GET /api/order/customer/1
```

---

## Create Order

### `POST /api/order`

Creates a new order.

The API calculates the prices of the order items using the current prices stored in the menu database.

This prevents the client from submitting an incorrect item price.

### Example Request

```json
{
  "customerID": 1,
  "deliveryMethod": "Delivery",
  "deliveryAddressID": 1,
  "items": [
    {
      "menuItemID": 1,
      "quantity": 2,
      "notes": "Extra sauce"
    }
  ]
}
```

The API calculates the order total automatically.

---

## Update Order Status

### `PUT /api/order/{id}/status`

Updates the status of an order.

### Request

```json
{
  "orderStatus": "Preparing"
}
```

Example order statuses can include:

```text
Pending
Preparing
Ready
Completed
Cancelled
```

---

# 💳 Payment Endpoints

## Get Payment Methods

### `GET /api/payment/methods`

Returns all available payment methods.

---

## Get Payment Method

### `GET /api/payment/methods/{id}`

Returns a specific payment method.

---

## Create Payment Method

### `POST /api/payment/methods`

Adds a new payment method.

### Request

```json
{
  "methodName": "Card"
}
```

---

## Delete Payment Method

### `DELETE /api/payment/methods/{id}`

Deletes a payment method.

---

## Create Payment

### `POST /api/payment`

Creates a payment associated with an order.

### Request

```json
{
  "amount": 119.98,
  "paymentStatus": "Pending",
  "paymentMethodID": 1,
  "orderID": 1
}
```

---

## Get Payment

### `GET /api/payment/{id}`

Returns a specific payment.

Example:

```text
GET /api/payment/1
```

---

## Get Order Payments

### `GET /api/payment/order/{orderId}`

Returns payments associated with a specific order.

Example:

```text
GET /api/payment/order/1
```

---

## Update Payment Status

### `PUT /api/payment/{id}/status`

Updates the status of a payment.

### Request

```json
{
  "paymentStatus": "Paid"
}
```

---

# 👨‍🍳 Staff & Admin Endpoints

Staff members and administrator accounts are separate entities.

A **Staff** record contains employee information, while an **Admin User** is an account that can be used to log into administrative functionality.

Not every staff member needs an administrator account.

## Staff

### `GET /api/adminstaff/staff`

Returns all staff members.

### `GET /api/adminstaff/staff/{id}`

Returns a specific staff member.

### `POST /api/adminstaff/staff`

Creates a staff member.

Example:

```json
{
  "firstName": "Squidward",
  "lastName": "Tentacles",
  "role": "Manager",
  "phoneNumber": "0812345678"
}
```

### `PUT /api/adminstaff/staff/{id}`

Updates a staff member.

### `DELETE /api/adminstaff/staff/{id}`

Deletes a staff member.

---

# 🔑 Admin User

## Get Admin Users

### `GET /api/adminstaff/admins`

Returns all administrator accounts.

Passwords are never returned.

---

## Get Admin User

### `GET /api/adminstaff/admins/{id}`

Returns a specific administrator account.

---

## Create Admin User

### `POST /api/adminstaff/admins`

Creates an administrator account.

The password is hashed before being stored.

An administrator can optionally be linked to an existing staff member.

---

## Update Admin User

### `PUT /api/adminstaff/admins/{id}`

Updates an administrator account.

---

## Delete Admin User

### `DELETE /api/adminstaff/admins/{id}`

Deletes an administrator account.

Deleting an admin account does not necessarily delete the associated staff member.

---

# ⭐ Customer Reviews

The API includes functionality for customers to submit reviews and administrators to respond to them.

The review functionality supports:

- Customer review submissions.
- Star ratings.
- Written comments.
- Administrator responses.

Review records are associated with customers, while administrator responses can be linked to an administrator account.

---

# 🎁 Customer Rewards

The API includes a customer rewards system associated with orders.

Reward records can store:

- Customer information.
- Associated order.
- Prize name.
- Discount percentage.
- Reward code.

The database uses unique indexes for order IDs and reward codes to prevent duplicate reward associations.

---

# 🦞 Crabby Bot

Crabby Bot is a predefined **dialogue/FAQ system** designed to help customers with common restaurant questions.

It does not use generative AI. Instead, the API provides predefined responses and options based on the option selected by the customer.

This makes the responses predictable and allows the restaurant to control the information provided to customers.

## Crabby Bot Response

### `POST /api/CrabbyBot`

Returns a response based on the selected option.

### Request

```json
{
  "optionId": "start"
}
```

The `start` option provides choices such as:

- 🍔 Menu
- 🚚 Delivery
- 🛒 Ordering
- 💳 Payments
- 🕐 Opening Hours
- 📞 Contact Information

---

# 👤 Initial Administrator Account

The API automatically attempts to create an initial administrator account when it starts.

This is handled by `AdminSeeder.cs` in the `Data` folder.

During startup, `Program.cs` creates a service scope, retrieves the `AppDbContext`, and calls:

```csharp
await AdminSeeder.SeedAsync(context);
```

The seeder checks whether an administrator with the configured username already exists.

If the account does not exist, it creates the administrator and hashes the password using BCrypt before saving it to the database.

### Default Administrator Credentials

| Field | Value |
|---|---|
| Username | `Admin2` |
| Email | `admin@crustycrab.co.za` |
| Password | `ChangeMe123!` |

The initial administrator is not linked to a staff record (`StaffId` is null).

**Important:** These are development credentials. Change the default password before using the application in a production environment.

If the administrator already exists, the seeder does not create another account or reset its password.

---

# 🌐 API Integration

The Crusty API acts as the backend for the Crusty Crab applications.

- **MVC Web Application:** Uses HTTP requests to retrieve and update backend data.
- **Android Application:** Communicates with the API to access customer, menu, cart and order functionality.

The API must be running for applications that depend on it to access backend functionality.

---

# 🖼️ Image Storage

The API serves uploaded and stored images from its local `Images` directory.

The directory is configured as a static file provider in `Program.cs` and is accessible through the `/Images` request path.

Menu image uploads are limited to **5 MB per image**.

---

# 📝 Important Notes

- MySQL Server must be running before starting the API.
- The database and required tables must exist before the API starts.
- Each developer must create their own `appsettings.json` with their local database credentials.
- Do not commit personal database credentials to source control.
- The API runs locally and is intended to be used alongside the MVC and Android applications.
- Payment functionality is simulated and is not connected to a live payment gateway.
- Crabby Bot uses predefined responses rather than generative AI.
