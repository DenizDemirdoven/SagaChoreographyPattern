# ASP.NET CORE 7.0

### Table of Contents

- [About Project & Features](#about-project--features)
- [API Endpoints](#api-endpoints)
- [Json Example](#json-example-to-create-a-order)
- [3rd Party Libraries](#3rd-party-libraries)
- [Credits](#credits)

### About Project & Features

- This is an Asp.Net Core Api project to understand Choreography-based Saga Pattern. There are 3 APIs in this case. First API is for create orders [Order.API], second API is for reserve product stock count [Stok.API], third API is for payment [Payment.API] 
- Order.API and Stock.API work with MSSQL database. Migration can be initialized for databases. SQL Connection strings are in the "appsettings.json".
- RabbitMQ (cloudamqp) was selected for message broker.
- APIs are both subcriber and publisher. Transactions can be seen in tke image below. 

![alt text]([(https://github.com/DenizDemirdoven/SagaChoreographyPattern/blob/master/saga-pattern-design-choreography.jpg)]

Main features of the application:

- OrdersController create orders in Order.API.
- Stock.API subcribe the order event and if stock balance is enough for the product, stock is reserved for the buyer. 
- Payment.API subcribe stock reserve event, if the stock count is enough and buyer credit card limit is enough then this api publishes the payment event.
- If buyers credit card limit is not enough payment failed event would published.
- If stock count is enough for othe order but the credit card limit is not enough then the reserved stock will be rollback. 

### API Endpoints
| API               | HTTP Verbs | Endpoints        | Action                                 |
| ----------------- | ---------- | ---------------- | -------------------------------------- |
| Order             | POST       | /api/orders      | To send order data as Json             |
| StocksController  | GET        | /api/stocks      | To get stock data to check stock count |

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
