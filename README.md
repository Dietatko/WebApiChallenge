# WebApi Challenge

The solution consists of .NET Core WebApi application, .NET Standard client access library and .NET Framework SpecFlow acceptance tests. Comments are added to code in places where solution is not obvious.

## Service (Part 1)
### Application

The Application project is the service entry point. It uses ASP.Net Core web host (Kestrel-based). 
The service setup is splitted into modules:
- WebApiModule - configures the ASP.NET
- InMemoryDataModule - configures in-memory data storage
Modules are maybe little overkill for such a small service but in long run can be advatage as service grows. Modules could be reused in other services

Two controllers define the REST endpoint. Data validation errors are propagated to consumer as BadRequest status code.

### Domain

Defines the business logic of the service. Contains all the buisness rules:
- Order is linked to customer id (Guid) but the service does not understand what customer it is.
- Customer of an order can be changed but has to be set and cannot be erased.
- Order can delete all items and it can also be deleted.
- Deleted order cannot be changed.
- Order can have zero or many items.
- Order item is linked to product by id (Guid). This service has no knowledge of products.
- Product llinekd to order item cannot be changed (no particular reason, just variety of business rules). Item has to be removed and new item created.
- Order item has amount (decimal number).
- If second item with same product id are added, existing item is modified instead (no particular reason, just a business rule).

Domain objects folow DDD ideas.

### DataAccess

Contains naive in-memory implementation of data repository. This is the place where proper real data storage would be implemented.

### DataContracts

Data types used in REST data contract. This would be great candidate to distribute as a NuGet package.

## Client libarary (Part 2)
### Client

Is a .NET standard library to access the service. Public IOrderingClient interface is the entry point for a consuming application. It allows to get list of orders and create a new order. Each returns an order instance that allows further manipulation.
The FindOrders method lists all orders in the system. This would be the place to implement search and paging functionality in next steps.

OrderingClient is an implementation of the public IOrderingClient interface as well as internal IInternalOrderingClient interface used by object to perform further operations.

## Acceptance tests

Acceptance tests covers whole functionality of the service including client library. That is the reason I decided not to implement classic unit tests. I believe acceptance tests simulating real consumers have more value. 

### Features

Contains SpecFlow specification of the service functionality.

### Steps

Implements steps used in feature files. It uses the client library to access the service and thus testing also the client. The service has to be running in its own process and tests are connecting using network communication. So these tests are testing the service exactly how consuming application would use it (including serialization). 

## What is missing?

### HAL
The current REST service is based on plain JSON structure. As the next step I would invest into implementing proper HAL interface. While I was able to implement this on service side using the Halcyon.Net library, I did not find any reasonable client side parser. So I moved the HAL changes to hal branch for review. It is not completely finished as I would have to break client compatibility.

### Data storage
All data are stored in memory. I would implement event store to store all changes to domain objects.
