# ASP.NET CORE 7.0

### Table of Contents

- [About Project & Features](#about-project--features)
- [API Endpoints](#api-endpoints)
- [Json Example](#json-example-to-create-a-order)
- [3rd Party Libraries](#3rd-party-libraries)
- [Credits](#credits)

### About Project & Features

- This is an Asp.Net Core Api project to understand Choreography-based Saga Pattern. There are 3 APIs in this case. The first API is to create orders [Order.API], the second API is to reserve product stock count [Stok.API], and the third API is for payment [Payment.API] 
- Order.API and Stock.API work with MSSQL database. Migration can be initialized to create databases. SQL connection strings are in the "appsettings.json" file.
- RabbitMQ (cloudamqp) is selected for message broker in this project. RabbitMQ connection strings are in the "appsettings.json" file.
- APIs are both subcribers and publishers. Transactions can be seen in the image below. 

![alt text](https://github.com/DenizDemirdoven/SagaChoreographyPattern/blob/master/saga-pattern-design-choreography.jpg)

Main features of the application:

- OrdersController creates orders in Order.API.
- Stock.API subcribes the order event and if the stock balance is enough for the product, stock is reserved for the buyer. 
- Payment.API subcribes stock reserve event, if the stock count and the buyer's credit card limit are enough, then this api publishes the payment event.
- If the buyer's credit card limit is not enough, "payment failed event" will be published.
- If the stock count is enough for the order but the credit card limit is not enough, then the reserved stock will be rolled back. 

### API Endpoints
| API               | HTTP Verbs | Endpoints        | Action                                 |
| ----------------- | ---------- | ---------------- | -------------------------------------- |
| Order             | POST       | /api/orders      | To send order data as Json             |
| Stocks            | GET        | /api/stocks      | To get stock data to check stock count |

### Json example to create a order

```json
{
  "buyerId": "123",
  "orderItems": [
    {
      "productId": 1,
      "count": 1,
      "price": 100
    },
    {
      "productId": 2,
      "count": 1,
      "price": 100
    }
  ],
  "payment": {
    "cardName": "deniz demirdöven",
    "cardNumber": "1234123412341234",
    "expiration": "09/26",
    "cvv": "0602"
  },
  "address": {
    "line": "adres satır 1",
    "province": "istanbul",
    "district": "Kadıköy"
  }
}
```
### 3rd Party Libraries

The following libraries are used in the application:

- MassTransit.AspNetCore : MassTransit is a message-based distributed application framework for .NET.
- MassTransit.RabbitMQ : MassTransit provides a developer-focused, modern platform for creating distributed applications without complexity.
- Microsoft.EntityFrameworkCore : Entity Framework Core is a modern object-database mapper for .NET.
- Microsoft.EntityFrameworkCore.Design : Shared design-time components for Entity Framework Core tools
- Microsoft.EntityFrameworkCore.Tools : Entity Framework Core Tools for the NuGet Package Manager Console in Visual Studio.
- Microsoft.EntityFrameworkCore.SqlServer : Microsoft SQL Server database provider for Entity Framework Core.

### Credits

Deniz Demirdöven

- [Github](https://github.com/DenizDemirdoven)
- [LinkedIn](https://www.linkedin.com/in/denizdemirdoven)
- [Email](mailto:denizdemirdoven@gmail.com)
- [Web](https://www.denizdemirdoven.com/)
