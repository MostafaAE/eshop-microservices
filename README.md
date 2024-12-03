# A Microservices E-Commerce Platform

![microservices](https://github.com/aspnetrun/run-aspnetcore-microservices/assets/1147445/efe5e688-67f2-4ddd-af37-d9d3658aede4)

## Project Overview
This project implements e-commerce modules (Catalog, Basket, Discount, and Ordering) with both NoSQL (DocumentDb, Redis) and Relational databases (PostgreSQL, SQL Server). It uses RabbitMQ for event-driven communication and YARP API Gateway for routing.

## Modules and Features
#### Catalog Microservice
* Built with ASP.NET Core Minimal APIs leveraging the latest features of .NET 8 and C# 12.
* Implements Vertical Slice Architecture with feature folders.
* CQRS implementation using MediatR library for command and query separation.
* CQRS Validation Pipeline Behaviors with MediatR and FluentValidation.
* Uses Marten library for .NET Transactional Document DB on PostgreSQL.
* Uses Carter for Minimal API endpoint definition.
* Features cross-cutting concerns like Logging, Global Exception Handling, and Health Checks.

#### Basket Microservice
* ASP.NET 8 Web API application, Following REST API principles, CRUD.
* Utilizes Redis as a Distributed Cache for basket data.
* Implements design patterns: Proxy, Decorator, and Cache-aside.
* Syncs with the Discount microservice using gRPC for pricing calculations.
* Publishes BasketCheckout events to RabbitMQ using MassTransit.
  
#### Discount Microservice
* High-performance gRPC Server for inter-service communication with the Basket microservice.
* Defines Protobuf messages for gRPC services.
* Manages data with SQLite and Entity Framework Core, including database migrations.
* Fully containerized for seamless deployment.

#### Ordering Microservice
* Implementing DDD, CQRS, and Clean Architecture.
* Features CQRS with MediatR, FluentValidation, and Mapster.
* Consumes RabbitMQ BasketCheckout events with MassTransit.
* Manages data with SQL Server and EF Core, including automatic migrations on startup.

#### Microservices Communication
* Synchronous Communication: gRPC-based inter-service calls.
* Asynchronous Communication: RabbitMQ Message-Broker.
  * Implements Publish/Subscribe Topic Exchange Model.
  * Manages events with MassTransit.
* Shared EventBus Messages Library for consistent event contracts across microservices.

#### Yarp API Gateway Microservice
* Built using YARP Reverse Proxy, applying the Gateway Routing Pattern.
* Configurable with routes, clusters, transforms, and rate limiting via FixedWindowLimiter.

#### WebUI ShoppingApp Microservice
* ASP.NET Core web application styled with Bootstrap 4 and Razor templates.
* Consumes APIs from microservices via YARP using Refit HttpClientFactory.

#### Docker Compose Setup
* Containerizes microservices and databases.

## Run The Project
### Prerequisites
Ensure the following tools are installed on your system:
* [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
* [.Net Core 8 or later](https://dotnet.microsoft.com/download/dotnet-core/8)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Installation Steps
1. Clone the repository.
2. Start Docker Desktop.
3. Run the project:
    * Open the solution in Visual Studio, set docker-compose as the startup project, and start without debugging.
    * Alternatively, execute the following command in the root directory:
    ```csharp
    docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
    ```
4. Access the Web UI:
    * Open your browser and navigate to https://localhost:6065.
    * Explore the ShoppingApp to interact with microservices via the YARP API Gateway.
